using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;

namespace webform_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            webBrowser1.DocumentText =
                "<html><body>Please enter your name:<br/>" +
                "<input type='text' name='userName'/><br/>"+
                "<a href='http://www.baidu.com'>进入</a>" +
                "</body></html>";
            webBrowser1.Navigating +=
                new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            
            System.Windows.Forms.HtmlDocument document = this.webBrowser1.Document;

            if (document != null && document.All["userName"] != null &&
                String.IsNullOrEmpty(
                document.All["userName"].GetAttribute("value")))
            {
                e.Cancel = true;              
                System.Windows.Forms.MessageBox.Show(
                    "You must enter your name before you can navigate to " +
                    e.Url);
            }
        }
        /*
        private void textBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Navigate(textBoxAddress.Text);
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            Navigate(textBoxAddress.Text);
        }
        */

        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address))
                return;
            if (address.Equals("about:blank"))
                return;
            if(!address.StartsWith("http://")&&!address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch(System.UriFormatException)
            {
                return;
            }

        }
    }
}
