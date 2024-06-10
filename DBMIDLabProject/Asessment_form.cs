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
    public partial class Asessment_form : Form
    {
        public Asessment_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Assessment (title, DateCreated, totalmarks, totalweightage) " +
                                                "VALUES (@title, GETDATE(), @totalmarks, @totalweightage)", con);

                cmd.Parameters.AddWithValue("@title", textBox2.Text);
                cmd.Parameters.AddWithValue("@totalmarks", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@totalweightage", int.Parse(textBox5.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close(); 
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
