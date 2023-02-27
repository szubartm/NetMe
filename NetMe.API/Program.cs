
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NetMe.API.Data;
using NetMe.API.Data.Models;
using NetMe.API.Data.Repositories;
using System.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => 
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNet backend for React", Version = "v1" }));
builder.Services.AddDbContext<PostgreSQLDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DATABASE_URL")));
builder.Services.AddScoped<IPostRepository, PostRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.DocumentTitle = "backend api");
}

app.UseHttpsRedirection();

app.MapGet("/posts", async (IPostRepository postRepository) => await postRepository.GetPostsAsync())
    .WithTags("Post endpoints");

app.MapGet("/posts/{id}", async (Guid id, IPostRepository postRepository) => await postRepository.GetPostByIdAsync(id))
    .WithTags("Post endpoints");

app.MapPost("/posts", async (Post post, IPostRepository postRepository) => await postRepository.CreatePostAsync(post))
    .WithTags("Post endpoints");

app.MapDelete("/posts", (Guid id, IPostRepository postRepository) => postRepository.DeletePost(id))
    .WithTags("Post endpoints");

app.MapPut("/posts", async (Post post, IPostRepository postRepository) => await postRepository.UpdatePostAsync(post))
    .WithTags("Post endpoints");

app.Run();

