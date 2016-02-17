using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SistemaDePlanillas.Models
{
    public class SessionManager
    {
        private static SessionManager single;
        private Dictionary<long, User> loggedUsers;
        private Dictionary<long, Role> roles;

        public static SessionManager getInstance()
        {
            return single != null ? single : single = new SessionManager();
        }

        private SessionManager()
        {
            loggedUsers = new Dictionary<long, User>();
            roles = new Dictionary<long, Role>();

            var rolesList = DBManager.getInstance().selectRoles().detail;
            foreach(Role role in rolesList)
            {
                roles[role.id] = role;
            }
        }

        private Role getRole(long id)
        {
                return roles[id];
        }

        public NavbarConfig getNavbarConfig(User user)
        {
            return getRole(user.role).navbar;
        }

        public MenubarConfig getMenuBarConfig(User user, string group)
        {
            return getRole(user.role).navbar.menus[group];
        }

        public void updateRole(int roleId)
        {
            roles[roleId].update();
        }

        public bool verifyOperation(User user, string group, string operation)
        {
            var privileges = getRole(user.role).privileges;
            return privileges.ContainsKey(group) && privileges[group].Contains(operation);
        }

        public string getRoleName(User user)
        {
            return getRole(user.role).name;
        }

        public bool isLogged(HttpSessionStateBase session)
        {
            return session["user"] != null;
        }

        public User getUser(HttpSessionStateBase session)
        {
            return (User)session["user"];
        }

        public bool login(string username, string password, HttpSessionStateBase session)
        {        
            User user = DBManager.getInstance().login(username,password).detail;
            user.session = session;
            session["user"] = user;
            loggedUsers[user.id] = user;
            return true;
        }

        public bool logout(HttpSessionStateBase session)
        {
            User user = getUser(session);
            loggedUsers.Remove(user.id);
            session.Remove("user");
            return true;
        }

        public bool logout(int id)
        {
            User user = loggedUsers[id];
            loggedUsers.Remove(id);
            user.session.Remove("user");
            return true;
        }

    }
}
