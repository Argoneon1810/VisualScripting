using System.IO;
using UnityEngine;

namespace IO
{
    public static class JSONParser<T>
    {
        public static string SaveToJSON(string path, T toParse)
        {
            string stringifiedObject = JsonUtility.ToJson(toParse, true);
            File.WriteAllText(path, stringifiedObject);
            return stringifiedObject;
        }

        public static T ReadFromJson(string path)
        {
            string jsonInString = File.ReadAllText(path);
            var parsedJson = JsonUtility.FromJson<T>(jsonInString);
            return parsedJson;
        }
    }
}