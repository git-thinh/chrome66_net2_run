using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace appui
{
    internal sealed class CefApplication : CefApp
    {
        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return new RenderProcessHandlerJS();
        }
    }

    internal sealed class RenderProcessHandlerJS : CefRenderProcessHandler
    {
        protected override void OnWebKitInitialized()
        {
            //CefRuntime.RegisterExtension("testExtension", "var test; if (!test) test = {}; (function() { test.myval = 'My Value!'; })(); ", null);
            CefRuntime.RegisterExtension("testy", "var testy; if (!testy)testy = {}; (function() { testy.hello = function() {}; })(); ", new V8Handler());

            base.OnWebKitInitialized();
        }
    }
}
