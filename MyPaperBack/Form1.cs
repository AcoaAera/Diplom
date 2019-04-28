using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyPaperBack
{
    public partial class Form1 : Form
    {
        byte[] myArray;
        List<List<int>> mess;

        public Form1()
        {
            InitializeComponent();
            byte[] x = fieldGalua.Table0;
            mess = new List<List<int>>();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lblpath.Text = openFileDialog1.FileName;
                myArray = File.ReadAllBytes(@"" + openFileDialog1.FileName + "");
                MessageBox.Show("Файл выбран");
            }
            else
                MessageBox.Show("Файл не выбран");
        }

        private void btnEncoding_Click(object sender, EventArgs e)
        {
            Encoding encoding = new Encoding((int)numericUpDown1.Value);
            mess = encoding.startEncoding(MyMath.ConvertToList(myArray));
        }

        private void btnDecoding_Click(object sender, EventArgs e)
        {

        }
    }
}
