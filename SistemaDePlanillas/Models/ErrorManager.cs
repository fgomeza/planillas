﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SistemaDePlanillas.Models
{
    /// <summary>
    /// Esta clases gestiona el lanzamiendo de mensajes entre modulos 
    /// hace a la ves de repositorio comun de errores
    /// </summary>
    public class ErrorManager
    {
        private static Dictionary<int, Message> reposotory = new Dictionary<int, Message>();
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();
        private static bool isLoad = false;

        /// <summary>
        /// Carga el diccionario con la informacion de los errores
        /// </summary>
        private static void loadDictionary()
        {
            /*Esto es un pseudo origen de datos*/
            List<Message> ErrorList = new List<Message>();
            ErrorList.Add(new Message(1, "Error tipo 1"));
            ErrorList.Add(new Message(2, "Error tipo 2"));
            ErrorList.Add(new Message(3, "Error tipo 3"));
            ErrorList.Add(new Message(4, "Error tipo 4"));
            /*de aca consulta a la base de datos*/
            foreach (Message message in ErrorList)
                reposotory.Add(message.Code, message);
            isLoad = true;

        }
        /// <summary>
        /// Solicita la recarga de los datos del repositorio de errores
        /// en su proxima ejecución
        /// </summary>
        public static void reloadDictionary()
        {
            isLoad = false;
        }
        /// <summary>
        /// Busca y retorna un error en el repositorio comun por su id
        /// </summary>
        /// <param name="code">Codigo de error a solicitar</param>
        /// <returns>informacion del error en formato JSON, null</returns>
        
        public static string ErrorByCode(int code)
        {
            if (!isLoad)
                loadDictionary();
            Message error = (reposotory.ContainsKey(code)) ? reposotory[code] : null;
            return (error == null) ? "{status:'Error no encontrado'}" : serializer.Serialize(error);
        }
    }

    /// <summary>
    /// Arquetipo de un mensaje para serializarse a JSON
    /// </summary>

    public class Message
    {
        private string _status;
        private string _details;
        private int _code;


        public Message(int code, string details)
        {
            Code = code;
            Status = "Error";
            Details = details;
        }
        public Message(int code,string status, string details)
        {
            Code = code;
            Status = status;
            Details = details;
        }
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }

    }
}
