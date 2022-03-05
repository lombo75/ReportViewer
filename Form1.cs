using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Reporting.WinForms;

namespace ReportsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         //if (CustomerList.CustomerID != "")
         //{
            this.reportViewer1.Reset();
         //this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportsApplication1.Report1.rdlc";
         /* 
    * The processing mode of a report viewer is Local by default, 
    * this line is not necessary. 
    */
         this.reportViewer1.ProcessingMode = ProcessingMode.Local;

         PrepareReport(reportViewer1.LocalReport, "Report1.rdlc", getData());

         //switch (strOption)
         //{
         //   case "Preview":
         //      reportViewer1.RefreshReport();
         //      break;
            //case "ExporttoPDF":
               GenerateFile("PDF", reportViewer1.LocalReport, "report.pdf");
            //   break;
            //case "ExporttoExcel":
            //   GenerateFile("Excel", reportViewer1.LocalReport, "Reports/report.xls");
            //   break;
            //   // Add more cases as needed.
         //}
         //ReportDataSource rds = new ReportDataSource("CustomerListRetrieve", getCustomerData());
         //this.reportViewer1.LocalReport.DataSources.Clear();
         //this.reportViewer1.LocalReport.DataSources.Add(rds);
         //ReportParameter CustID = new ReportParameter("CustomerID", CustomerList.CustomerID);
         //this.reportViewer1.LocalReport.SetParameters(CustID);
         this.reportViewer1.LocalReport.Refresh();
			//ViewButtonClicked();
			//}
			//else
			//{ }
			this.reportViewer1.RefreshReport();
		}
      private DataTable getData()
      {
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ReportsApplication1.Properties.Settings.NicomConnectionString"].ConnectionString);
         DataSet ds = new DataSet();
			ds.DataSetName = "NicomDataSet";
			string sql = "SELECT * FROM tblDevelopmentStage";
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			da.Fill(ds);
			DataTable dt = ds.Tables[0];
         return dt;
      }
      // Prepare the report
      private void PrepareReport(LocalReport report, string reportFilePath, DataTable data = null)
      {
         report.ReportPath = reportFilePath;
			report.DataSources.Add(new ReportDataSource("DataSet1", data));
		}
      // Generate a file
      private void GenerateFile(string fileType, LocalReport report, string savePath)
      {
         // The FileStream class is in the System.IO namespace.
         /* 
          * The savePath include the file name with the proper file extension.
          * If file is a pdf -> filename.pdf
          */
         byte[] bytes = report.Render(fileType);
         FileStream fs = new FileStream(savePath, FileMode.Create);
         fs.Write(bytes, 0, bytes.Length);
         fs.Close();
      }
      
   }
}