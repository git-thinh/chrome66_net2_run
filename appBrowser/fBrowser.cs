using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue.WindowsForms;

namespace appBrowser
{
    public class fBrowser: Form
    {
        private CefWebBrowser browser;
        public fBrowser()
        {
            browser = new CefWebBrowser
            {
                Dock = DockStyle.Fill
            };
            browser.Parent = this;
            browser.BrowserCreated += (se, ev) => {
                browser.Browser.GetMainFrame().LoadUrl("https://google.com.vn");
                //browser.Browser.GetMainFrame().LoadUrl("http://localhost:60000/index.html");
                //browser.Browser.Reload();
                //browser.Browser.GetMainFrame().LoadUrl("about:blank");
                //browser.Browser.GetMainFrame().LoadUrl("http://localhost:60000/index.html"); 
            };
        }
    }
}
