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
    public partial class CLO_form : Form
    {
        public CLO_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

            
                SqlCommand cmd = new SqlCommand("INSERT INTO CLO (Name,DateCreated,DateUpdated) VALUES (@Name,GetDate(),0);", con);

                cmd.Parameters.AddWithValue("@Name", textBox1.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully inserted");
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from CLO", con);
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

            SqlCommand cmd = new SqlCommand("UPDATE CLO SET Name = @Name, DateUpdated = GETDATE() WHERE ID = @ID;", con);

            cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated");

               
          
        }
    }
}
