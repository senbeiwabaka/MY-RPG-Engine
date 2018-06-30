﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MY3DEngine.Build.Properties;
using MY3DEngine.Logging;
using MY3DEngine.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MY3DEngine.Build
{
    // TODO - REFACTOR/FIX
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
            StaticLogger.Info($"Starting Method: {nameof(BuildGame)}");

            var buildSuccessful = false;

            if (string.IsNullOrWhiteSpace(folderLocation))
            {
                StaticLogger.Info($"Argument: {nameof(folderLocation)} was not supplied");

                return buildSuccessful;
            }

            if (string.IsNullOrWhiteSpace(gameName))
            {
                StaticLogger.Info($"Argument: {nameof(gameName)} was not supplied");

                return buildSuccessful;
            }

            try
            {
                var fileName = Constants.MainFileName;
                var fullPath = $"{folderLocation}\\{fileName}";
                var fileContents = FileIO.GetFileContent(fullPath);
                var assemblies = AssemblyHelper.GetAssemblies();
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

                //    if (usingName == "MY3DEngine" || usingName == "MY3DEngine.Models" || usingName == "System")
                //        continue;

                //    var assembly = System.Reflection.Assembly.Load(usingName);

                //    references.Add(MetadataReference.CreateFromFile(assembly.Location));
                //}

                var systemAssembly = System.Reflection.Assembly.Load("mscorlib");
                //var systemWindowsFormAssembly = System.Reflection.Assembly.LoadFrom(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7\System.Windows.Forms.dll");

                references.Add(MetadataReference.CreateFromFile(systemAssembly.Location));
                //references.Add(MetadataReference.CreateFromFile(systemWindowsFormAssembly.Location));
                references.Add(MetadataReference.CreateFromFile(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Windows.Forms.dll"));

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

                    buildSuccessful = false;
                }
            }
            catch (Exception e)
            {
                StaticLogger.Exception(nameof(BuildGame), e);

                buildSuccessful = false;
            }

            StaticLogger.Info($"Finished Method: {nameof(BuildGame)}");

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

        // TODO - Finish using new FileIO from Utilities
        public static bool GenerateFilesForBuildingGame(string folderLocation)
        {
            StaticLogger.Info($"Starting Method: {nameof(GenerateFilesForBuildingGame)}");

            var fileName = Constants.MainFileName;
            var fileContents = Resources.MainFile
                .Replace("{0}", $"@\"{folderLocation}\\GameObjects.go\"")
                .Replace("{1}", $"@\"{folderLocation}\\ErrorLog.txt\"")
                .Replace("{2}", $"@\"{folderLocation}\\InformationLog.txt\"")
                .Replace("{ScreenWidth}", "800")
                .Replace("{ScreenHeight}", "400");
            var fullPath = $"{folderLocation}\\{fileName}";
            var assemblies = AssemblyHelper.GetAssemblies();

            try
            {
                if (!FileIO.FileExists(fullPath))
                {
                    FileIO.WriteFileContent(fullPath, fileContents);
                }

                if (!FileIO.DirectoryExists($"{folderLocation}\\Assets"))
                {
                    FileIO.CreateDirectory($"{folderLocation}\\Assets");
                }

                foreach (var file in Directory.EnumerateFiles($"{folderLocation}\\Assets"))
                {
                    var fileInfo = new FileInfo(file);

                    FileIO.CopyFile(file, $"{folderLocation}\\Assets\\{fileInfo.Name}", true);
                }

                foreach (var item in assemblies)
                {
                    FileIO.CopyFile(item.Location, $"{folderLocation}\\{item.ManifestModule.Name}", true);
                }
            }
            catch (Exception e)
            {
                StaticLogger.Exception(nameof(GenerateFilesForBuildingGame), e);

                return false;
            }

            StaticLogger.Info($"Finished {nameof(GenerateFilesForBuildingGame)}");

            return true;
        }
    }
}
