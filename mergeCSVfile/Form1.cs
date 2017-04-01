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

        private List<string> GetDuplicates(string[] str1)
        {
            List<string> duplicates = new List<string>();
            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = i + 1; j < str1.Length; j++)
                {
                    if(String.Compare(str1[i], str1[j], true) == 0)
                    {
                        duplicates.Add(str1[i]);
                    }
                }
            }
            return duplicates;
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
                        string[] baseFile = File.ReadAllLines(textBox1.Text);
                        string[] addtionalContentFile = File.ReadAllLines(textBox2.Text);
                        string[] outFile = new string[baseFile.Length + addtionalContentFile.Length];

                        //Merging & Sorting
                        baseFile.CopyTo(outFile, 0);
                        addtionalContentFile.CopyTo(outFile, baseFile.Length);
                        Array.Sort(outFile);

                        //DUPLICATES HANDLING
                        //Saving occurrences
                        string[] duplicates = GetDuplicates(outFile).ToArray();

                        if (duplicates.Length > 0)
                        {
                            saveFileDialog2.FileName = "duplicates.txt";
                            saveFileDialog2.DefaultExt = ".txt";

                            //Prompt about duplicates
                            DialogResult dr = MessageBox.Show("There were some duplicated lines on the files.\n"+
                                                "Click OK to save them on another file or Cancel to continue without save.",
                                                "Warning!",
                                                MessageBoxButtons.OKCancel,
                                                MessageBoxIcon.Exclamation,
                                                MessageBoxDefaultButton.Button1);
                            if (dr == DialogResult.OK)
                            {
                                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                                {
                                    File.WriteAllLines(saveFileDialog2.FileName, duplicates);
                                }
                                else
                                {
                                    //Cancels whole process
                                    return;
                                }
                            }
                        }

                        //Removing duplicates
                        outFile = outFile.Distinct(StringComparer.OrdinalIgnoreCase).ToArray();

                        //Saving output file
                        File.WriteAllLines(saveFileDialog1.FileName, outFile);

                        label3.Text = "OK!";
                    }
                }
                catch(Exception ex)
                {
                    label3.Text = "ERROR!";
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
