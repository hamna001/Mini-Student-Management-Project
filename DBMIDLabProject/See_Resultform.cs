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
    public partial class See_Resultform : Form
    {
        public See_Resultform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            try
            {
              
                int studentId = int.Parse(textBox1.Text);

              
                DataTable resultDataTable = new DataTable();

                SqlCommand cmd = new SqlCommand(
                    "SELECT sr.StudentId, sr.AssessmentComponentId, sr.RubricMeasurementId, sr.EvaluationDate, " +
                    "       ac.Name AS AssessmentComponentName,ac.totalmarks AS marks, r.details AS RubricDescription " +
                    "FROM StudentResult sr " +
                    "JOIN AssessmentComponent ac ON sr.AssessmentComponentId = ac.Id " +
                    "JOIN Rubric r ON sr.RubricMeasurementId = r.Id " +
                    "WHERE sr.StudentId = @StudentId;",
                    con);

                cmd.Parameters.AddWithValue("@StudentId", studentId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(resultDataTable);

                dataGridView1.DataSource = resultDataTable;

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
