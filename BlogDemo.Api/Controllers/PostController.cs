using BlogDemo.DB.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController :Controller
    {
        private readonly MyContext _myContext;

        public PostController(MyContext myContext)
        {
            _myContext = myContext;
        }

        public async Task<IActionResult> Get() 
        {
            var posts = await _myContext.Posts.ToListAsync();
            return Ok(posts);
        }
    }
}
