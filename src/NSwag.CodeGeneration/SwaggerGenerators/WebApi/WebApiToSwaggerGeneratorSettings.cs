//-----------------------------------------------------------------------
// <copyright file="WebApiToSwaggerGeneratorSettings.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using NJsonSchema;
using NJsonSchema.Generation;
using Stucco.NSwag.CodeGeneration.Infrastructure;
using Stucco.NSwag.Core.Interfaces;

namespace Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi
{
    /// <summary>Settings for the <see cref="WebApiToSwaggerGenerator" />.</summary>
    public class WebApiToSwaggerGeneratorSettings : JsonSchemaGeneratorSettings
    {
        /// <summary>Initializes a new instance of the <see cref="WebApiToSwaggerGeneratorSettings" /> class.</summary>
        public WebApiToSwaggerGeneratorSettings()
        {
            DefaultUrlTemplate = "api/{controller}/{action}/{id}";
            NullHandling = NullHandling.Swagger;
            DocumentationProvider = DocumentationService.Default;
        }

        /// <summary>Gets or sets the default Web API URL template.</summary>
        public string DefaultUrlTemplate { get; set; }

        /// <summary>
        /// </summary>
        public IDocumentationService DocumentationProvider { get; set; }
    }
}