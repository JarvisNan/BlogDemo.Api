using BlogDemo.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.DB.DataBase
{
    /// <summary>
    /// 准备原始数据
    /// </summary>
    public class MyContextSeed
    {
        public static async Task SeedAsync(MyContext myContext, ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                if (!myContext.Posts.Any())
                {
                    myContext.Posts.AddRange
                    (
                        new List<Post>{ 
                            new Post{ 
                                Title = "Post Title 1",
                                Body = "Post Body 1",
                                Author = "Giao 1",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 2",
                                Body = "Post Body 2",
                                Author = "Giao 2",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 3",
                                Body = "Post Body 3",
                                Author = "Giao 3",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 4",
                                Body = "Post Body 4",
                                Author = "Giao 4",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 5",
                                Body = "Post Body 5",
                                Author = "Giao 5",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 6",
                                Body = "Post Body 6",
                                Author = "Giao 6",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 7",
                                Body = "Post Body 7",
                                Author = "Giao 7",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 8",
                                Body = "Post Body 8",
                                Author = "Giao 8",
                                LastModified = DateTime.Now
                            }
                        }    
                    );
                    await myContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<MyContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(myContext, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
