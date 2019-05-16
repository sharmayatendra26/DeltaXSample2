using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DeltaXSetup.Helpers
{
    public static class Serializer
    {
        public static string ToJSON(object obj, bool indented = false)
        {
            if (indented)
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static T FromJson<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static T FromJson<T>(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            return FromJson<T>(json);
        }

        public static T FromJson<T>(HttpContextBase context)
        {
            return FromJson<T>(context.Request.InputStream);
        }
    }
}