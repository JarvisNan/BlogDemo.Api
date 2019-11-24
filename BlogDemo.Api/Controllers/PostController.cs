using BlogDemo.Core.Entities;
using BlogDemo.Core.Interfaces;
using BlogDemo.DB.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController :Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostController> _logger;
        private readonly IConfiguration _configuration;

        public PostController(IPostRepository postRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<PostController> logger, 
            IConfiguration configuration)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var posts = await _postRepository.GetAllPostsAsync();

            var giao = _configuration["Key1"]; 
            var giao2 = _configuration.GetSection("Key1").Value;
            _logger.LogError("Get All Post.......");
            
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var post = new Post()
            {
                Author = "admin",
                Body = "123",
                Title = "a",
                LastModified = DateTime.Now
            };
            _postRepository.AddPost(post);

            await _unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
