using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;


namespace _13_TopSpotsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Engable Cors -WHITELIST-
            // new instance of EnableCorsAtribute function w/ 3 parameters (#1 POST (URLs Allowed), #2 CREATE (Verbs Allowed), #3 CONTROLS (All Routes Allowed)
            var policy = new EnableCorsAttribute("*", "*" , "*");
            // Tell configuration object to enable CORS and passing in the policy function.
            config.EnableCors(policy);

            // Setup a CamelCase Contract Resolver to change normal case in C# to camelCase in JavaScript.
            // When data comes out of API it will be transformed in camelCase
            // When it comes back into API transformed into Normal Case
            // Using the Json Formatter, setup the contact resolver of the serializer to be a camelCase property names resolver.
            // ContractResolver class is responbile for getting an object in camelCase and will turn into PascalCase/NormalCase.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            // Maps the API attributes routes
            config.MapHttpAttributeRoutes();

            // Maps the API http routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
