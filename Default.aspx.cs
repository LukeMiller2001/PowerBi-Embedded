using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using PBIWebApp.Properties;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;

namespace PBIWebApp
{
    /* NOTE: This code is for sample purposes only. In a production application, you could use a MVC design pattern.
    * In addition, you should provide appropriate exception handling and refactor authentication settings into 
    * a secure configuration. Authentication settings are hard-coded in the sample to make it easier to follow the flow of authentication. 
    * In addition, the sample uses a single web page so that all code is in one location. However, you could refactor the code into
    * your own production model.
    */
    public partial class _Default : System.Web.UI.Page
    {
        public const string authResultString = "authResult";

        public AuthenticationResult authResult { get; set; }
        string baseUri = Settings.Default.PowerBiDataset;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session[authResultString] != null)
            {

                authResult = (AuthenticationResult)Session[authResultString];

                signInStatus.Visible = true;
                signInButton.Visible = false;

                userLabel.Text = authResult.UserInfo.DisplayableId;
                //accessTokenTextbox.Text = AuthenticationResult.AccessToken;
            }

        }

        protected void signInButton_Click(object sender, EventArgs e)
        {

            var @params = new NameValueCollection
            {

                {"response_type", "code"},

                {"client_id", Properties.Settings.Default.ApplicationID},

                {"resource", Properties.Settings.Default.PowerBiAPIResource},

                {"redirect_url", "http://localhost:13526/Default"}

            };

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add(@params);

            string authorityUri = Properties.Settings.Default.AADAuthorityUri;
            var authUri = String.Format("{0}?{1}", authorityUri, queryString);
            Response.Redirect(authUri);
        }

        protected void embedReportButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/EmbedReport.aspx");
        }

        protected void embedDashboardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/EmbedDashboard.aspx");
        }

        protected void embedTileButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/EmbedTile.aspx");
        }
    }
}