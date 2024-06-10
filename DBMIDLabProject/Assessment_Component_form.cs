using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMIDLabProject
{
    public partial class Assessment_Component_form : Form
    {
        public Assessment_Component_form()
        {
            InitializeComponent();
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();


            SqlCommand cmd = new SqlCommand("INSERT INTO AssessmentComponent (Name,Rubricid,TotalMarks,DateCreated,DateUpdated,Assessmentid) VALUES (@Name,@Rubricid,@TotalMarks,GetDate(),0,@Assessmentid);", con);

         //   cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@Rubricid", int.Parse(textBox3.Text));
            cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(textBox4.Text));
            cmd.Parameters.AddWithValue("@Assessmentid", int.Parse(textBox5.Text));
            

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully inserted");
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "DECLARE @ObtainedRubricLevel INT; " +
                    "DECLARE @MaxRubricLevel INT; " +
                    "DECLARE @ComponentMarks FLOAT; " +
                    "SELECT @ObtainedRubricLevel = CASE " +
                    "    WHEN @TotalMarks > 70 THEN 3 " +
                    "    WHEN @TotalMarks > 40 THEN 2 " +
                    "    ELSE 1 " +
                    "END; " +
                    "SELECT @MaxRubricLevel = MAX(id) FROM Rubric; " +
                    "SELECT @ComponentMarks = (@ObtainedRubricLevel * 1.0 / @MaxRubricLevel) * @TotalMarks; " +
                    "INSERT INTO AssessmentComponent (Name, Rubricid, TotalMarks, DateCreated, DateUpdated, Assessmentid) " +
                    "VALUES (@Name, @ObtainedRubricLevel, @ComponentMarks, GETDATE(), GETDATE(), @Assessmentid); ",
                    con);

                cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@Assessmentid", int.Parse(textBox5.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully inserted with calculated TotalMarks");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }









        }
    }
}
