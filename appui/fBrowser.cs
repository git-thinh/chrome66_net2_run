using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;

namespace appui
{
    public class V8Handler : Xilium.CefGlue.CefV8Handler
    {

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {

            if (name == "testy")
                Console.WriteLine("CALLED TESTY");
            else
                Console.WriteLine("CALLED SOMETHING WEIRED ({0})", name);

            returnValue = CefV8Value.CreateNull();
            exception = null;

            return true;
        }

    }

    public class fBrowser : Form
    {
        TextBox ui_browAddressTextBox;
        CefWebBrowser browser;
        //string URL = "about:blank";
        string URL = "https://vnexpress.net";
        //string URL = "https://webcamtoy.com/";

        public fBrowser()
        {
            browser = new CefWebBrowser
            {
                Dock = DockStyle.Fill
            };
            browser.Parent = this;
            browser.BrowserCreated += (se, ev) => f_browserCreated();


            Panel footer = new Panel()
            {
                Height = 15,
                Dock = DockStyle.Bottom,
                BackColor = System.Drawing.Color.LightGray,
            };
            this.Controls.Add(footer);

            ui_browAddressTextBox = new TextBox()
            {
                Text = URL,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackColor = System.Drawing.Color.LightGray,
                ForeColor = System.Drawing.Color.Gray
            };
            ui_browAddressTextBox.KeyUp += (se, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                    f_browserGo(ui_browAddressTextBox.Text.Trim());
            };

            Label lblMenu = new Label()
            {
                Dock = DockStyle.Right,
                Text = "☰",
                Width = 35,
                BackColor = System.Drawing.Color.Gray,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            lblMenu.Click += (se, ev) => f_menuClick();

            footer.Controls.AddRange(new Control[] { ui_browAddressTextBox, lblMenu });
            this.Shown += (se, ev) =>
            {
                this.Icon = Resources.icon;
                this.Text = "Browser";

                this.Width = 800;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Top = 0;
                this.Left = 0;
            };
        }

        void f_menuClick()
        {
            ///browser.Browser.GetMainFrame().ExecuteJavaScript("document.getElementById('btnOK').onclick = function () {alert('hello world!'); }", URL, 0);
            //browser.Browser.GetMainFrame().ExecuteJavaScript("alert(test.myval);", URL, 0);
            //browser.Browser.GetMainFrame().ExecuteJavaScript("alert(document.body.innerText);", URL, 0);

            //////var result = CefV8Value.CreateNull();
            ////CefV8Value result = CefV8Value.CreateString("");
            ////CefV8Exception err;
            ////browser.Browser.GetMainFrame().V8Context.TryEval("return document.body.innerText;", URL, 0, out result, out err);
            ////MessageBox.Show(result.ToString());


        }

        void f_browserCreated()
        {
            browser.Browser.GetMainFrame().LoadUrl(URL);

            var host = browser.Browser.GetHost();
            var wi = CefWindowInfo.Create();
            wi.SetAsPopup(IntPtr.Zero, "DevTools");
            host.ShowDevTools(wi, new DevToolsWebClient(), new CefBrowserSettings(), new CefPoint(0, 0));
        }

        void f_browserGo(string url)
        {
            browser.Browser.GetMainFrame().LoadUrl(url);
            ui_browAddressTextBox.Text = url;
            URL = url;
        }


        void f_test_sendProcessMessageCommand()
        {
            var message = CefProcessMessage.Create("myMessage1");
            var arguments = message.Arguments;
            arguments.SetString(0, "hello");
            arguments.SetInt(1, 12345);
            arguments.SetDouble(2, 12345.6789);
            arguments.SetBool(3, true);

            browser.Browser.SendProcessMessage(CefProcessId.Renderer, message);
        }

        void f_test_sendKeyEventCommand()
        {
            var host = browser.Browser.GetHost();

            foreach (var c in "This text typed with CefBrowserHost.SendKeyEvent method!")
            {
                // little hacky
                host.SendKeyEvent(new CefKeyEvent
                {
                    EventType = CefKeyEventType.Char,
                    Modifiers = CefEventFlags.None,
                    WindowsKeyCode = c,
                    NativeKeyCode = c,
                    Character = c,
                    UnmodifiedCharacter = c,
                });
            }
        }
    }

    class DevToolsWebClient : CefClient
    {
    }

}
