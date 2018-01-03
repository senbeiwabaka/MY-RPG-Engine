using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;

namespace MY3DEngine.Build
{
    public class Build
    {
        public static bool GenerateCSharpFile(string folderLocation)
        {
            Contract.Requires(string.IsNullOrEmpty(folderLocation) == false);

            var fileName = "main.cs";
            var contents = @"using MY3DEngine;
using System;
using System.Windows.Forms;

namespace Game
{
	public class MainWindow : Form
	{
		public MainWindow()
		{
			this.ClientSize = new System.Drawing.Size(970, 639);
            var graphicsException = new ExceptionData(" + "\"Engine Graphics not setup correctly\", \"Engine\", string.Empty);" + @"

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

            try
            {
                DeleteMainFileIfExists(fullPath);

                File.AppendAllText(fullPath, fileContents);

                File.Copy(engine.Location, $"{folderLocation}\\{engine.ManifestModule.Name}", true);
                File.Copy(newtonsoft.Location, $"{folderLocation}\\{newtonsoft.ManifestModule.Name}", true);
                File.Copy(sharpDx.Location, $"{folderLocation}\\{sharpDx.ManifestModule.Name}", true);
                File.Copy(sharpDxMath.Location, $"{folderLocation}\\{sharpDxMath.ManifestModule.Name}", true);
                File.Copy(sharpDx3D11.Location, $"{folderLocation}\\{sharpDx3D11.ManifestModule.Name}", true);
                File.Copy(sharpDxDXGL.Location, $"{folderLocation}\\{sharpDxDXGL.ManifestModule.Name}", true);

                var options = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
                var codeProvider = CodeDomProvider.CreateProvider("CSharp", options);
                var compilerParameters = new CompilerParameters
                {
                    GenerateExecutable = true,
                    GenerateInMemory = false,
                    CompilerOptions = "/debug /target:winexe",
                    OutputAssembly = $"{folderLocation}\\Game.exe",
                    IncludeDebugInformation = true,

                };

                compilerParameters.ReferencedAssemblies.Add(typeof(System.Windows.Forms.Form).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(typeof(System.Windows.Forms.Panel).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(typeof(System.Threading.ThreadExceptionEventArgs).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(typeof(System.ComponentModel.ComponentCollection).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(typeof(System.Drawing.Size).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(engine.Location);

                var compilerResults = codeProvider.CompileAssemblyFromFile(compilerParameters, fullPath);

                //DeleteMainFileIfExists(fullPath);

                if (compilerResults.Errors.HasErrors)
                {
                    foreach (var error in compilerResults.Errors)
                    {
                        var compileError = error as CompilerError;

                        Engine.GameEngine.AddCompilerErrors(compileError.FileName, compileError.Line, compileError.Column, compileError.ErrorNumber, compileError.ErrorText);
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
