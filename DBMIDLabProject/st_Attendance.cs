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
    public partial class st_Attendance : Form
    {
        public st_Attendance()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("AttendanceID", "Attendance ID");
            dataGridView1.Columns.Add("AttendanceDate", "Attendance Date");
            dataGridView1.Columns.Add("AttendanceStatus", "Attendance Status");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Clear();

        
            if (int.TryParse(textBox1.Text, out int studentID))
            {
           
                using (var con = new SqlConnection("Server=LAPTOP-0FV783QN;Database=MIDProject;Integrated Security=True;"))
                {
                    con.Open();

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT AttendanceID, AttendanceDate, AttendanceStatus FROM StudentAttendance INNER JOIN ClassAttendance ON StudentAttendance.AttendanceID = ClassAttendance.ID WHERE StudentID = @StudentID", con))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", studentID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int attendanceID = reader.GetInt32(0);
                                    DateTime attendanceDate = reader.GetDateTime(1);
                                    int attendanceStatus = reader.GetInt32(2);

                              
                                    string status;
                                    switch (attendanceStatus)
                                    {
                                        case 1:
                                            status = "Present";
                                            break;
                                        case 2:
                                            status = "Absent";
                                            break;
                                        case 3:
                                            status = "Leave";
                                            break;
                                        default:
                                            status = "Unknown";
                                            break;
                                    }

                               
                                    dataGridView1.Rows.Add(attendanceID, attendanceDate, status);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid student ID. Please enter a valid numeric ID.");
            }


        }
    }
}
    
