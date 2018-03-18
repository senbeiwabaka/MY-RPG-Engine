using MY3DEngine.Logging;
using Newtonsoft.Json;
using System;

namespace MY3DEngine.Utilities
{
    public static class Deserialize
    {
        public static T DeserializeStringAsT<T>(string content)where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch(Exception e)
            {
                WriteToLog.Exception(nameof(DeserializeStringAsT), e);
            }

            return new T();
        }

        public static T DeserializeFileAsT<T>(string path) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(FileIO.GetFileContent($"{path}"));
            }
            catch(Exception e)
            {
                WriteToLog.Exception(nameof(DeserializeFileAsT), e);
            }

            return new T();
        }
    }
}