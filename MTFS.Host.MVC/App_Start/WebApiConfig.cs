using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;

namespace MTFS.Host.MVC
{
    public static class WebApiConfig
    { 
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new JWTAuthenticationAttribute());

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MessageHandlers.Add(new PreflightRequestsHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();


            //config.Routes.MapHttpRoute("Html", "{whatever}.html/{*pathInfo}", null, null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("png", "{whatever}.png/{*pathInfo}", null, null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("jpg", "{whatever}.jpg/{*pathInfo}", null, null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("gif", "{whatever}.gif/{*pathInfo}", null, null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("ico", "{whatever}.ico/{*pathInfo}", null, null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("mp3", "{whatever}.mp3/{*pathInfo}", null, null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("ogg", "{whatever}.ogg/{*pathInfo}", null, null, new StopRoutingHandler());

            config.Formatters.JsonFormatter.SupportedMediaTypes
             .Add(new MediaTypeHeaderValue("text/html"));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "account", action = "login", id = RouteParameter.Optional }
            );
        }
    }
    public class PreflightRequestsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("Origin") && request.Method.Method == "OPTIONS")
            {
                var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization");
                response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, PUT, POST, DELETE");
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    } 
}
