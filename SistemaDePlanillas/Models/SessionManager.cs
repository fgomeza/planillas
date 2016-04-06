using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

            var rolesList = DBManager.Instance.selectRoles().Detail;
            foreach(Role role in rolesList)
            {
                roles[role.id] = role;
            }
        }

        public void updateRoles()
        {
            roles = new Dictionary<long, Role>();

            var rolesList = DBManager.Instance.selectRoles().Detail;
            foreach (Role role in rolesList)
            {
                roles[role.id] = role;
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

        public NavbarConfig getNavbarConfig(User user)
        {
            return getRole(user.Role).navbar;
        }

        public MenubarConfig getMenuBarConfig(User user, string group)
        {
            return getRole(user.Role).navbar.menus[group];
        }

        public void updateRole(int roleId)
        {
            roles[roleId].update();
        }

        public bool verifyOperation(User user, string group, string operation)
        {
            var privileges = getRole(user.Role).privileges;
            return privileges.ContainsKey(group) && privileges[group].Contains(operation);
        }

        public string getRoleName(User user)
        {
            return getRole(user.Role).name;
        }

        public bool isLogged(HttpSessionStateBase session)
        {
            return session["user"] != null;
        }

        public User getUser(HttpSessionStateBase session)
        {
            return (User)session["user"];
        }

        public bool isLogged(HttpSessionState session)
        {
            return session["user"] != null;
        }

        public User getUser(HttpSessionState session)
        {
            return (User)session["user"];
        }

        public bool login(string username, string password, HttpSessionStateBase session)
        {
            var Result = DBManager.Instance.login(username, password);
            if (Result.Status == 0)
            {
                User User = Result.Detail;
                User.Session = session;
                session["user"] = User;
                session["UserName"] = User.Name;
                return true;
            }
            return false;
        }

        public bool logout(HttpSessionStateBase session)
        {
            User user = getUser(session);
            session.Remove("user");
            return true;
        }

        /*
        public bool logout(int id)
        {
            User user = loggedUsers[id];
            loggedUsers.Remove(id);
            user.session.Remove("user");
            return true;
        }
        */
    }
}
