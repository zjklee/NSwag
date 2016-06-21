using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stucco.NSwag.Annotations;
using Stucco.NSwag.CodeGeneration.Infrastructure;
using Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi;
using Stucco.NSwag.Core;

namespace NSwag.CodeGeneration.Tests.WebApiToSwaggerGenerator.Attributes
{
    [TestClass]
    public class BindedTests
    {
        public class Complex2
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        public class Complex
        {
            public Complex2 X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        [Route("api")]
        [Version("1.0.0")]
        [Title("Test documentation")]
        [Scheme("http"), Scheme("https")]
        [Stucco.NSwag.Annotations.Description(Description = "Test documentation description")]
        public class TestController : ApiController
        {
            /// <summary>
            /// Test methodmmmm
            /// </summary>
            /// <param name="a">Some param</param>
            /// <param name="b">Some binded param from route</param>
            /// <param name="c">Some binded param from url</param>
            /// <returns>A param</returns>
            [HttpGet]
            [Route("{a}/{b}/test")]            
            [Display(Name = "Add", Description = "Test method")]
            [Header("X-Header", "", false)]
            [Parameter("page", typeof(int), "5", "Page number", false)]
            [ResponseType("200", typeof(IEnumerable<Complex>), Description = "OK")]
            [ResponseType("500", typeof(string), Description = "BAD")]
            [ResponseHeader("X-Page-Count", "200", "Descr")]
            [ResponseHeader("X-Page-Count2", "200", "Descr")]
            [ResponseHeader("X-Page-Count3", "200", "Descr")]
            [ResponseHeader("X-Page-Count4", "500", "Descr")]
            [ResponseHeader("X-Page-Count5", "500", "Descr")]
            public IHttpActionResult AddPost(
                [Display(Description = "Some param"), DefaultValue(typeof(int), "10")]int a, 
                [ModelBindsFrom(typeof(string)), Display(Description = "Some binded param from route")]Complex b, 
                [ModelBindsFrom(typeof(string)), Display(Description = "Some binded param from url")]Complex c = null)
            {
                return Ok(a); 
            }
        }

        [TestMethod]
        public void When_modelbinder_is_used_then_http_method_is_correct()
        {
            DocumentationService<TestController>
                .Create(typeof(TestController).GetMethod("AddPost"), documentation =>
                {
                    documentation.Descripation = "Some desc";
                });

            DocumentationService<TestController>
                .Create(typeof(TestController).GetMethod("AddPost").ReturnParameter, documentation =>
                {
                    documentation.Descripation = "Some return descr";
                });

            DocumentationService<TestController>
                .Create(typeof(TestController).GetMethod("AddPost").GetParameters().Single(o => o.Name == "a"), documentation =>
                {
                    documentation.Descripation = "Some a descr";
                });


            //// Arrange
            var generator = new Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings
            {
                DocumentationProvider = new AttributeDocumentationService(typeof(DisplayAttribute), nameof(DisplayAttribute.Description))
            });
            var service = Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi.WebApiToSwaggerGenerator.CreateService();            
            //// Act
            service = generator.GenerateForController<TestController>(swaggerService: service);
            var operation = service.Operations.First(o => o.Operation.OperationId == "Test_AddPost");
            var json = service.ToJson();
            //// Assert
            Assert.AreEqual(SwaggerOperationMethod.Get, operation.HttpMethod);
        }

        [TestMethod]
        public void When_accept_verbs_attribute_with_get_is_used_then_http_method_is_correct()
        {
            //// Arrange
            var generator = new Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());

            //// Act
            var service = generator.GenerateForController<TestController>();
            var operation = service.Operations.First(o => o.Operation.OperationId == "Test_AddGet");

            //// Assert
            Assert.AreEqual(SwaggerOperationMethod.Get, operation.HttpMethod);
        }

        [TestMethod]
        public void When_accept_verbs_attribute_with_delete_is_used_then_http_method_is_correct()
        {
            //// Arrange
            var generator = new Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());

            //// Act
            var service = generator.GenerateForController<TestController>();
            var operation = service.Operations.First(o => o.Operation.OperationId == "Test_AddDelete");

            //// Assert
            Assert.AreEqual(SwaggerOperationMethod.Delete, operation.HttpMethod);
        }

        [TestMethod]
        public void When_accept_verbs_attribute_with_put_is_used_then_http_method_is_correct()
        {
            //// Arrange
            var generator = new Stucco.NSwag.CodeGeneration.SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());

            //// Act
            var service = generator.GenerateForController<TestController>();
            var operation = service.Operations.First(o => o.Operation.OperationId == "Test_AddPut");

            //// Assert
            Assert.AreEqual(SwaggerOperationMethod.Put, operation.HttpMethod);
        }
    }
}