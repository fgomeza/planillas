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
        private Dictionary<long, User> loggedUsers;
        private Dictionary<long, Role> roles;

        public static SessionManager Instance
        {
            get
            {
                return instance == null ? (instance = new SessionManager()) : instance;
            }
        }

        private SessionManager()
        {
            loggedUsers = new Dictionary<long, User>();
            roles = new Dictionary<long, Role>();

            var rolesList = DBManager.Instance.selectAllRoles().Detail;
            foreach(Role role in rolesList)
            {
                roles[role.id] = role;
            }
        }

        public void updateRoles()
        {
            roles = new Dictionary<long, Role>();

            var rolesList = DBManager.Instance.selectAllRoles().Detail;
            foreach (Role role in rolesList)
            {
                roles[role.id] = role;
            }
        }

        public void updateUser(long id)
        {
            if (loggedUsers.ContainsKey(id))
            {
                loggedUsers[id] = DBManager.Instance.selectUser(id).Detail;
            }
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
            return verifyOperation(user.Role, group, operation);
        }

        public bool verifyOperation(long role, string group, string operation)
        {
            var privileges = getRole(role).privileges;
            return privileges.ContainsKey(group) && privileges[group].Contains(operation);
        }

        public string getRoleName(User user)
        {
            return getRole(user.Role).name;
        }

        private User userFromTicket(FormsAuthenticationTicket ticket)
        {
            long userId = long.Parse(ticket.UserData);
            return loggedUsers[userId];
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
            loggedUsers[user.Id] = user;
        }

        public void removeSessionUser(HttpRequestBase request)
        {
            var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            long userId = long.Parse(ticket.UserData);
            loggedUsers.Remove(userId);
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

        public User validateUser(string username, string password)
        {
            var Result = DBManager.Instance.login(username, password);
            return Result.Status == 0 ?
                Result.Detail : null;
        }

    }
}
