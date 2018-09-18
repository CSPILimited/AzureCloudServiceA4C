using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

namespace WebRoleA4C
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument cryRpt = new ReportDocument();
            //cryRpt.Load("c:/temp/CrystalReport1.rpt");
            cryRpt.Load(Server.MapPath("CrystalReport1.rpt"));
            //FixDatabase(cryRpt, "packet size = 4096; User = sa; Password = rg121yq; data source = 'malcolm-w8'; persist security info = False; initial catalog = SiteProfile; Connect Timeout = 180; Connection LifeTime = 20");
            FixDatabase(cryRpt, "Server=tcp:cspiuktest.database.windows.net,1433;Initial Catalog=SiteProfile;Persist Security Info=False;User ID=cspiadmin;Password=M0chdreChester99$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            CrystalReportViewer1.ReportSource = cryRpt;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
        }

        public static void FixDatabase(ReportDocument report, string ReportingConnectionString)
        {
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in report.Database.Tables)
            {
                TableLogOnInfo logOnInfo = table.LogOnInfo;

                if (logOnInfo != null)
                {
                    // Reporting Data Base Connection String
                    //  <add  
                    //  key="ReportingConnectionString" 
                    //  value=
                    //      "
                    //          packet size=4096;
                    //      **  User=sa;
                    //      **  Password=rg121yq;
                    //      **  data source='ANDYOB2010DEV\DEVSQL2008';
                    //          persist security info=False;
                    //      **  initial catalog=EASPIRE_REPORTING;
                    //          Connect Timeout=180;
                    //         Connection LifeTime=20
                    //      "
                    //  />
                    int sPos;
                    int slen;
                    try
                    {
                        /* Testing 
                        sPos = ReportingConnectionString.IndexOf("User=") + "User=".Length;
                        slen = ReportingConnectionString.IndexOf(';', sPos) - sPos;
                        table.LogOnInfo.ConnectionInfo.UserID = "sa";
                        // table.LogOnInfo.ConnectionInfo.UserID = ReportingConnectionString.Substring(sPos, slen);


                        sPos = ReportingConnectionString.IndexOf("Password=") + "Password=".Length;
                        slen = ReportingConnectionString.IndexOf(';', sPos) - sPos;
                        table.LogOnInfo.ConnectionInfo.Password = "rg121yq";
                        //table.LogOnInfo.ConnectionInfo.Password = ReportingConnectionString.Substring(sPos, slen);

                        sPos = ReportingConnectionString.IndexOf("initial catalog=") + "initial catalog=".Length;
                        slen = ReportingConnectionString.IndexOf(';', sPos) - sPos;
                        table.LogOnInfo.ConnectionInfo.DatabaseName = "EASPIRE_REPORTING";
                        //table.LogOnInfo.ConnectionInfo.DatabaseName = ReportingConnectionString.Substring(sPos, slen);

                        sPos = ReportingConnectionString.IndexOf("data source=") + "data source=".Length;
                        slen = ReportingConnectionString.IndexOf(';', sPos) - sPos;
                        table.LogOnInfo.ConnectionInfo.ServerName = @"AndyOB2010DEV\DEVSQL2008";
                        //table.LogOnInfo.ConnectionInfo.ServerName = ReportingConnectionString.Substring(sPos, slen);
                        */

                        System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();

                        if ((string)(configurationAppSettings.GetValue("reportingUser", typeof(string))) == null || (string)(configurationAppSettings.GetValue("reportingUser", typeof(string))) == "")
                        {
                            table.LogOnInfo.ConnectionInfo.IntegratedSecurity = true;
                        }
                        else
                        {
                            table.LogOnInfo.ConnectionInfo.UserID = (string)(configurationAppSettings.GetValue("reportingUser", typeof(string)));
                            table.LogOnInfo.ConnectionInfo.Password = (string)(configurationAppSettings.GetValue("reportingPassword", typeof(string)));
                        }
                        table.LogOnInfo.ConnectionInfo.DatabaseName = (string)(configurationAppSettings.GetValue("reportingDatabase", typeof(string)));
                        table.LogOnInfo.ConnectionInfo.ServerName = (string)(configurationAppSettings.GetValue("reportingServer", typeof(string)));

                        table.ApplyLogOnInfo(table.LogOnInfo);
                    }
                    catch (Exception x)
                    {
                        string m_last_error = x.Message;
                    }

                }
            }
        }
    }
}