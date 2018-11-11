namespace MY3DEngine.BuildTools
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using MY3DEngine.BuildTools.Properties;
    using MY3DEngine.Logging;
    using MY3DEngine.Utilities;
    using MY3DEngine.Utilities.Interfaces;

    // TODO - REFACTOR/FIX
    public static class Build
    {
        /// <summary>
        /// Builds the game
        /// </summary>
        /// <param name="folderLocation">
        /// The folder location with the files to build and where the .exe goes
        /// </param>
        /// <param name="gameName">The name of the game</param>
        /// <param name="fileIo"></param>
        /// <returns></returns>
        public static bool BuildGame(string folderLocation, string gameName, IFileIO fileIo)
        {
            StaticLogger.Info($"Starting {nameof(Build)}.{nameof(BuildGame)}");

            if (string.IsNullOrWhiteSpace(folderLocation))
            {
                StaticLogger.Info($"Argument: {nameof(folderLocation)} was not supplied");

                throw new ArgumentNullException(nameof(folderLocation));
            }

            if (string.IsNullOrWhiteSpace(gameName))
            {
                StaticLogger.Info($"Argument: {nameof(gameName)} was not supplied");

                throw new ArgumentNullException(nameof(gameName));
            }

            if (fileIo == null)
            {
                throw new ArgumentNullException(nameof(fileIo));
            }

            var buildSuccessful = false;

            try
            {
                var fileName = Constants.MainFileName;
                var fullPath = $"{folderLocation}\\{fileName}";
                var fileContents = fileIo.GetFileContent(fullPath);
                var assemblies = AssemblyHelper.GetAssemblies(fileIo);
                var references = new List<MetadataReference>(assemblies.Count);

                foreach (var assembly in assemblies)
                {
                    references.Add(MetadataReference.CreateFromFile(assembly.Location));
                }

                var tree = CSharpSyntaxTree.ParseText(fileContents);

                //var root = (CompilationUnitSyntax)tree.GetRoot();
                //var usings = root.Usings;

                //foreach (var use in usings)
                //{
                //    var usingName = use.Name.ToString();

                // if (usingName == "MY3DEngine" || usingName == "MY3DEngine.Models" || usingName ==
                // "System") continue;

                // var assembly = System.Reflection.Assembly.Load(usingName);

                //    references.Add(MetadataReference.CreateFromFile(assembly.Location));
                //}

                //The location of the .NET assemblies
                var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

                /*
                    * Adding some necessary .NET assemblies
                    * These assemblies couldn't be loaded correctly via the same construction as above,
                    * in specific the System.Runtime.
                    */
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll")));
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.dll")));
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Core.dll")));
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll")));
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Windows.Forms.dll")));
                references.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Drawing.dll")));

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

                        StaticLogger.Info($"{mappedLineSpan.Path}; {mappedLineSpan.StartLinePosition.Line}; {mappedLineSpan.StartLinePosition.Character}; {error.Id}; {error}");
                    }

                    return buildSuccessful;
                }

                buildSuccessful = true;
            }
            catch (Exception e)
            {
                StaticLogger.Exception(nameof(BuildGame), e);

                buildSuccessful = false;
            }

            StaticLogger.Info($"Finished {nameof(Build)}.{nameof(BuildGame)}");

            return buildSuccessful;
        }

        /// <summary>
        /// Compiles a file looking for syntax and other errors
        /// </summary>
        /// <param name="file">The file to compile</param>
        /// <param name="errors">The output list of errors (if any)</param>
        /// <returns></returns>
        public static bool CompileFile(string file, out ICollection<object> errors)
        {
            StaticLogger.Info($"Starting {nameof(CompileFile)}");

            errors = new List<object>();

            try
            {
                var tree = CSharpSyntaxTree.ParseText(
                    File.ReadAllText(file),
                    new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.Diagnose, SourceCodeKind.Regular));

                var root = tree.GetRoot();

                foreach (var error in tree.GetDiagnostics().Where(x => x.Severity == DiagnosticSeverity.Error))
                {
                    var mappedLineSpan = error.Location.GetMappedLineSpan();

                    errors.Add($"{file} has had an error compiling. On line {mappedLineSpan.StartLinePosition.Line + 1} in column {mappedLineSpan.StartLinePosition.Character + 1}. The error code is {error.Id}. {error}");
                }
            }
            catch (Exception e)
            {
                StaticLogger.Exception(nameof(CompileFile), e);

                return false;
            }

            StaticLogger.Info($"Finished {nameof(CompileFile)}");

            return true;
        }

        public static bool GenerateFilesForBuildingGame(string folderLocation, IFileIO fileIo)
        {
            StaticLogger.Info($"Starting {nameof(Build)}.{nameof(GenerateFilesForBuildingGame)}");

            var mainFileContents = Resources.MainFile
                .Replace("{0}", $"@\"{folderLocation}\\GameObjects.go\"")
                .Replace("{1}", $"@\"{folderLocation}\\ErrorLog.txt\"")
                .Replace("{2}", $"@\"{folderLocation}\\InformationLog.txt\"")
                .Replace("{ScreenWidth}", "800")
                .Replace("{ScreenHeight}", "400");
            var loggerFileContents = Resources.LoggerFile;
            var mainFileFullPath = $"{folderLocation}\\{Constants.MainFileName}";
            var loggerFileFullPath = $"{folderLocation}\\{Constants.LoggerFileName}";
            var assemblies = AssemblyHelper.GetAssemblies(fileIo);

            try
            {
                if (!fileIo.FileExists(mainFileFullPath))
                {
                    fileIo.WriteFileContent(mainFileFullPath, mainFileContents);
                }

                if (!fileIo.FileExists(loggerFileFullPath))
                {
                    fileIo.WriteFileContent(loggerFileFullPath, loggerFileContents);
                }

                if (!fileIo.DirectoryExists($"{folderLocation}\\Assets"))
                {
                    fileIo.CreateDirectory($"{folderLocation}\\Assets");
                }

                foreach (var file in Directory.EnumerateFiles($"{folderLocation}\\Assets"))
                {
                    var fileInfo = new FileInfo(file);

                    fileIo.CopyFile(file, $"{folderLocation}\\Assets\\{fileInfo.Name}", true);
                }

                foreach (var item in assemblies)
                {
                    fileIo.CopyFile(item.Location, $"{folderLocation}\\{item.ManifestModule.Name}", true);
                }
            }
            catch (Exception e)
            {
                StaticLogger.Exception($"{nameof(Build)}.nameof{nameof(GenerateFilesForBuildingGame)}", e);

                return false;
            }

            StaticLogger.Info($"Finished {nameof(Build)}.{nameof(GenerateFilesForBuildingGame)}");

            return true;
        }
    }
}
