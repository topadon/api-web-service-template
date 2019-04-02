using API.Gateway.App_Start;
using API.Gateway.Filters;
using Swashbuckle.Application;
using System.Web;
using System.Web.Http;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace API.Gateway.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
              .EnableSwagger(c =>
              {
                  c.SingleApiVersion("v1", "API.Gateway");
                  //c.IgnoreObsoleteActions();
                  //c.UseFullTypeNameInSchemaIds();
                  //c.DescribeAllEnumsAsStrings();
                  //c.OperationFilter<AddRequiredHeaderParameter>();
              })
              .EnableSwaggerUi();
        }
    }
}