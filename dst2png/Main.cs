using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dst2png
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                String filename = openFileDialog1.FileName;
                byte[] file = System.IO.File.ReadAllBytes(filename);

                DSTConvert convert = new DSTConvert(file);
                convert.ToPNG(filename.Substring(0, filename.Length - 4) + ".png");

                MessageBox.Show("DST File Converted and saved to: " + filename.Substring(0, filename.Length - 4) + ".png");
            }

        }
    }
}
