using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Controllers;
using blog.Domain;
using blog.Model;
using blog.Repositories;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace blog.tests.Controllers
{
    public class CategoryControllerTest
    {
        private CategoryController _categoryController;

        private Mock<ICategoryRepository> _categoryRepositoryMock = new Mock<ICategoryRepository>();

        public CategoryControllerTest()
        {
            _categoryController = new CategoryController(_categoryRepositoryMock.Object);
        }


        [Fact]
        public async Task Get_NotFound()
        {
            //Arrange
            _categoryRepositoryMock.Setup(x => x.GetAsync(1)).Returns(Task.FromResult<Category>(null));


            //Act
            var result = await _categoryController.Get(1);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_ReturnCategoryWithSameId()
        {
            //Arrange
            _categoryRepositoryMock.Setup(x => x.GetAsync(1)).Returns(Task.FromResult<Category>(Builder<Category>.CreateNew().Build()));


            //Act
            var result = await _categoryController.Get(1);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var content = result as Category;
            Assert.NotNull(content);
            Assert.Equal(1, content.Id);
        }

        [Fact]
        public async Task Get_ReturnListCategories()
        {
            //Arrange
            var categoryDbSetMock = Builder<Category>.CreateListOfSize(10).Build().ToAsyncDbSetMock();
            _categoryRepositoryMock.Setup(x => x.Query()).Returns(categoryDbSetMock.Object);

            //Act
            var result = await _categoryController.GetAll();


            //Assert
            Assert.NotNull(result);

            var content = result as List<Category>;
            Assert.NotNull(content);
            Assert.Equal(10, content.Count());
        }

        [Fact]
        public async Task Post_CreateCategory()
        {
            //Arrange
            var categoryDbSetMock = Builder<CategoryModel>.CreateNew()
                .With(c => c.Name = "Programação")
                .Build();


            //Act
            var result = await _categoryController.Post(categoryDbSetMock);

            
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
          

        }

        [Fact]
        public async Task Post_BadRequest()         
        {
            //Arrange
            var categoryDbSetMock = Builder<CategoryModel>.CreateNew().With(x => x.Name = string.Empty).Build();

            //Act

            var result = await _categoryController.Post(null);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Put_NotFound()
        {
            //Act
            //Id = 1
            var parameter = Builder<CategoryModel>.CreateNew().With(x => x.Name = "teste").Build();

            //Arrange
            var result = await _categoryController.Put(2, parameter);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<NotFoundResult>(result);      
        }

        [Fact]
        public async Task Put_BadRequest()
        {
            //Act
            //Id = 1
            var parameter = Builder<CategoryModel>.CreateNew()
                                                  .With(x => x.Name = string.Empty)
                                                  .Build();

            //Arrange
            var result = await _categoryController.Put(1, parameter);

            //Assert
            Assert.NotNull(result);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Put_OkResult()
        {
            //Act            
            var categoryBuilder = Builder<Category>
                                   .CreateNew()
                                   .Build();

            _categoryRepositoryMock.Setup(x => x.GetAsync(0))
                                   .Returns(Task.FromResult<Category>(categoryBuilder));
            

            //Arrange
            var result = await _categoryController.Put(Builder<int>.CreateNew().Build(), Builder<CategoryModel>.CreateNew().Build());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var content = result as Category;
            Assert.NotNull(content);

            Assert.Equal("testeAlterado", content.Name);
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            //Act   
            var invalidId = -2;
        //   _categoryRepositoryMock.Setup(_categoryController.Delete(invalidId)).Returns(Task.FromResult);

            //Arrange
            var result = await _categoryController.Delete(invalidId);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Delete_OkResult()
        {
            //Act
            _categoryRepositoryMock.Setup(x=> x.GetAsync(1)).Returns(Task.FromResult<Category>(Builder<Category>.CreateNew().With(c => c.Id =1).Build()));

            //Arrange
            var result = await _categoryController.Delete(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

    }
}