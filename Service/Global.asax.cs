﻿using System.Web;
using Service.Mapping;

namespace Service
{
    using System.Web.Http;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MappingConfiguration.InitializeMapper();
        }

        protected void Application_BeginRequest()
        {
            if (Request.HttpMethod == "OPTIONS")
            {
                Response.Flush();
            }
        }
    }
}
