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

namespace Licenta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node.Text == "Variabila noua")
            {
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
            }
            else if (node.Text == "Seteaza variabila")
            {
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                groupBox4.Visible = false;
            }
            else if(node.Text == "If")
            {
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                groupBox4.Visible = false;
            }
            else if (node.Text == "Else")
            {
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                groupBox4.Visible = false;
            }
            else if (node.Text == "Return")
            {
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                groupBox4.Visible = false;
            }
            else if (node.Text == "While")
            {
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = true;
            }
            else if (node.Text == "For")
            {
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox4.Visible = true;
            }
        }
    }
}
