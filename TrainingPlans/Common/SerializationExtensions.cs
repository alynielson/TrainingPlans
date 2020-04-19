using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace TrainingPlans.Common
{
    public static class SerializationExtensions
    {
        public static string ToJsonString<T>(this T value, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(value, typeof(T), settings);
        }

        public static string ToJsonString(this object value, Type type, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(value, type, settings);
        }

        public static async Task<byte[]> ToByteArray<T>(this T obj)
        {
            if (obj is null)
            {
                return null;
            }
            var binaryFormatter = new BinaryFormatter();
            await using var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }
        public static async Task<T> FromByteArray<T>(this byte[] byteArray)
        {
            if (byteArray is null)
            {
                return default;
            }
            var binaryFormatter = new BinaryFormatter();
            await using var memoryStream = new MemoryStream(byteArray);
            return (T)binaryFormatter.Deserialize(memoryStream);
        }
    }
}
