using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Web.Swagger
{
    public class AuthAnnotationFilter : IOperationFilter
    {
        public AuthAnnotationFilter() { }

        private bool HasAttribute(MethodInfo methodInfo, Type type, bool inherit)
        {
            var actionAttributes = methodInfo.GetCustomAttributes(inherit);
            var controllerAttributes = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(inherit);
            var actionAndControllerAttributes = actionAttributes.Union(controllerAttributes);


            return actionAndControllerAttributes.Any(attr =>
            {
                return attr.GetType() == type;
            });
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool hasAuthorizeAttribute = HasAttribute(context.MethodInfo, typeof(AuthorizeAttribute), true);
            bool hasAnonymousAttribute = HasAttribute(context.MethodInfo, typeof(AllowAnonymousAttribute), true);

            // so far as I understood the action/operation is public/unprotected 
            // if there is no authorize or an allow anonymous (allow anonymous overrides all authorize)
            bool isAuthorized = hasAuthorizeAttribute && !hasAnonymousAttribute;

            if (isAuthorized)
            {
                operation.Security.Add(new OpenApiSecurityRequirement
                                            {
                                                {
                                                    new OpenApiSecurityScheme
                                                    {
                                                       Reference = new OpenApiReference
                                                       {
                                                           Type=ReferenceType.SecurityScheme,
                                                            Id="Bearer"
                                                        }
                                                    },
                                                   new string[]{}
                                                }
                                            });
            }
        }
    }
}
