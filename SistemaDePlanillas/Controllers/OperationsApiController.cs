﻿using SistemaDePlanillas.Models;
using SistemaDePlanillas.Models.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace SistemaDePlanillas.Controllers
{
    public class OperationsApiController : ApiController
    {
        public string Post(string group, string operation, [FromBody]object[] args)
        {
            try
            {
                //gets the session state
                var Session = HttpContext.Current.Session;

                SessionManager sm = SessionManager.getInstance();
            
                //checks if the user is logged
                if (!sm.isLogged(Session))
                {
                    return Responses.Error(10, "No se ha iniciado sesion");
                }

                User user = sm.getUser(Session);
                //checks if the user have privileges to do the current operation
                //if (!sm.verifyOperation(user, group, operation))
                //{
                //    return "{status:'ERROR', error:11, detail:'El rol: "+sm.getRoleName(user)+" no cuenta con el permiso para realizar "+group+"/"+operation+"'}";
                //}    

                //add the user to the parameters   
                object[] parameters = new object[args.Length + 1];
                parameters[0] = user;
                System.Array.Copy(args, 0, parameters, 1, args.Length);

                //formatting the class name
                string groupType = "SistemaDePlanillas.Models.Operations." + group + "Group";

                //Uses reflexion to get the correct method
                Type type = Type.GetType(groupType, false, true);
                if (type == null)
                {
                    return Responses.Error(12, "No se encuentra el grupo: " + group + ", imposible realizar operacion");
                }
                MethodInfo method = type.GetMethod(operation, BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (method == null)
                {
                    return Responses.Error(13, "No se encuentra la operacion: " + group + "/" + operation);
                }
                
                //call the method
                return (string)method.Invoke(null, parameters);
            }
            catch (TargetParameterCountException)
            {
                return Responses.Error(14, "No coincide el numero de parametros esperado para: " + group + "/" + operation);
            }
            catch (ArgumentException)
            {
                return Responses.Error(15, "No coincide el tipo de los argumentos esperados para: " + group + "/" + operation);
            }
            catch (Exception e)
            {
                return Responses.ExceptionError(e);
            }
        }


    }
}