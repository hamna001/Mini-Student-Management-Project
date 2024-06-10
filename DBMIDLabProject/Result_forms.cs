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
    public partial class Result_forms : Form
    {
        public Result_forms()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            try
            {

                int assessmentComponentId = int.Parse(textBox2.Text);
                int rubricMeasurementId = int.Parse(textBox3.Text);

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO StudentResult (StudentId, AssessmentComponentId, RubricMeasurementId, EvaluationDate) " +
                    "VALUES (@StudentId, @AssessmentComponentId, @RubricMeasurementId, GETDATE());",
                    con);

                cmd.Parameters.AddWithValue("@StudentId", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentComponentId);
                cmd.Parameters.AddWithValue("@RubricMeasurementId", rubricMeasurementId);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully inserted into StudentResult");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

        }
    }
}
