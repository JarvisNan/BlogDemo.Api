using AutoMapper;
using BlogDemo.DB.Resources;
using BlogDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemo.Api.Extensions
{
    /// <summary>
    /// 对象映射
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //对象属性映射，不同属性名方法如下
            CreateMap<Post,PostResource>().ForMember(a => a.UpdateTime, b => b.MapFrom( c => c.LastModified ));
            CreateMap<PostResource, Post>();
        }
    }
}
