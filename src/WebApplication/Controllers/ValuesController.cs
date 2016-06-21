using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using Stucco.NSwag.Annotations;
using Stucco.NSwag.CodeGeneration.Infrastructure;
using Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi;
using Stucco.NSwag.Core;
using Stucco.NSwag.Core.Interfaces;

namespace WebApplication.Controllers
{
    public class Complex
    {
        public int X { get; set; }
        public string Y { get; set; }
    }

    
    [Route("api/ctx")]
    public class ValuesController : Controller
    {                
        [Deprecated]        
        [HttpGet]
        [Route("{a}")]
        [SwaggerTags("testttt", "ttttest")]
        [Description(Summary = "sssumary", Description = "mmmmmmethod")]
        [Header("X-STC", "headerDescr", true)]
        [Header("X-Page-Count", "Maximum number of pages", false)]
        [Parameter("page", typeof(int), "0", "Page number", true)]
        [ResponseType("200", typeof(IEnumerable<Complex>), Description = "OK")]
        [ResponseType("500", typeof(string), Description = "BAD")]   
        [ResponseHeader("X-Page-Count", "200", "Descr")]       
        public ActionResult Get(
            [ModelBindsFrom(typeof(string)), Display(Description = "aaaaa")]Complex a,
            [ModelBindsFrom(typeof(string)), Display(Description = "bbbbb")]Complex b = null)
        {
            return Ok(new string[] { "value1", "value2" });
        }        
    }

    public class XmlService : IDocumentationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public Documentation GetMemberDescription(object memberInfo)
        {
            if (memberInfo is MethodInfo)
            {
                return new Documentation
                {
                    Descripation = ((MethodInfo) memberInfo).GetXmlDocumentation()
                };
            }

            if (memberInfo is ParameterInfo)
            {
                return new Documentation
                {
                    Descripation = ((ParameterInfo)memberInfo).GetXmlDocumentation()
                };                
            }

            return new Documentation();
        }
    }

    public class SwaggerController : Controller
    {
        private static readonly Lazy<byte[]> _swagger = new Lazy<byte[]>(() =>
        {
            var controllers = new[] { typeof(ValuesController) };
            var settings = new WebApiToSwaggerGeneratorSettings
            {
                //DefaultUrlTemplate = "api/{controller}/{action}/{id}",
                DocumentationProvider = new AttributeDocumentationService(typeof(DisplayAttribute), "Description"),
                //DocumentationProvider = new XmlService()
            };

            var s = WebApiToSwaggerGenerator.CreateService();
            
            var generator = new WebApiToSwaggerGenerator(settings);
            var service = generator.GenerateForControllers(controllers);                        

            return Encoding.UTF8.GetBytes(service.ToJson());
        });

        [HttpGet, Route("/api/swagger/docs/v1")]
        public ActionResult Swagger()
        {
            return new FileContentResult(_swagger.Value, "application/json");
        }
    }
}
