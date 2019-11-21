using System;
using System.Web;
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
    public class MediaControllerTest
    {
        

        private MediaController _mediaController;
       

        public MediaControllerTest()
        {
           
        }



        [Fact]
        public async Task Post_MediaReturnBadRequest()
        {
             //Given
             
             
             //When
             
             //Then
        }

        [Fact]
        public async Task Post_MediaReturnCreated()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public async Task Get_MediaReturnSameWithId()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public async Task Get_MediaReturnNotFound()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public async Task Put_ReturnNotFound()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public async Task Put_ReturnBadRequest()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public async Task Put_ReturnOk()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public async Task Delete_ReturnNotFound()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public void Delete_RetrunOk()
        {
        //Given
        
        //When
        
        //Then
        }
        
        
    }
}