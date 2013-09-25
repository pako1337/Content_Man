using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Conventions;

namespace Content_Man.Web
{
    public class CustomBootstraper : Nancy.DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            Conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/Scripts"));
        }
    }
}