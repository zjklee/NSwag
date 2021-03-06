﻿//-----------------------------------------------------------------------
// <copyright file="SwaggerUiExtensions.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace NSwag.AspNetCore
{
    /// <summary>Provides OWIN extensions to enable Swagger UI.</summary>
    public static class SwaggerUiExtensions
    {
        /// <summary>Addes the Swagger generator and Swagger UI to the OWIN pipeline.</summary>
        /// <param name="app">The app.</param>
        /// <param name="webApiAssembly">The Web API assembly to search for controllers.</param>
        /// <param name="settings">The Swagger generator settings.</param>
        /// <param name="baseRoute">The Swagger UI base route.</param>
        /// <param name="swaggerUrl">The Swagger specification url.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerUi(
            this IApplicationBuilder app,
            Assembly webApiAssembly,
            //WebApiToSwaggerGeneratorSettings settings,
            string baseRoute = "/swagger/ui",
            string swaggerUrl = "/swagger/v1/swagger.json")
        {
            return app.UseSwaggerUi(new[] { webApiAssembly }, /*settings,*/ baseRoute, swaggerUrl);
        }

        /// <summary>Addes the Swagger generator and Swagger UI to the OWIN pipeline.</summary>
        /// <param name="app">The app.</param>
        /// <param name="webApiAssemblies">The Web API assemblies to search for controllers.</param>
        /// <param name="settings">The Swagger generator settings.</param>
        /// <param name="baseRoute">The Swagger UI base route.</param>
        /// <param name="swaggerUrl">The Swagger specification url.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerUi(
            this IApplicationBuilder app,
            IEnumerable<Assembly> webApiAssemblies,
            //WebApiToSwaggerGeneratorSettings settings,
            string baseRoute = "/swagger/ui",
            string swaggerUrl = "/swagger/v1/swagger.json")
        {
            baseRoute = baseRoute.Trim('/');

            app.UseMiddleware<RedirectMiddleware>(baseRoute, baseRoute + "/index.html?url=" + Uri.EscapeDataString(swaggerUrl));
            app.UseMiddleware<SwaggerMiddleware>(swaggerUrl, webApiAssemblies/*, settings*/);
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(baseRoute),
                FileProvider = new EmbeddedFileProvider(typeof(SwaggerUiExtensions).GetTypeInfo().Assembly, "NSwag.SwaggerUi.AspNetCore.content")
            });

            return app;
        }
    }
}
