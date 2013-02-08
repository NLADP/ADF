using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Adf.Core.Extensions;

namespace Adf.WinRT.Extensions
{
    public static class ObjectExtensions
    {
        public static string SerializeJson<T>(this T obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            using (var stream = new MemoryStream())
            {
                new DataContractJsonSerializer(typeof(T)).WriteObject(stream, obj);
                var byteArray = stream.ToArray();
                return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            }
        }

        public static T DeserializeJson<T>(this string json)
        {
            if (json.IsNullOrEmpty()) throw new ArgumentNullException("json");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
            }
        }

        public static object DeserializeJson(this string json, Type type)
        {
            if (json.IsNullOrEmpty()) throw new ArgumentNullException("json");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return new DataContractJsonSerializer(type).ReadObject(stream);
            }
        }
    }
}
