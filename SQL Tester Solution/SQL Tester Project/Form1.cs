using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SQL_Tester_Project
{
    public partial class frmSQLTester : Form
    {
        SqlConnection booksConnection;
        public frmSQLTester()
        {
            InitializeComponent();
        }

        private void frmSQLTester_Load(object sender, EventArgs e)
        {
            booksConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;"
                                               + "AttachDbFilename=" + Path.GetFullPath("SQLBooksDB.mdf")
                                               + ";Integrated Security=True;"
                                               + "Connect Timeout=30;");
        }

        private void frmSQLTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            booksConnection.Close();
            booksConnection.Dispose();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            SqlCommand resultsCommand = null;
            SqlDataAdapter resultsAdapter = new SqlDataAdapter();
            DataTable resultsTable = new DataTable();
            try
            {
                resultsCommand = new SqlCommand(txtSQLTester.Text, booksConnection);
                resultsAdapter.SelectCommand = resultsCommand;
                resultsAdapter.Fill(resultsTable);
                grdSQLTester.DataSource = resultsTable;
                lblRecords.Text = resultsTable.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in Processing SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            resultsCommand.Dispose();
            resultsAdapter.Dispose();
            resultsTable.Dispose();
        }
    }
}
