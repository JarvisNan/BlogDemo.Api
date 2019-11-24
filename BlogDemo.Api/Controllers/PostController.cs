using BlogDemo.Core.Entities;
using BlogDemo.Core.Interfaces;
using BlogDemo.DB.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public PostController(IPostRepository postRepository, IUnitOfWork unitOfWork, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var posts = await _postRepository.GetAllPostsAsync();

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
