using Microsoft.AspNetCore.Mvc;
using Plus.Services.Dto.Test;
using Plus.Services.Test.Blog;
using Plus.WebApi;
using System.Threading.Tasks;

namespace Plus.Web.Tests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public PostController()
        {
            _blogService = PlusEngine.Instance.Resolve<IBlogService>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<Response<PostDto>> Get(int id)
        {
            var response = new Response<PostDto>
            {
                Result = await _blogService.Get(id)
            };
            return response;
        }
    }
}