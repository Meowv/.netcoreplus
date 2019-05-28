using Plus.Services.Dto.Test;
using System.Threading.Tasks;

namespace Plus.Services.Test.Blog
{
    public partial interface IBlogService
    {
        Task<PostDto> Get(int id);
    }
}