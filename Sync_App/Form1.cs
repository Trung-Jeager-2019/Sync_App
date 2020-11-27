using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using System.Data.SqlClient;

namespace Sync_App
{
    public partial class Form_Sync : Form
    {
        //static string sServerConnection = @"Data Source=192.168.1.112;Initial Catalog=Server;User ID=sa;Password=123456";

        static string sServerConnection = @"Data Source=DESKTOP-AJ7H4HH\MSSQLSERVER2019;Initial Catalog=my_database;Integrated Security=True";

        static string sClientConnection = @"Data Source=DESKTOP-AJ7H4HH\MSSQLSERVER2019;Initial Catalog=Menu_PhucVuThucUong;Integrated Security=True";

        static string sScope = "MainScope";
        //Get Data From Client Provision
        public static void ProvisionClient()
        {
            SqlConnection serverConn = new SqlConnection(sServerConnection);
            SqlConnection clientConn = new SqlConnection(sClientConnection);

            //Drop scope_Info Table
            string cmdText = @"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='scope_info') DROP table scope_info";
            clientConn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, clientConn);
            cmd.ExecuteScalar();
            clientConn.Close();


            List<string> tables = new List<string>();
            // tables.Add("Accounts"); // Add Tables in List
            // tables.Add("Drinks");
            // tables.Add("Roles");
            // tables.Add("Tables");
            tables.Add("Toppings");

            var scopeDesc = new DbSyncScopeDescription("MainScope");
            foreach (var tbl in tables) //Add Tables in Scope
            {
                scopeDesc.Tables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable(tbl, clientConn));
            }

            SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(clientConn, scopeDesc); //Provisioning

            //skip creating the user tables
            clientProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            //skip creating the change tracking tables
            clientProvision.SetCreateTrackingTableDefault(DbSyncCreationOption.Skip);

            //skip creating the change tracking triggers
            clientProvision.SetCreateTriggersDefault(DbSyncCreationOption.Skip);

            //skip creating the insert/update/delete/selectrow SPs including those for metadata
            clientProvision.SetCreateProceduresDefault(DbSyncCreationOption.Skip);

            //create new SelectChanges SPs for selecting changes for the new scope
            //the new SelectChanges SPs will have a guid suffix
            clientProvision.SetCreateProceduresForAdditionalScopeDefault(DbSyncCreationOption.Create);


            clientProvision.Apply();
        }
        //Set Data To Server Provision
        public static void ProvisionServer()
        {

            SqlConnection serverConn = new SqlConnection(sServerConnection);

            string cmdText = @"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='scope_info') DROP table scope_info";
            serverConn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, serverConn);
            cmd.ExecuteScalar();
            serverConn.Close();

            List<string> tables = new List<string>();
            // tables.Add("Accounts"); // Add Tables in List
            // tables.Add("Drinks");
            // tables.Add("Roles");
            // tables.Add("Tables");
            tables.Add("Toppings");

            var scopeDesc = new DbSyncScopeDescription("MainScope");
            foreach (var tbl in tables)
            {
                scopeDesc.Tables.Add(SqlSyncDescriptionBuilder.GetDescriptionForTable(tbl, serverConn));
            }

            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc); // Create Provision From All Tables

            //skip creating the user tables
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);

            //skip creating the change tracking tables
            serverProvision.SetCreateTrackingTableDefault(DbSyncCreationOption.Skip);

            //skip creating the change tracking triggers
            serverProvision.SetCreateTriggersDefault(DbSyncCreationOption.Skip);

            //skip creating the insert/update/delete/selectrow SPs including those for metadata
            serverProvision.SetCreateProceduresDefault(DbSyncCreationOption.Skip);

            serverProvision.Apply();
        }

        public static void Sync()
        {
            SqlConnection serverConn = new SqlConnection(sServerConnection);

            SqlConnection clientConn = new SqlConnection(sClientConnection);

            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();

            syncOrchestrator.LocalProvider = new SqlSyncProvider(sScope, clientConn);

            syncOrchestrator.RemoteProvider = new SqlSyncProvider(sScope, serverConn);

            syncOrchestrator.Direction = SyncDirectionOrder.Upload;

            ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);

            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();

            Console.WriteLine("Start Time: " + syncStats.SyncStartTime);

            Console.WriteLine("Total Changes Uploaded: " + syncStats.UploadChangesTotal);

            //Console.WriteLine("Total Changes Downloaded: " + syncStats.DownloadChangesTotal);

            Console.WriteLine("Complete Time: " + syncStats.SyncEndTime);

            Console.WriteLine(String.Empty);

            Console.ReadLine();
        }
        static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            Console.WriteLine(e.Conflict.Type);
            Console.WriteLine(e.Error);
        }
        public Form_Sync()
        {
            InitializeComponent();
        }

        private void button_Sync_Click(object sender, EventArgs e)
        {
            ProvisionClient();
            ProvisionServer();
            Sync();
        }

        private void button_Thoat_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
