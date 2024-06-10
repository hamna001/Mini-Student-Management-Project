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
    public partial class TeacherForm : Form
    {
        public TeacherForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Studentform2 form = new Studentform2();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CLO_form form = new CLO_form();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rubrics_Form form = new Rubrics_Form();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Rubric_Level_Form form = new Rubric_Level_Form();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            All_Attendence_form form = new All_Attendence_form();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Asessment_form form = new Asessment_form();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Assessment_Component_form form = new Assessment_Component_form();
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Result_forms form = new Result_forms();
            form.Show();
        }
    }
}
