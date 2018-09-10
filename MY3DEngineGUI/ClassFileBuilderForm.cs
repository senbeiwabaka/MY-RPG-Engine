using MY3DEngine.BuildTools;
using NLog;
using ScintillaNET;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MY3DEngine.GUI
{
    public partial class ClassFileBuilderForm : Form
    {
        private const int padding = 2;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string fileName;
        private readonly string folder;
        private int baseMaxLineNumberCharLength;

        public ClassFileBuilderForm(string fileName, string folder = default(string))
            : this()
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            this.fileName = fileName;
            this.folder = folder;

            if (!string.IsNullOrWhiteSpace(this.folder))
            {
                scintilla1.Text = File.ReadAllText($"{this.folder}\\{this.fileName}");

                Text = $"{fileName} - Class File";
            }
        }

        private ClassFileBuilderForm()
        {
            InitializeComponent();

            scintilla1.Lexer = Lexer.Cpp;

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Consolas";
            scintilla1.Styles[Style.Default].Size = 10;
            scintilla1.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            scintilla1.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla1.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla1.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla1.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;

            scintilla1.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            scintilla1.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");

            //scintilla1.AssignCmdKey(Keys.Control | Keys.S, Command.s)
        }

        #region Events

        private void ClassFileBuilderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile();
        }

        private void CompileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();

            if (!Build.CompileFile($"{folder}\\{fileName}", out var errors))
            {
                AddToInformationDisplay("Compile Failed");
            }
            else
            {
                foreach (var error in errors)
                {
                    AddToInformationDisplay(error.ToString());
                }
            }

            if (errors.Count == 0)
            {
                AddToInformationDisplay("No errors found");
            }
        }

        private void Scintilla1_CharAdded(object sender, CharAddedEventArgs e)
        {
            // Find the word start
            var currentPos = scintilla1.CurrentPosition;
            var wordStartPos = scintilla1.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                if (!scintilla1.AutoCActive)
                {
                    scintilla1.AutoCShow(lenEntered, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
                }
            }
        }

        private void Scintilla1_Delete(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
            {
                UpdateLineNumbers(scintilla1.LineFromPosition(e.Position));
            }
        }

        private void Scintilla1_Insert(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
            {
                UpdateLineNumbers(scintilla1.LineFromPosition(e.Position));
            }
        }

        private void Scintilla1_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla1.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == baseMaxLineNumberCharLength)
            {
                return;
            }

            // Calculate the width required to display the last line number and include some padding for good measure.
            scintilla1.Margins[0].Width = scintilla1.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            baseMaxLineNumberCharLength = maxLineNumberCharLength;
        }

        #endregion Events

        #region Menu Events

        private void ClearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbInformation.Text = string.Empty;
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();

            Close();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        #endregion Menu Events

        #region Helpers

        private void AddToInformationDisplay(string message)
        {
            tbInformation.AppendText($"{message} {Environment.NewLine}");
        }

        private void SaveFile()
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                AddToInformationDisplay("Must have save location selected in order to add class files");

                return;
            }

            var fullPath = !fileName.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase) ? $"{fileName}.cs" : fileName;

            try
            {
                if (string.IsNullOrWhiteSpace(folder))
                {
                    File.WriteAllText($"{Engine.GameEngine.SettingsManager.Settings.MainFolderLocation}\\{fullPath}", scintilla1.Text);
                }
                else
                {
                    File.WriteAllText($"{folder}\\{fullPath}", scintilla1.Text);
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);

                AddToInformationDisplay($"Message: {exception.Message} {Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }

        private void UpdateLineNumbers(int startingAtLine)
        {
            // Starting at the specified line index, update each
            // subsequent line margin text with a hex line number.
            for (int i = startingAtLine; i < scintilla1.Lines.Count; i++)
            {
                scintilla1.Lines[i].MarginStyle = Style.LineNumber;
                scintilla1.Lines[i].MarginText = "0x" + i.ToString("X2");
            }
        }

        #endregion Helpers

        private void ClassFileBuilderForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            AddToInformationDisplay($"Key Pressed: {e.KeyChar}");
        }

        private void Scintilla1_KeyPress(object sender, KeyPressEventArgs e)
        {
            AddToInformationDisplay($"Key Pressed: {e.KeyChar}");
        }

        private void Scintilla1_SavePointLeft(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }
    }
}
