#region Using namespaces.  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
#endregion

namespace DeltaXSetup.Helpers
{
    public static class JSONHelper
    {
        #region Public extension methods.  
        /// <summary>  
        /// Extened method of object class, Converts an object to a json string.  
        /// </summary>  
        /// <param name="obj"></param>  
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch (Exception)
            {                
                return "";
            }
        }
        #endregion

        public static T DeserializeObj<T>(HttpContextBase context)
        {
            StreamReader sr = new StreamReader(context.Request.InputStream);
            string json = sr.ReadToEnd();
            try
            {
                return Serializer.FromJson<T>(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T DeserializeObj<T>(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            try
            {
                return Serializer.FromJson<T>(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string SerializeObj(object obj)
        {
            return Serializer.ToJSON(obj);
        }
    }
}