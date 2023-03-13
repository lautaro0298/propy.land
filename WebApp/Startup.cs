using Microsoft.Owin;
using Owin;
using JavaScriptEngineSwitcher;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;

using React.AspNet;
using FluentAssertions.Common;
using JavaScriptEngineSwitcher.Core;
using React;

[assembly: OwinStartupAttribute(typeof(WebApp.Startup))]
namespace WebApp
{
    


    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ReactSiteConfiguration.Configuration
              .AddScript("~/React/src/App.jsx");

            JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
            JsEngineSwitcher.Current.EngineFactories.AddV8();
            ConfigureAuth(app);
        }
    }

}
