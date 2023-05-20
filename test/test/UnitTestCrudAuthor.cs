
using dotnetapi.Services.Master;
using dotnetapi.Models.Master;
using AutoMapper;
using dotnetapi.Models.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using dotnetapi.ViewModels.Master;
using dotnetapi.Profiles.Master;
using dotnetapi.Controllers;
using dotnetapi.ViewModels.Base;
using dotnetapi.Utils;
using NTR.Common.DataAccess;

namespace test
{
    public class UnitTestCrudAuthor
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        private DBMasterContext dbMasterContext;
        private AuthorService authorService;

        private long id = 3;

        public UnitTestCrudAuthor() {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            this.configuration = builder.Build();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AuthorProfile());
            });
            this.mapper = mappingConfig.CreateMapper();

            var serviceProvider = new Moq.Mock<IServiceProvider>();
            var searchUtil = new Moq.Mock<ISearchUtil>();
            var uowProvider = new Moq.Mock<IUowProvider>();

            this.dbMasterContext = new DBMasterContext(new Microsoft.EntityFrameworkCore.DbContextOptions<DBMasterContext>(), this.configuration);
            serviceProvider.Setup((o) => o.GetService(typeof(IEntityContext))).Returns(this.dbMasterContext);

            this.authorService = new AuthorService(this.dbMasterContext, this.mapper, searchUtil.Object, uowProvider.Object);
        }

        [Fact]
        public void TestGetAll()
        {
            var list = this.authorService.GetAll();

            if (list.Count > 0) {
                foreach (var author in list) {
                    Assert.NotNull(author.id);
                    Assert.NotNull(author.name);
                    Assert.NotNull(author.email_address);
                }

                Assert.True(list.Count > 0);
            } else {
                Assert.True(list.Count == 0);
            }
        }

        [Fact]
        public void TestControllerGetAll()
        {
            var authorController = new AuthorController(this.authorService);
            var result = authorController.GetAuthor();

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, 200);
            Assert.NotNull(okObjectResult.Value);

            var response = okObjectResult.Value as BaseResponseView<List<AuthorViewModel>>;
            if (response.success) {
                Assert.True(response.success);
                Assert.Equal(response.error_code, 0);
                Assert.Equal(response.error_message, "");

                Assert.NotNull(response.data);
                if (response.data.Count > 0) {
                    foreach (var author in response.data) {
                        Assert.NotNull(author.id);
                        Assert.NotNull(author.name);
                        Assert.NotNull(author.email_address);
                    }
                    Assert.True(response.data.Count > 0);
                } else {
                    Assert.True(response.data.Count == 0);
                }
            } else {
                Assert.False(response.success);
                Assert.NotNull(response.error_code);
                Assert.NotNull(response.error_message);
                Assert.NotNull(response.data);
            }
        }

        [Fact]
        public void TestCrud()
        {
            // Create
            var udid = Guid.NewGuid().ToString();
            var requestCreate = new AuthorRequestViewModel();
            requestCreate.name = "test create";
            requestCreate.email_address = udid + "@mail.com";

            var authorCreate = this.authorService.Create(requestCreate);
            Assert.NotNull(authorCreate.id);
            Assert.NotNull(authorCreate.name);
            Assert.NotNull(authorCreate.email_address);

            this.id = authorCreate.id;

            // Get ById after create a new
            var author = this.authorService.GetById(this.id);
            Assert.NotNull(author.id);
            Assert.NotNull(author.name);
            Assert.NotNull(author.email_address);
            Assert.Equal(author.id, this.id);
            Assert.Equal(author.name, authorCreate.name);
            Assert.Equal(author.email_address, authorCreate.email_address);

            // Update 
            udid = Guid.NewGuid().ToString();
            var requestUpdate = new AuthorRequestViewModel();
            requestUpdate.name = "test update";
            requestUpdate.email_address = udid + "@mail.com";

            var result = this.authorService.Update(this.id, requestUpdate);
            Assert.True(result);

            // GetById after update old
            author = this.authorService.GetById(this.id);
            Assert.NotNull(author.id);
            Assert.NotNull(author.name);
            Assert.NotNull(author.email_address);
            Assert.Equal(author.id, this.id);
            Assert.Equal(author.name, requestUpdate.name);
            Assert.Equal(author.email_address, requestUpdate.email_address);

            // Delete
            result = this.authorService.Delete(this.id);
            Assert.True(result);
            
            // GetById after update old
            var exception = Assert.Throws<Exception>(() => this.authorService.GetById(this.id));
            Assert.Equal(exception.Message, "not found author");
        }

        [Fact]
        public void TestControllerCrud()
        {
            var authorController = new AuthorController(this.authorService);

            // Create
            var udid = Guid.NewGuid().ToString();
            var requestCreate = new AuthorRequestViewModel();
            requestCreate.name = "test create";
            requestCreate.email_address = udid + "@mail.com";
            var result = authorController.CreateAuthor(requestCreate);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, 200);
            Assert.NotNull(okObjectResult.Value);

            var response = okObjectResult.Value as BaseResponseView<AuthorResponseViewModel>;
            if (response.success) {
                Assert.True(response.success);
                Assert.Equal(response.error_code, 0);
                Assert.Equal(response.error_message, "");

                Assert.NotNull(response.data);
                Assert.NotNull(response.data.id);
                Assert.NotNull(response.data.name);
                Assert.NotNull(response.data.email_address);

                this.id = response.data.id;
            } else {
                Assert.False(response.success);
                Assert.NotNull(response.error_code);
                Assert.NotNull(response.error_message);
                Assert.NotNull(response.data);
            }

            // Get ById after create a new
            result = authorController.GetAuthorById(this.id);
            okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, 200);
            Assert.NotNull(okObjectResult.Value);

            var author = okObjectResult.Value as BaseResponseView<AuthorViewModel>;
            if (author.success) {
                Assert.True(author.success);
                Assert.Equal(author.error_code, 0);
                Assert.Equal(author.error_message, "");

                Assert.NotNull(author.data);
                Assert.NotNull(author.data.id);
                Assert.NotNull(author.data.name);
                Assert.NotNull(author.data.email_address);
                Assert.Equal(this.id, author.data.id);
                Assert.Equal(requestCreate.name, author.data.name);
                Assert.Equal(requestCreate.email_address, author.data.email_address);
            } else {
                Assert.False(author.success);
                Assert.NotNull(author.error_code);
                Assert.NotNull(author.error_message);
                Assert.NotNull(author.data);
            }

            // Update
            var requestUpdate = new AuthorRequestViewModel();
            requestUpdate.name = "test create";
            requestUpdate.email_address = udid + "@mail.com";
            result = authorController.UpdateAuthor(this.id, requestUpdate);

            okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, 200);
            Assert.NotNull(okObjectResult.Value);

            var responseUpdate = okObjectResult.Value as BaseResponseView<bool>;
            Assert.True(responseUpdate.data);

            // GetById after update old
            result = authorController.GetAuthorById(this.id);
            okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, 200);
            Assert.NotNull(okObjectResult.Value);

            author = okObjectResult.Value as BaseResponseView<AuthorViewModel>;
            if (author.success) {
                Assert.True(author.success);
                Assert.Equal(author.error_code, 0);
                Assert.Equal(author.error_message, "");

                Assert.NotNull(author.data);
                Assert.NotNull(author.data.id);
                Assert.NotNull(author.data.name);
                Assert.NotNull(author.data.email_address);
            } else {
                Assert.False(author.success);
                Assert.NotNull(author.error_code);
                Assert.NotNull(author.error_message);
                Assert.NotNull(author.data);
            }

            // Delete
            result = authorController.DeleteAuthor(this.id);

            okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(okObjectResult.StatusCode, 200);
            Assert.NotNull(okObjectResult.Value);

            var responseDelete = okObjectResult.Value as BaseResponseView<bool>;
            Assert.True(responseDelete.data);

            // GetById after delete old
            var exception = Assert.Throws<Exception>(() => authorController.GetAuthorById(this.id));
            Assert.Equal(exception.Message, "not found author");
        }
    }
}
