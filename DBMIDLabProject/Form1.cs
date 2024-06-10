using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace DBMIDLabProject
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            /* SqlCommand cmd = new SqlCommand(@"
     SELECT s.id, s.firstname, s.RegistrationNumber, c.Name AS CLOName, a.title, ac.TotalMarks, ac.RubricId
     FROM student s
     INNER JOIN studentresult sr ON s.id = sr.studentid
     LEFT JOIN clo c ON sr.studentid = c.Id
     LEFT JOIN assessmentcomponent ac ON sr.AssessmentComponentId = ac.Id
     LEFT JOIN assessment a ON ac.AssessmentId = a.Id
     ORDER BY s.id, c.Name, a.title
 ", con);*/

            SqlCommand cmd = new SqlCommand(@"
    SELECT s.id, s.firstname, s.RegistrationNumber, c.CLOName, sr.Result
    FROM student s
    LEFT JOIN studentresult sr ON s.id = sr.studentid
    LEFT JOIN clo c ON sr.CLOId = c.CLOId
    ORDER BY s.id, c.CLOName
", con);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            GeneratePDF(dt);
        }

        private void GeneratePDF(DataTable dt)
        {
            Document doc = new Document();

           
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Report.pdf");

            try
            {
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

                doc.Open();

              
                PdfPTable table = new PdfPTable(dt.Columns.Count);

                
                foreach (DataColumn column in dt.Columns)
                {
                    table.AddCell(new PdfPCell(new Phrase(column.ColumnName)));
                }

                
                foreach (DataRow row in dt.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        table.AddCell(new PdfPCell(new Phrase(item.ToString())));
                    }
                }

                doc.Add(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during PDF generation: " + ex.Message);
            }
            finally
            {
                doc.Close();
            }

            MessageBox.Show("PDF report generated successfully!");

           
            System.Diagnostics.Process.Start(filePath);
        }

    }
}
