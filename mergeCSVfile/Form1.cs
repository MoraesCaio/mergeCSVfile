using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace mergeCSVfile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = Application.StartupPath;
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = openFileDialog2.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(File.Exists(textBox1.Text) && File.Exists(textBox2.Text))
            {
                try
                {
                    saveFileDialog1.FileName = Path.GetFileName(textBox1.Text);
                    saveFileDialog1.DefaultExt = Path.GetExtension(textBox1.Text);

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string[] file1 = File.ReadAllLines(textBox1.Text);
                        string[] file2 = File.ReadAllLines(textBox2.Text);
                        string[] outFile = new string[file1.Length + file2.Length];

                        file1.CopyTo(outFile, 0);
                        file2.CopyTo(outFile, file1.Length);
                        Array.Sort(outFile);

                        outFile = outFile.Distinct(StringComparer.OrdinalIgnoreCase).ToArray();

                        File.WriteAllLines(saveFileDialog1.FileName, outFile);

                        label3.Text = "OK!";
                    }
                }
                catch(Exception ex)
                {
                    label3.Text = "ERRO!";
                    MessageBox.Show(ex.Message,
                        "Warning!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Select two valid files.", 
                        "Warning!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
            }
        }
    }
}
