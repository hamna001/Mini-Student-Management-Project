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
    public partial class Rubrics_Form : Form
    {
        public Rubrics_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            if (int.TryParse(textBox3.Text, out int cloIDForRubric))
            {

                SqlCommand cmdCheckCLO = new SqlCommand("SELECT COUNT(*) FROM CLO WHERE ID = @CLOID;", con);
                cmdCheckCLO.Parameters.AddWithValue("@CLOID", cloIDForRubric);


                int cloCount = Convert.ToInt32(cmdCheckCLO.ExecuteScalar());

                if (cloCount > 0)
                {
                  
                    SqlCommand cmdInsertRubric = new SqlCommand("INSERT INTO Rubric (id,Details, CLOID) VALUES (@id,@Details, @CLOID);", con);

                    cmdInsertRubric.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    cmdInsertRubric.Parameters.Add("@Details", SqlDbType.NVarChar).Value = textBox2.Text;
                    cmdInsertRubric.Parameters.AddWithValue("@CLOID", cloIDForRubric);

                    cmdInsertRubric.ExecuteNonQuery();
                    MessageBox.Show("Rubric added successfully for the CLO");
          
                }
                else
                {
                    MessageBox.Show("Invalid CLO ID. Please enter a CLO ID that exists in the CLO table.");
                }
            }
            else
            {
                MessageBox.Show("Invalid CLO ID. Please enter a valid integer ID.");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
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

            SqlCommand cmd = new SqlCommand("UPDATE Rubric SET Details = @Details  WHERE ID = @ID;", con);

            cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@Details", textBox2.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");
        }
    }
}
