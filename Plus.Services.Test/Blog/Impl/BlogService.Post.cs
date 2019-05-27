using Plus.AutoMapper;
using Plus.Core.Tests.Repositories;
using Plus.Services.Dto.Test;
using System.Threading.Tasks;

namespace Plus.Services.Test.Blog.Impl
{
    public partial class BlogService : ServiceBase, IBlogService
    {
        private readonly IPostRepository _postRepository;

        public BlogService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostDto> Get(int id)
        {
            var entity = await _postRepository.GetAsync(id);
            return entity.MapTo<PostDto>();
        }
    }
}