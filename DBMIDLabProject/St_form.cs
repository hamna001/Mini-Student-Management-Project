using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMIDLabProject
{
    public partial class St_form : Form
    {
        public St_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            studentform form = new studentform();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            st_Attendance form = new st_Attendance();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SeeCLOFORM form = new SeeCLOFORM();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            See_Resultform form = new See_Resultform();
            form.Show();
        }
    }
}
