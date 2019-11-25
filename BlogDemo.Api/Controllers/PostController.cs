using AutoMapper;
using BlogDemo.DB.Resources;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlogDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController :Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public PostController(IPostRepository postRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<PostController> logger, 
            IConfiguration configuration,
            IMapper mapper,
            IUrlHelper urlHelper)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name ="GetPosts")]
        public async Task<IActionResult> Get(PostParameters postParameters) 
        {
            var postList = await _postRepository.GetAllPostsAsync(postParameters);

            //转化实体
            var postResources = _mapper.Map <IEnumerable<Post>, IEnumerable<PostResource>> (postList);

            //前一页
            var previousPageLink = postList.HasPrevious ?
                CreatePostUri(postParameters,
                    PaginationResourceUriType.PreviousPage) : null;
            //后一页
            var nextPageLink = postList.HasNext ?
                CreatePostUri(postParameters,
                    PaginationResourceUriType.NextPage) : null;

            var meta = new
            {
                PageSize = postList.PageSize,
                PageIndex = postList.PageIndex,
                TotalItemsCount = postList.TotalItemsCount,
                PageCount = postList.PageCount,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(meta, new JsonSerializerSettings 
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver() //转化数据不管大小写，首字母都转化为小写
            }));
            //读取配置文件方法
            var giao = _configuration["Key1"]; 
            var giao2 = _configuration.GetSection("Key1").Value;
            _logger.LogError("Get All Post.......");
            
            return Ok(postResources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(); //没有找到返回404
            }
            //转化实体
            var postResource = _mapper.Map<Post, PostResource>(post);

            return Ok(postResource);
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


        private string CreatePostUri(PostParameters parameters, PaginationResourceUriType uriType)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var previousParameters = new
                    {
                        pageIndex = parameters.PageIndex - 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        //title = parameters.Title
                    };
                    return _urlHelper.Link("GetPosts", previousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        //title = parameters.Title
                    };
                    return _urlHelper.Link("GetPosts", nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        //title = parameters.Title
                    };
                    return _urlHelper.Link("GetPosts", currentParameters);
            }
        }
    }
}
