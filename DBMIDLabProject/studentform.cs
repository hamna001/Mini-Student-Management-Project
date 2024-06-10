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
    public partial class studentform : Form
    {
        public studentform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();

         
            if (!int.TryParse(textBox7.Text, out int statusID) || (statusID != 1 && statusID != 2))
            {
                MessageBox.Show("Invalid status. Please enter 1 for Active or 2 for Inactive.");
                return;
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO Student (FirstName, LastName, Contact, Email, RegistrationNumber, Status) VALUES (@FirstName, @LastName, @Contact, @Email, @Reg_No, @Status)", con);

            cmd.Parameters.AddWithValue("@FirstName", textBox2.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox3.Text);
            cmd.Parameters.AddWithValue("@Contact", textBox4.Text);
            cmd.Parameters.AddWithValue("@Email", textBox5.Text);
            cmd.Parameters.AddWithValue("@Reg_No", textBox6.Text);
            cmd.Parameters.AddWithValue("@Status", statusID);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully saved");

            con.Close();





        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Studentform2 form = new Studentform2();
            form.Show();
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }

        private int GetStatusID(SqlConnection con, string statusName)
        {
            if (int.TryParse(statusName, out int statusID))
            {
                Console.WriteLine($"Parsed statusName: {statusName}, statusID: {statusID}");

                using (var cmd = new SqlCommand("SELECT LookupID FROM Lookup WHERE LookupID = @StatusID", con))
                {
                    cmd.Parameters.AddWithValue("@StatusID", statusID);
                    //con.Open();
                    var result = cmd.ExecuteScalar();
                  //  con.Close();

                    if (result != null)
                    {
                        Console.WriteLine($"Valid statusID found: {statusID}");
                        return Convert.ToInt32(result);
                    }
                }
            }

            Console.WriteLine($"Invalid statusName: {statusName}");
            // Return -1 if the statusName is not a valid integer or not found in the Lookup table
            return -1;
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            
        }
    }
}
