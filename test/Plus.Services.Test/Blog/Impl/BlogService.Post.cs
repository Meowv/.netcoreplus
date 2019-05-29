using Plus.AutoMapper;
using Plus.Core.Tests.Repositories;
using Plus.Services.Dto.Test;
using System.Threading.Tasks;

namespace Plus.Services.Test.Blog.Impl
{
    public partial class BlogService : ServiceBase, IBlogService
    {
        private readonly IPostRepository _postRepository;

        private readonly IArticleRepository _articleRepository;

        public BlogService(IPostRepository postRepository, IArticleRepository articleRepository)
        {
            _postRepository = postRepository;
            _articleRepository = articleRepository;

        }

        public async Task<PostDto> Get(int id)
        {
            var entity = await _postRepository.GetAsync(id);
            return entity.MapTo<PostDto>();
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            var entity = await _articleRepository.FirstOrDefaultAsync(x => x.NumId == id);
            return entity.MapTo<ArticleDto>();
        }
    }
}