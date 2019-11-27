using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class AuthorControllerTest
    {
        
        private AuthorController _authorController;

        private Mock<IAuthorRepository> _authorRepositoryMock = new Mock<IAuthorRepository>();

        public AuthorControllerTest()
        {
            _authorController = new AuthorController(_authorRepositoryMock.Object);
        }


        [Fact]
        public async Task GetReturnsAuthorWithSameId()
        {
            //Arrange
            _authorRepositoryMock.Setup(x=> x.GetAsync(1)).Returns(Task.FromResult<Author>(Builder<Author>.CreateNew().With(x=> x.Id==1).Build()));

            //Act
            var result = await _authorController.Get(1);


            //Assert
            Assert.NotNull(result);

            var objResult = result as OkObjectResult;
            Assert.NotNull(objResult);


            var content = objResult.Value as AuthorModel;
            Assert.NotNull(content);

            Assert.Equal(1, content.Id);

        }        



        [Fact]
        public async Task GetReturnNotFound()
        {
            //Arrange
            _authorRepositoryMock.Setup(x=> x.GetAsync(1)).Returns(Task.FromResult<Author>(null));

            
            //Act
            var result = await _authorController.Get(1);

            //Assert        
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        
        } 


        [Fact]
        public async Task ReturnEmptyList()
        {
             // Arrange
            var authorDbSetMock = Builder<Author>.CreateListOfSize(3).Build().ToAsyncDbSetMock();
            _authorRepositoryMock.Setup(m => m.Query()).Returns(authorDbSetMock.Object);

            // Act
            var result = await _authorController.Search("Invalid");

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as AuthorListModel;
            Assert.NotNull(content);

            Assert.Equal(0, content.Authors.Count());
        }


        [Fact]
        public async Task PostCreateNewAuthor()
        {

            ///Arrange
            var authorDbSetMock = Builder<AuthorModel>.CreateNew().Build();
            

            ///Act
            var result = await _authorController.Post(authorDbSetMock);

            ///Assert
            Assert.NotNull(result);

            var objectResult = Assert.IsType<CreatedResult>(result);
            var model = Assert.IsAssignableFrom<Author>(objectResult.Value); 
            var name = model.Name;
                               
            Assert.Equal("Name1", name);

        }

        [Fact]
        public async Task PostReturnBadRequest()
        {
            //Arrange
            var authorDbSetMock = Builder<AuthorModel>.CreateNew().Build();
            //Act
            var result = await _authorController.Post(model: null);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<BadRequestResult>(result);
            
        }


        [Fact]
        public async Task PutReturnAcceptedResult()
        {
            //Arrange                      
            
           _authorRepositoryMock.Setup(x=> x.GetAsync(1)).Returns(Task.FromResult<Author>(Builder<Author>.CreateNew().With(x=> x.Name = "teste")
                                                                                                                     .With(x=> x.Id = 2).Build()));


            //Act
            var result = await _authorController.Put(2, Builder<AuthorModel>.CreateNew().Build());         
            

            //Assert
            
            Assert.NotNull(result);

            Assert.IsType<AcceptedResult>(result);            
            var contentResult = result as AuthorModel;            

            Assert.NotNull(contentResult);                
            Assert.Equal(2, contentResult.Id);
            
        }



        [Fact]
        public async Task PutReturnBadRequestResult()
        {
            //Arrange                                  
           _authorRepositoryMock.Setup(x=> x.GetAsync(1)).Returns(Task.FromResult<Author>(Builder<Author>.CreateNew().With(x=> x.Name = "teste").Build()));


            //Act
            // var result = await _authorController.Put(2, new AuthorModel(){Id = 2, Name = "teste!", Description= ""});         
            var result = await _authorController.Put(-2, null);
            

            //Assert
            Assert.NotNull(result);

            Assert.IsType<BadRequestResult>(result);            
            
            
        }


        

        [Fact]
        public async Task DeleteReturnNotFoundResult()
        {
            //Arrange
            
            
            //Act
            var result = await _authorController.Delete(-2);
            
            //Assert
             Assert.NotNull(result);              
             Assert.IsType<NotFoundResult>(result);

        }    


        // [Fact]
        // public async Task DeleteReturnOkResult()
        // {
        // //     //Arrange 
        // //     var dbContext = _authorRepositoryMock.

            
            
        // //     //Act
        // //     var result = await _authorController.Delete(3);
            
        // //     //Assert
        // //     Assert.NotNull(result);
        // //    _authorRepositoryMock.Verify(v=> v.DeleteAsync(3), Times.Once());

        // }


    }
}