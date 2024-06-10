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
    public partial class All_Attendence_form : Form
    {
        public All_Attendence_form()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //  LoadAttendanceData();

            DataGridViewTextBoxColumn presentColumn = new DataGridViewTextBoxColumn();
            presentColumn.HeaderText = "Present";
            presentColumn.Name = "Present";
            dataGridView1.Columns.Add(presentColumn);

            DataGridViewTextBoxColumn absentColumn = new DataGridViewTextBoxColumn();
            absentColumn.HeaderText = "Absent";
            absentColumn.Name = "Absent";
            dataGridView1.Columns.Add(absentColumn);

            DataGridViewTextBoxColumn leaveColumn = new DataGridViewTextBoxColumn();
            leaveColumn.HeaderText = "Leave";
            leaveColumn.Name = "Leave";
            dataGridView1.Columns.Add(leaveColumn);

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM student WHERE Status =1", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            SqlCommand cmd1 = new SqlCommand("SELECT * FROM studentAttendance ", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            dataGridView1.DataSource = dt;
            con.Close();



            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name != "Present" && column.Name != "Absent" && column.Name != "Leave")
                {
                    column.ReadOnly = true;
                }
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=LAPTOP-0FV783QN;Database=MIDProject;Integrated Security=True;";
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                try
                {
                    DateTime selectedDate = dateTimePicker1.Value.Date;

                    using (SqlCommand checkDateCmd = new SqlCommand("SELECT COUNT(*) FROM ClassAttendance WHERE AttendanceDate = @AttendanceDate", con))
                    {
                        checkDateCmd.Parameters.AddWithValue("@AttendanceDate", selectedDate);

                        int existingDateCount = Convert.ToInt32(checkDateCmd.ExecuteScalar());

                        if (existingDateCount == 0)
                        {
                            using (SqlCommand saveDateCmd = new SqlCommand("INSERT INTO ClassAttendance (AttendanceDate) VALUES (@AttendanceDate); SELECT SCOPE_IDENTITY();", con))
                            {
                                saveDateCmd.Parameters.AddWithValue("@AttendanceDate", selectedDate);

                                int attendanceID = Convert.ToInt32(saveDateCmd.ExecuteScalar());

                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    int studentID = Convert.ToInt32(row.Cells["ID"].Value);

                                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                                    {
                                        if (column.Name != "ID")
                                        {
                                            int attendanceStatus = 0;
                                            string attendanceStatusP = row.Cells["Present"].Value?.ToString();
                                            string attendanceStatusA = row.Cells["Absent"].Value?.ToString();
                                            string attendanceStatusL = row.Cells["Leave"].Value?.ToString();

                                            if (!string.IsNullOrEmpty(attendanceStatusP) && attendanceStatusP.ToLower() == "p" || !string.IsNullOrEmpty(attendanceStatusA) && attendanceStatusA.ToLower() == "a" || !string.IsNullOrEmpty(attendanceStatusL) && attendanceStatusL.ToLower() == "l")
                                            {
                                                if (attendanceStatusP == "p")
                                                {
                                                    attendanceStatus = 1;
                                                }
                                                else if (attendanceStatusA == "a")
                                                {
                                                    attendanceStatus = 2;
                                                }
                                                else if (attendanceStatusL == "l")
                                                {
                                                    attendanceStatus = 3;
                                                }

                                                using (SqlCommand cmd = new SqlCommand("INSERT INTO StudentAttendance (AttendanceID, StudentID, AttendanceStatus) VALUES (@AttendanceID, @StudentID, @AttendanceStatus)", con))
                                                {
                                                    cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                                                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                                                    cmd.Parameters.AddWithValue("@AttendanceStatus", attendanceStatus);

                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                }

                                MessageBox.Show("Attendance saved successfully");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Attendance for the selected date already exists.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show($"Error: {ex.Message}");
                    MessageBox.Show("Successfully Added");
                }
                finally
                {
                    con.Close();
                }
            }


        }

    }






 }
