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
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            St_form form = new St_form();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TeacherForm form = new TeacherForm();
            form.Show();
        }
    }
}
