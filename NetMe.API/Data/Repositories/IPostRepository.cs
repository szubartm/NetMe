using NetMe.API.Data.Models;

namespace NetMe.API.Data.Repositories
{
    internal interface IPostRepository
    {
        Task<IResult> CreatePostAsync(Post post);
        Task<IResult> DeletePost(Guid id);
        Task<IResult> GetPostByIdAsync(Guid postId);
        Task<List<Post>> GetPostsAsync();
        Task<IResult> UpdatePostAsync(Post post);
    }
}
