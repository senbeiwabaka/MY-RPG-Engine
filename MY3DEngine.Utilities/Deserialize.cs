namespace MY3DEngine.Utilities
{
    using System;
    using MY3DEngine.Utilities.Interfaces;
    using Newtonsoft.Json;
    using NLog;

    public static class Deserialize
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static T DeserializeStringAsT<T>(string content) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                Logger.Error(e, nameof(DeserializeStringAsT));
            }

            return new T();
        }

        public static T DeserializeFileAsT<T>(string path, IFileIO fileIo) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(fileIo.GetFileContent($"{path}"));
            }
            catch (Exception e)
            {
                Logger.Error(e, nameof(DeserializeFileAsT));
            }

            return new T();
        }
    }
}
