using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;

namespace appBrowser
{
    public class fBrowser : Form
    {
        private CefWebBrowser _BROWSER;
        private string _URL = string.Empty;

        public fBrowser()
        {
            _BROWSER = new CefWebBrowser
            {
                Dock = DockStyle.Fill
            };
            _BROWSER.Parent = this;
            _BROWSER.BrowserCreated += (se, ev) =>
            {
                if (File.Exists("url.txt")) _URL = File.ReadAllText("url.txt");
                else _URL = "https://wwww.google.com.vn";
                _BROWSER.Browser.GetMainFrame().LoadUrl(_URL);                
                f_test_ShowDevTools();
            };

            Label lblMove = new Label()
            {
                Top = 0,
                Height = 9,
                Width = 9,
                BackColor = Color.Orange,
            };
            this.Controls.Add(lblMove);
            lblMove.MouseMove += f_form_move_MouseDown;

            this.Shown += (se, ev) =>
            {
                lblMove.BringToFront();

                this.FormBorderStyle = FormBorderStyle.None;
                this.Top = 0;
                this.Left = 0;
                this.Width = 800;
                this.Height = 480;
            };
        }

        void f_test_ShowDevTools()
        {
            var host = _BROWSER.Browser.GetHost();
            var wi = CefWindowInfo.Create();
            wi.SetAsPopup(IntPtr.Zero, "DevTools");
            host.ShowDevTools(wi, new DevToolsWebClient(), new CefBrowserSettings(), new CefPoint(0, 0));
        }

        #region [ FORM MOVE, RESIZE ]

        enum MOUSE_XY { OUT, INT };
        
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void f_form_move_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

    }

    class DevToolsWebClient : CefClient
    {
    }

}
