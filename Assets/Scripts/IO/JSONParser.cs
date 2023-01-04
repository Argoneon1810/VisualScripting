using System.IO;
using UnityEngine;

namespace IO
{
    public static class JSONParser<T>
    {
        public static string SaveToJSON(string dir, string filename, T toParse)
        {
            string stringifiedObject = JsonUtility.ToJson(toParse, true);

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            FileStream fs = File.Create(dir + "/" + filename + ".json");

            StreamWriter sw = new StreamWriter(fs);
            sw.Write(stringifiedObject);
            sw.Flush();

            return stringifiedObject;
        }

        public static T ReadFromJson(string path, string filename)
        {
            string jsonInString = File.ReadAllText(path + "/" + filename + ".json");
            var parsedJson = JsonUtility.FromJson<T>(jsonInString);
            return parsedJson;
        }
    }
}