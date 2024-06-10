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
    public partial class Rubric_Level_Form : Form
    {
        public Rubric_Level_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (int.TryParse(textBox2.Text, out int rubricID))
            {
            
                string details = textBox3.Text;

                if (int.TryParse(textBox4.Text, out int measurementLevel))
                {
                    SqlCommand cmdInsertRubricLevel = new SqlCommand("INSERT INTO RubricLevel (RubricID, Details, MeasurementLevel) VALUES (@RubricID, @Details, @MeasurementLevel);", con);

                    cmdInsertRubricLevel.Parameters.AddWithValue("@RubricID", rubricID);
                    cmdInsertRubricLevel.Parameters.AddWithValue("@Details", details);
                    cmdInsertRubricLevel.Parameters.AddWithValue("@MeasurementLevel", measurementLevel);

                    //con.Open();
                    cmdInsertRubricLevel.ExecuteNonQuery();
                    MessageBox.Show("Rubric level added successfully for the Rubric");
                   // con.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Measurement Level. Please enter a valid integer level.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Rubric ID. Please enter a valid integer ID.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubriclevel", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            dataGridView1.Rows.Remove(selectedRow);

            MessageBox.Show("Successfully deleted");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            SqlCommand cmd = new SqlCommand("UPDATE RubricLevel SET Details = @Details,MeasurementLevel = @MeasurementLevel  WHERE ID = @ID;", con);

            cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@Details", textBox3.Text);
            cmd.Parameters.AddWithValue("@MeasurementLevel", int.Parse(textBox4.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");
        }
    }
}
