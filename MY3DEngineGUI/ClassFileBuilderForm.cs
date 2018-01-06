using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MY3DEngineGUI
{
    public partial class ClassFileBuilderForm : Form
    {
        const int padding = 2;

        private int baseMaxLineNumberCharLength;
        private string folderLocation;

        public ClassFileBuilderForm()
        {
            InitializeComponent();

            this.scintilla1.Lexer = Lexer.Cpp;

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            this.scintilla1.StyleResetDefault();
            this.scintilla1.Styles[Style.Default].Font = "Consolas";
            this.scintilla1.Styles[Style.Default].Size = 10;
            this.scintilla1.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            this.scintilla1.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            this.scintilla1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            this.scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            this.scintilla1.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            this.scintilla1.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            this.scintilla1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            this.scintilla1.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            this.scintilla1.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            this.scintilla1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            this.scintilla1.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            this.scintilla1.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            this.scintilla1.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            this.scintilla1.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;

            this.scintilla1.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            this.scintilla1.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
        }

        private void Scintilla1_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = this.scintilla1.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.baseMaxLineNumberCharLength)
            {
                return;
            }

            // Calculate the width required to display the last line number and include some padding for good measure.
            this.scintilla1.Margins[0].Width = this.scintilla1.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.baseMaxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void Scintilla1_Insert(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
            {
                this.UpdateLineNumbers(this.scintilla1.LineFromPosition(e.Position));
            }
        }

        private void Scintilla1_Delete(object sender, ModificationEventArgs e)
        {
            // Only update line numbers if the number of lines changed
            if (e.LinesAdded != 0)
            {
                this.UpdateLineNumbers(this.scintilla1.LineFromPosition(e.Position));
            }
        }

        private void Scintilla1_CharAdded(object sender, CharAddedEventArgs e)
        {
            // Find the word start
            var currentPos = this.scintilla1.CurrentPosition;
            var wordStartPos = this.scintilla1.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                if (!this.scintilla1.AutoCActive)
                {
                    this.scintilla1.AutoCShow(lenEntered, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.folderLocation))
            {
                this.AddToInformationDisplay("Must have save location selected in order to add class files");
            }

            File.AppendAllText($"{folderLocation}\\", this.scintilla1.Text);
        }

        

        #region Helpers

        private void UpdateLineNumbers(int startingAtLine)
        {
            // Starting at the specified line index, update each
            // subsequent line margin text with a hex line number.
            for (int i = startingAtLine; i < this.scintilla1.Lines.Count; i++)
            {
                this.scintilla1.Lines[i].MarginStyle = Style.LineNumber;
                this.scintilla1.Lines[i].MarginText = "0x" + i.ToString("X2");
            }
        }

        private void AddToInformationDisplay(string message)
        {
            this.tbInformation.AppendText($"{message} {Environment.NewLine}");
        }

        #endregion

    }
}
