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
            _categoryRepositoryMock.Setup(x=> x.GetAsync(1)).Returns(Task.FromResult<Category>(null));


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
            _categoryRepositoryMock.Setup(x=> x.Query()).Returns(categoryDbSetMock.Object);   

            //Act
            var result = await  _categoryController.GetAll();


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
                                                          .Build();             


            //Act
            var result = await _categoryController.Post(new CategoryModel());

            //Assert
            // Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);

            var content = result as CategoryModel;
            Assert.NotNull(content);
            Assert.Equal("Programação", content.Name);

        }


        [Fact]
        public async Task Post_BadRequest()
        {
            //Arrange
            var categoryDbSetMock = Builder<CategoryModel>.CreateNew().With(x=> x.Name = string.Empty).Build();

            //Act

            var result = await _categoryController.Post(new CategoryModel());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);

        }



        [Fact]
        public async Task Put_NotFound()
        {
            //Act
            //Id = 1
            var parameter = Builder<CategoryModel>.CreateNew().With(x=> x.Name = "teste").Build();

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
                                                  .With(x=> x.Name = string.Empty)
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
            _categoryRepositoryMock.Setup(x=> x.GetAsync(1))
                                   .Returns(Task.FromResult<Category>(Builder<Category>
                                   .CreateNew().With(x=> x.Name = "teste")
                                   .Build()));

            // var categoryDbSetMock = Builder<CategoryModel>.CreateNew()
            //                                               .With(x=> x.Id = 1)
            //                                               .With(y=> y.Name="testeAlterado")
            //                                               .Build();
            var categoryDbSetMock = new CategoryModel(){Id = 1 , Name = "testeAlterado"};

            //Arrange
            var result = await _categoryController.Put(1, categoryDbSetMock );

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

            //Arrange
            var result =  await _categoryController.Delete(-2);            

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Delete_OkResult()
        {
            //Act
            
            //Arrange
            var result = await _categoryController.Delete(1);
            
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
        
    }
}