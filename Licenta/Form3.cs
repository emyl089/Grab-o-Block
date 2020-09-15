using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Licenta
{
    public partial class Form3 : Form
    {
        int cnt = 0;
        public Form3()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cnt++;
            if (cnt == 30)
                pictureBox1.Image = Image.FromFile("C:/Users/emyl_/Documents/Proiect/Licenta 2.0/Licenta/Licenta/Images/done.png");
            if (cnt == 50)
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
