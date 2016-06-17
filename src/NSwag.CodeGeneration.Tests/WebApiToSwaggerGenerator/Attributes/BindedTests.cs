using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSwag.Annotations;
using NSwag.CodeGeneration.Infrastructure;
using NSwag.CodeGeneration.SwaggerGenerators.WebApi;

namespace NSwag.CodeGeneration.Tests.WebApiToSwaggerGenerator.Attributes
{
    [TestClass]
    public class BindedTests
    {
        public class Complex
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        [Route("api")]
        [System.ComponentModel.Description("Controller descr")]
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
            [ResponseType("200", typeof(Complex), Description = "A param")]
            [ResponseType("400", typeof(int), Description = "Bad")]
            [Display(Name = "Add", Description = "Test method")]
            public IHttpActionResult AddPost(
                [Display(Description = "Some param")]int a, 
                [ModelBinder(typeof(string)), Display(Description = "Some binded param from route")]Complex b, 
                [ModelBinder(typeof(string)), Display(Description = "Some binded param from url"), Annotations.Description]Complex c = null)
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
            var generator = new SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings
            {
                DocumentationProvider = new AttributeDocumentationService(typeof(DisplayAttribute), nameof(DisplayAttribute.Description))
            });

            //// Act
            var service = generator.GenerateForController<TestController>();
            var operation = service.Operations.First(o => o.Operation.OperationId == "Test_AddPost");
            var json = service.ToJson();
            //// Assert
            Assert.AreEqual(SwaggerOperationMethod.Get, operation.HttpMethod);
        }

        [TestMethod]
        public void When_accept_verbs_attribute_with_get_is_used_then_http_method_is_correct()
        {
            //// Arrange
            var generator = new SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());

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
            var generator = new SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());

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
            var generator = new SwaggerGenerators.WebApi.WebApiToSwaggerGenerator(new WebApiToSwaggerGeneratorSettings());

            //// Act
            var service = generator.GenerateForController<TestController>();
            var operation = service.Operations.First(o => o.Operation.OperationId == "Test_AddPut");

            //// Assert
            Assert.AreEqual(SwaggerOperationMethod.Put, operation.HttpMethod);
        }
    }
}