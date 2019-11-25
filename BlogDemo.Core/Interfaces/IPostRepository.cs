﻿using BlogDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post> GetPostByIdAsync(int id);

        void AddPost(Post post);
    }
}
