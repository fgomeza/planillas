using SistemaDePlanillas.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Security;
using System.Web.SessionState;

namespace SistemaDePlanillas.Models
{
    public class SessionManager
    {
        private static SessionManager instance;
        private Dictionary<long, Role> roles;

        class InexistentUserException : Exception
        {
            long id;
            public InexistentUserException(long id)
            {
                this.id = id;
            }

            public override string Message
            {
                get
                {
                    return "The user with id = "+id+", not longer exist in the database, CLOSE HIS SESSION!";
                }
            }
        }

        public static SessionManager Instance
        {
            get
            {
                return instance == null ? (instance = new SessionManager()) : instance;
            }
        }

        private SessionManager()
        {
            updateRoles();
        }

        public void updateRoles()
        {
            var rolesList = DBManager.Instance.roles.selectAllActiveRoles();
            roles = rolesList.ToDictionary(r => r.id);
        }

        public IEnumerable<Role> getRoles()
        {
            return roles.Values;
        }

        public Role getRole(long id)
        {
            return roles[id];
        }

        public bool verifyOperation(User user, string group, string operation)
        {
            var current = DBManager.Instance.locations.getLocation(user.Location).CurrentPayroll;
            if (current == null)
                return verifyOperation(user.Role, group.ToLower(), operation.ToLower());
            else
                return verifyOperationLocked(user.Role, group.ToLower(), operation.ToLower());
        }

        private bool verifyOperation(long role, string group, string operation)
        {
            var privileges = getRole(role).privileges;
            return privileges.ContainsKey(group) && privileges[group].ContainsKey(operation);
        }

        private bool verifyOperationLocked(long role, string group, string operation)
        {
            var privileges = getRole(role).privileges;
            return privileges.ContainsKey(group) && privileges[group].ContainsKey(operation) && !privileges[group][operation];
        }

        public string getRoleName(User user)
        {
            return getRole(user.Role).name;
        }

        private User userFromTicket(FormsAuthenticationTicket ticket)
        {
            long userId = long.Parse(ticket.UserData);
            User user = DBManager.Instance.users.selectUser(userId);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new InexistentUserException(userId);
            }
        }

        public void setSessionUser(HttpResponseBase response, User user, bool isPersistent)
        {
            string userId = user.Id.ToString();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                user.Name,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                isPersistent,
                userId,
                FormsAuthentication.FormsCookiePath);
            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);
            // Create the cookie.
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        public void removeSessionUser(HttpRequestBase request)
        {
            FormsAuthentication.SignOut();
        }

        public User getSessionUser(HttpRequestContext request)
        {
            FormsIdentity id = (FormsIdentity)request.Principal.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            return userFromTicket(ticket);
        }

        public User getSessionUser(HttpRequestBase request)
        {
            var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            return userFromTicket(ticket);
        }

        public bool validateUser(string username, string password , out User user, out AppException error)
        {
            try
            {
                user= DBManager.Instance.users.login(username, password);
                error = null;
                return true;
            }
            catch(AppException e)
            {
                user = null;
                error = e;
                return false;
            }
        }

    }
}
