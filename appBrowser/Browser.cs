﻿using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace appBrowser
{

    public class RenderProcessHandler : CefRenderProcessHandler
    {
        #region Private/Protected Methods

        protected override void OnBrowserDestroyed(CefBrowser browser) { }

        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            ////////////if (message.Name == RenderProcessMessages.VisitDom)
            ////////////{
            ////////////    string visitorId = message.Arguments.GetString(0);
            ////////////    lock (DomVisitor.Visitors)
            ////////////    {
            ////////////        DomVisitor visitor = null;
            ////////////        if (DomVisitor.Visitors.TryGetValue(visitorId, out visitor))
            ////////////        {
            ////////////            browser.GetMainFrame().VisitDom(visitor);
            ////////////            return true;
            ////////////        }
            ////////////    }
            ////////////}

            //return base.OnProcessMessageReceived(browser, sourceProcess, message);

            return true;
        }

        #endregion
    }

    public static class RenderProcessMessages
    {
        #region Public/Internal Properties

        public static string VisitDom
        {
            get { return "Renderer.Messages.VisitDom"; }
        }

        #endregion
    }

    public class TestApp : CefApp
    {
        #region Private/Protected Fields and Constants

        private readonly RenderProcessHandler renderProcessHandler;

        #endregion

        #region Constructors

        public TestApp()
        {
            renderProcessHandler = new RenderProcessHandler();
        }

        #endregion

        #region Private/Protected Methods

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return renderProcessHandler;
        }

        #endregion
    }
}