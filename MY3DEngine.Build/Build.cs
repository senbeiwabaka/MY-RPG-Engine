using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MY3DEngine.Build.Properties;
using MY3DEngine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MY3DEngine.Build
{
    // TODO - REFACTOR/FIX/STOP USING ENGINE EXCEPTIONS
    public static class Build
    {
        /// <summary>
        /// Builds the game
        /// </summary>
        /// <param name="folderLocation">The folder location with the files to build and where the .exe goes</param>
        /// <param name="gameName">The name of the game</param>
        /// <returns></returns>
        public static bool BuildGame(string folderLocation, string gameName)
        {
            try
            {
                var fileName = "main.cs";
                var fullPath = $"{folderLocation}\\{fileName}";
                var fileContents = File.ReadAllText(fullPath);
                var engine = typeof(Engine).Assembly;
                var systemCollectionGenerics = typeof(System.Collections.Generic.IEnumerable<string>).Assembly;
                var linq = typeof(Enumerable).Assembly;
                var tree = CSharpSyntaxTree.ParseText(fileContents);
                var references = new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(System.Windows.Forms.Form).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Threading.ThreadExceptionEventArgs).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.ComponentModel.ComponentCollection).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Drawing.Size).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Object).Assembly.Location),
                    MetadataReference.CreateFromFile(systemCollectionGenerics.Location),
                    MetadataReference.CreateFromFile(engine.Location),
                    MetadataReference.CreateFromFile(linq.Location)
                };
                var csharpCompilationOptions = new CSharpCompilationOptions(
                    outputKind: OutputKind.WindowsApplication,
                    optimizationLevel: OptimizationLevel.Debug,
                    platform: Platform.AnyCpu);
                var cSharpCompilation = CSharpCompilation.Create(gameName, new[] { tree }, references, csharpCompilationOptions);

                var emitResult = cSharpCompilation.Emit(
                    $"{folderLocation}\\Game.exe",
                    pdbPath: $"{folderLocation}\\Game.pdb");

                if (!emitResult.Success)
                {
                    foreach (var error in emitResult.Diagnostics.Where(x => x.Severity == DiagnosticSeverity.Error))
                    {
                        var mappedLineSpan = error.Location.GetMappedLineSpan();

                        Engine.GameEngine.AddCompilerErrors(mappedLineSpan.Path, mappedLineSpan.StartLinePosition.Line, mappedLineSpan.StartLinePosition.Character, error.Id, error.ToString());
                    }

                    return false;
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Compiles a file looking for syntax and other errors
        /// </summary>
        /// <param name="file">The file to compile</param>
        /// <param name="errors">The output list of errors (if any)</param>
        /// <returns></returns>
        public static bool CompileFile(string file, out ICollection<object> errors)
        {
            errors = new List<object>();

            try
            {
                var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));

                foreach (var error in tree.GetDiagnostics().Where(x => x.Severity == DiagnosticSeverity.Error).ToList())
                {
                    var mappedLineSpan = error.Location.GetMappedLineSpan();

                    errors.Add($"{file} has had an error compiling. On line {mappedLineSpan.StartLinePosition.Line + 1} in column {mappedLineSpan.StartLinePosition.Character + 1}. The error code is {error.Id}. {error}");
                }
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the main game file with a new game project is selected
        /// </summary>
        /// <param name="folderLocation">The location for the new game files</param>
        /// <param name="gameName">The name of the game for the ini file</param>
        /// <returns></returns>
        public static bool CreateGameMainFiles(string folderLocation, string gameName)
        {
            try
            {
                var fileName = "main.cs";
                var fileContents = Resources.MainFile
                    .Replace("{0}", $"@\"{folderLocation}\\GameObjects.go\"")
                    .Replace("{1}", $"@\"{folderLocation}\\ErrorLog.txt\"")
                    .Replace("{2}", $"@\"{folderLocation}\\InformationLog.txt\"");
                var fullPath = $"{folderLocation}\\{fileName}";
                var settingsFileName = "DefaultSettings.ini";
                var settingsContent = JsonConvert.SerializeObject(new Settings
                {
                    GameName = gameName
                });

                File.AppendAllText(fullPath, fileContents);
                File.AppendAllText($"{folderLocation}\\{settingsFileName}", settingsContent);
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            return true;
        }

        // TODO - FIX
        public static bool GenerateCSharpFile(string folderLocation)
        {
            var fileName = "main.cs";
            var fileContents = Resources.MainFile
                .Replace("{0}", $"@\"{folderLocation}\\GameObjects.go\"")
                .Replace("{1}", $"@\"{folderLocation}\\ErrorLog.txt\"")
                .Replace("{2}", $"@\"{folderLocation}\\InformationLog.txt\"");
            var fullPath = $"{folderLocation}\\{fileName}";
            var engine = typeof(Engine).Assembly;
            var newtonsoft = typeof(DateFormatHandling).Assembly;
            var sharpDx = typeof(SharpDX.Collision).Assembly;
            var sharpDxMath = typeof(SharpDX.Mathematics.Interop.RawBool).Assembly;
            var sharpDx3D11 = typeof(SharpDX.Direct3D11.Asynchronous).Assembly;
            var sharpDxDXGL = typeof(SharpDX.DXGI.Adapter).Assembly;
            var shardDxCompiler = typeof(SharpDX.D3DCompiler.ShaderBytecode).Assembly;
            var systemCollections = typeof(System.Collections.IEnumerable).Assembly;
            var systemCollectionGenerics = typeof(System.Collections.Generic.IEnumerable<string>).Assembly;
            var linq = typeof(Enumerable).Assembly;

            try
            {
                DeleteMainFileIfExists(fullPath);

                File.AppendAllText(fullPath, fileContents);

                if (!Directory.Exists($"{folderLocation}\\Assets"))
                {
                    Directory.CreateDirectory($"{folderLocation}\\Assets");
                }

                foreach (var file in Directory.EnumerateFiles($"{folderLocation}\\Assets"))
                {
                    var fileInfo = new FileInfo(file);

                    File.Copy(file, $"{folderLocation}\\Assets\\{fileInfo.Name}", true);
                }

                File.Copy(engine.Location, $"{folderLocation}\\{engine.ManifestModule.Name}", true);
                File.Copy(newtonsoft.Location, $"{folderLocation}\\{newtonsoft.ManifestModule.Name}", true);
                File.Copy(sharpDx.Location, $"{folderLocation}\\{sharpDx.ManifestModule.Name}", true);
                File.Copy(sharpDxMath.Location, $"{folderLocation}\\{sharpDxMath.ManifestModule.Name}", true);
                File.Copy(sharpDx3D11.Location, $"{folderLocation}\\{sharpDx3D11.ManifestModule.Name}", true);
                File.Copy(sharpDxDXGL.Location, $"{folderLocation}\\{sharpDxDXGL.ManifestModule.Name}", true);
                File.Copy(shardDxCompiler.Location, $"{folderLocation}\\{shardDxCompiler.ManifestModule.Name}", true);
                File.Copy(systemCollections.Location, $"{folderLocation}\\{systemCollections.ManifestModule.Name}", true);
                File.Copy(systemCollectionGenerics.Location, $"{folderLocation}\\{systemCollectionGenerics.ManifestModule.Name}", true);
                File.Copy(linq.Location, $"{folderLocation}\\{linq.ManifestModule.Name}", true);
            }
            catch (Exception e)
            {
                Engine.GameEngine.AddException(e);

                return false;
            }

            return true;
        }

        private static void DeleteMainFileIfExists(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}