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
    public class CommentControllerTest
    {
        private CommentsController _commentsController;
        
        private Mock<ICommentRepository> _commentRepositoryMock = new Mock<ICommentRepository>();

        private Mock<IArticleRepository> _articleRepositoryMock = new Mock<IArticleRepository>();


        public CommentControllerTest()
        {
            _commentsController = new CommentsController(_articleRepositoryMock.Object, _commentRepositoryMock.Object);            
        }



        [Fact]
        public async Task Post_ReturnCreatedResult()
        {
            //Arrange
            var articleDbSetMock = Builder<Article>.CreateNew().With(y=> y.Id = 1).Build();
            _articleRepositoryMock.Setup(x=> x.InsertAsync(articleDbSetMock));   
            var commentDbMock = Builder<CommentModel>.CreateNew().Build();
            
            //Act
            var result = await _commentsController.Post(1, commentDbMock);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);


        }

        [Fact]
        public async Task  Post_ReturnBadRequest()
        {
            //Arrange
            var comentsDbMockSet = Builder<Comment>.CreateListOfSize(10)
                                                   .Build()
                                                   .ToAsyncDbSetMock();

            _articleRepositoryMock.Setup(x=> x.InsertAsync(Builder<Article>.CreateNew().Build()));                                                               
            _commentsController.ModelState.AddModelError("Email", "formato inválido");

            //Act
            var result = await _commentsController.Post(1, new CommentModel(){});

            //Assert
           var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
           Assert.IsType<SerializableError>(badRequestResult.Value);

        }

        [Fact]
        public async Task  Post_ReturnNotFound()
        {
            //Arrange
            _articleRepositoryMock.Setup(x=> x.InsertAsync(Builder<Article>.CreateNew().Build()));
            var commentDbMock = Builder<CommentModel>.CreateNew().Build();

            //Act
            var result = await _commentsController.Post(2, commentDbMock);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Get_ReturnEmptyList()
        {
          //Arrange
          var listCommentDbSetMock = Builder<Comment>.CreateListOfSize(10).Build().ToAsyncDbSetMock();
          _commentRepositoryMock.Setup(x=> x.Query()).Returns(listCommentDbSetMock.Object);
         
          //Act
          var result = await _commentsController.Get(1);          

         
          //Assert
          Assert.NotNull(result);
          var listComment = Assert.IsAssignableFrom<IEnumerable<Comment>>(result);
          Assert.Equal(10, listComment.Count());

        }

        [Fact]
        public async Task Get_ReturnListComments()
        {
            //Arrange
          var listCommentDbSetMock = Builder<Comment>.CreateListOfSize(0).Build().ToAsyncDbSetMock();
          _commentRepositoryMock.Setup(x=> x.Query()).Returns(listCommentDbSetMock.Object);
         
          //Act
          var result = await _commentsController.Get(1);          

         
          //Assert
          Assert.NotNull(result);
          var listComment = Assert.IsAssignableFrom<IEnumerable<Comment>>(result);
          Assert.Equal(0, listComment.Count());

        }
        

        
    }
}