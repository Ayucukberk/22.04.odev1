using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _22._04_odev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKlasorSecVeClasslariListele_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBoxDosyalar.Items.Clear();
                string selectedPath = dialog.SelectedPath;
                KlasorleriVeClasslariListele(selectedPath);
            }
        }

        private void KlasorleriVeClasslariListele(string rootPath)
        {
            foreach (var dir in Directory.GetDirectories(rootPath))
            {
                string klasorAdi = "📁 " + Path.GetFileName(dir);
                listBoxDosyalar.Items.Add(klasorAdi);

                foreach (var file in Directory.GetFiles(dir, "*.cs"))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        if (line.Trim().StartsWith("class "))
                        {
                            string className = Regex.Match(line, @"class (\w+)").Groups[1].Value;
                            listBoxDosyalar.Items.Add("   📄 " + className);
                        }
                    }
                }
            }
        }
    }
}
