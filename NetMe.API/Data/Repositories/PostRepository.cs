using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NetMe.API.Data.Models;

namespace NetMe.API.Data.Repositories
{
    internal class PostRepository : IPostRepository
    {
        private readonly PostgreSQLDbContext db;
        public PostRepository(PostgreSQLDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Post>> GetPostsAsync()
        {
            return await db.Posts.ToListAsync();
        }

        public async Task<IResult> GetPostByIdAsync(Guid postId)
        {
            return await db.Posts.FindAsync(postId) is Post post ? Results.Ok(post) : Results.NotFound();
        }

        public async Task<IResult> CreatePostAsync(Post post)
        {
            try
            {
                await db.Posts.AddAsync(post);
                await db.SaveChangesAsync();
                return Results.Created($"/posts/{post.PostId}", post);
            }
            catch (Exception exception)
            {
                return Results.BadRequest(exception);
            }
        }

        public async Task<IResult> UpdatePostAsync(Post post)
        {
            try
            {
                db.Posts.Update(post);
                await db.SaveChangesAsync();
                return Results.Accepted($"/posts/{post.PostId}", post);
            }
            catch (Exception exception)
            {
                return Results.BadRequest(exception);
            }
        }

        public async Task<IResult> DeletePost(Guid id)
        {
            try
            {
               if(await db.Posts.FindAsync(id) is Post post)
                {
                    db.Posts.Remove(post);
                    await db.SaveChangesAsync();
                    return Results.Ok(post);
                }
                return Results.NotFound();
            }
            catch (Exception exception)
            {
                return Results.BadRequest(exception);
            }
        }
    }
}
