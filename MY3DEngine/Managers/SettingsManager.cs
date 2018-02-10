using MY3DEngine.Models;
using MY3DEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY3DEngine.Managers
{
    public sealed class SettingsManager
    {
        private const string OverrideFolderName = "\\Override";

        private const string DefaultIniFileName = "\\DefaultSettings.ini";
        private const string DefaultAssetsPath = "\\Assets";
        private const string DefaultShaderPath = "\\Assets\\Shaders";

        private readonly string CurrentDirectory = FileIO.GetCurrentDirectory;

        private Settings settings;

        public Settings Settings => this.settings;

        public bool Initialize()
        {
            if(!FileIO.FileExists($"{CurrentDirectory}{DefaultIniFileName}"))
            {
                return false;
            }

            try
            {
                this.settings = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(FileIO.GetFileContent($"{CurrentDirectory}\\{DefaultIniFileName}"));
            }
            catch(Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            if (FileIO.DirectoryExists($""))
            {

            }

            if(string.IsNullOrWhiteSpace(this.settings.ShaderPath))
            {
                this.settings.ShaderPath = $"{CurrentDirectory}{DefaultShaderPath}";
            }

            return true;
        }
    }
}
