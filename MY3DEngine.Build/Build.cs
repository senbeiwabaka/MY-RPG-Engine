using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace MY3DEngine.Build
{
    public static class Build
    {
        public static bool GenerateCSharpFile(string folderLocation)
        {
            Contract.Requires(string.IsNullOrEmpty(folderLocation) == false);

            var fileName = "main.cs";
            var contents = @"using MY3DEngine;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class MainWindow : Form
	{
		public MainWindow()
		{
			this.ClientSize = new System.Drawing.Size(970, 639);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

            var graphicsException = new ExceptionData(" + "\"Engine Graphics not setup correctly\", \"Engine\", string.Empty);" + @"

            Engine.IsDebugginTurnedOn = true;

            try
			{
				if (Engine.GameEngine.InitliazeGraphics(
					this.Handle,
					this.ClientSize.Width,
					this.ClientSize.Height,
					false,
					false))
				{
					Engine.GameEngine.Initialize(this.Handle);

					Engine.GameEngine.Load({0});

                    this.ListExceptions(Engine.GameEngine.Exception.Exceptions.ToList());
                }
                else
                {
                    System.IO.File.AppendAllText({1}, DateTime.Now + Environment.NewLine);
					System.IO.File.AppendAllText({1}, graphicsException.ToString() + Environment.NewLine + Environment.NewLine);
                }
            }
			catch(Exception e)
			{
                System.IO.File.AppendAllText({1}, DateTime.Now + Environment.NewLine);
				System.IO.File.AppendAllText({1}, e.Message + Environment.NewLine);
				System.IO.File.AppendAllText({1}, e.StackTrace + Environment.NewLine + Environment.NewLine);
			}
		}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDown();
        }

        private void ListExceptions(IEnumerable<ExceptionData> exceptions)
        {
			if(exceptions.Count() > 0)
			{
				System.IO.File.AppendAllText({1}, DateTime.Now + Environment.NewLine);
            }

            foreach (var exception in exceptions)
            {
				System.IO.File.AppendAllText({1}, exception.ToString() + Environment.NewLine);
            }

			if(exceptions.Count() > 0)
			{
				System.IO.File.AppendAllText({1}, Environment.NewLine + Environment.NewLine);
			}
        }

        private static void ShutDown()
        {
            MY3DEngine.Engine.GameEngine?.Shutdown();

            MY3DEngine.Engine.GameEngine?.Dispose();
        }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Set the unhandled exception mode to force all Windows Forms errors
			// to go through our handler.
			//Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

			//AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			// Add the event handler for handling UI thread exceptions to the event.
			//Application.ThreadException += Application_ThreadException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}

		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			var exception = e.Exception;

			MY3DEngine.Engine.GameEngine.AddException(exception);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = e.ExceptionObject as Exception;

			MY3DEngine.Engine.GameEngine.AddException(exception);
		}
	}
}";
            var fileContents = contents
                .Replace("{0}", $"@\"{folderLocation}\\GameObjects.go\"")
                .Replace("{1}", $"@\"{folderLocation}\\ErrorLog.txt\"")
                .Replace("{2}", $"@\"{folderLocation}\\InformationLog.txt\"");
            var fullPath = $"{folderLocation}\\{fileName}";
            var engine = typeof(Engine).Assembly;
            var newtonsoft = typeof(Newtonsoft.Json.DateFormatHandling).Assembly;
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

                if(!Directory.Exists($"{folderLocation}\\Shaders"))
                {
                    Directory.CreateDirectory($"{folderLocation}\\Shaders");
                }

                File.Copy("Shaders\\Triangle.fx", $"{folderLocation}\\Shaders\\Triangle.fx", true);

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
                var cSharpCompilation = CSharpCompilation.Create("test", new[] { tree }, references, csharpCompilationOptions);

                var emitResult = cSharpCompilation.Emit(
                    $"{folderLocation}\\Game.exe",
                    pdbPath: $"{folderLocation}\\Game.pdb");

                DeleteMainFileIfExists(fullPath);

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

        private static void DeleteMainFileIfExists(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}