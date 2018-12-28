// <copyright file="Deserialize.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Utilities
{
    using Newtonsoft.Json;
    using NLog;
    using System;
    using Utilities.Interfaces;

    public static class Deserialize
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static T DeserializeStringAsT<T>(string contents)
            where T : new()
        {
            T result = new T();

            try
            {
                result = JsonConvert.DeserializeObject<T>(contents);
            }
            catch (Exception e)
            {
                Logger.Error(e, nameof(DeserializeStringAsT));
            }

            return result;
        }

        public static T DeserializeFileAsT<T>(string path, IFileService fileIo)
            where T : new()
        {
            string contents = fileIo.GetFileContent($"{path}");

            return DeserializeStringAsT<T>(contents);
        }
    }
}
