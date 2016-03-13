using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDePlanillas.Models
{
    public  class ViewManager
    {
        static private ViewManager instance;
        private Dictionary<string, OperationsGroup> groups;

        private ViewManager()
        {
            groups = new Dictionary<string, OperationsGroup>();
            var groupsList = DBManager.Instance.selectOperationsGroups().detail;
            foreach(var group in groupsList)
            {
                groups[group.name] = group;
            }
        }

        public static ViewManager Instance
        {
            get
            {
                return instance == null ? (instance = new ViewManager()) : instance;
            }
        }  
        
        public OperationsGroup getOperationsGroup(string name)
        {
            return groups[name];
        }
    }

    public class MenubarConfig 
    {
        public string name;
        public string icon;
        public List<Tuple<string, string>> options;

        public MenubarConfig(string name, List<Tuple<string, string>> options, string icon)
        {
            this.name = name;
            this.icon=icon;
            this.options = options;
        }
    }

    public class NavbarConfig
    {
        public Dictionary<string, MenubarConfig> menus;
        public List<MenubarConfig> leftMenus;
        public List<MenubarConfig> rightMenus;

        public NavbarConfig(Dictionary<string, HashSet<string>> privileges)
        {
            leftMenus = new List<MenubarConfig>();
            rightMenus = new List<MenubarConfig>();
            menus = new Dictionary<string, MenubarConfig>();
            ViewManager vm = ViewManager.Instance;
            foreach(KeyValuePair<string, HashSet<string>> grupo in privileges)
            {
                OperationsGroup og = vm.getOperationsGroup(grupo.Key);
                var options = new List<Tuple<string, string>>();
                foreach(string operacion in grupo.Value)
                {
                    Operation op = og.operations[operacion];
                    options.Add(new Tuple<string,string>(op.desc,op.url));
                }
                MenubarConfig menu = new MenubarConfig(og.desc, options, og.icon);
                (og.rightAlign ? rightMenus : leftMenus).Add(menu);
                menus[grupo.Key] = menu;
            }
        }
    }

    //configuration to pass to a view before processing it
    public class ViewConfig
    {
        public string title { set; get; }
        public string activeMenu { set; get; } 
        public string activeOption { set; get; } 
        public bool showNavbar { set; get; }
        public bool showMenubar { set; get; }
    }
}
