using System;
using System.IO;
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
                    string[] file1 = File.ReadAllLines(textBox1.Text);
                    string[] file2 = File.ReadAllLines(textBox2.Text);
                    string[] outFile = new string[file1.Length + file2.Length];
                    file1.CopyTo(outFile, 0);
                    file2.CopyTo(outFile, file1.Length);
                    Array.Sort(outFile);
                    if(textBox3.Text == ""){
                        File.WriteAllLines("MergedFile.csv", outFile);
                    }else{
                        File.WriteAllLines(textBox3.Text + ".csv", outFile);
                    }
                    label3.Text = "OK!";
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
