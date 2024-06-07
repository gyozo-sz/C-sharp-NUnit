using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EntityPractice
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string? Url { get; set; }
        public List<Post> Posts { get; set; } = new();
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = test.db");
        }
    }

    public class Program
    {

        public static void PrintDatabaseContent(int? postId = null)
        {
            using BloggingContext db = new();
            var posts = db.Posts.Include(p => p.Blog).ToList();
            if (postId != null)
            {
                posts = posts.Where(p => p.PostId == postId).ToList();
            }
            int i = 1;
            foreach (var post in posts)
            {
                Console.WriteLine($"Post {i}: {post.Title} ({post.Content}) - {post.Blog?.Url}");
                ++i;
            }
            Console.Write("\n\n");
        }

        public static void Main()
        {
            using (BloggingContext db = new())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Blog scienceBlog = new() { Url = "crazyscience.com" };
                Blog fashionBlog = new() { Url = "dripfits.com" };

                db.Blogs.AddRange(scienceBlog, fashionBlog);

                Post pharaohsSnake = new()
                {
                    Title = "Pharaoh's snake",
                    Content = "Check out the mindblowing sight of burning mercury(II) thiocyanate!",
                    Blog = scienceBlog
                };
                Post floydsTurtleAndHare = new()
                {
                    Title = "Find cycles in lists is easier than ever",
                    Content = "Floyd's Turtle and Hare algorithm provides an easy solution to finding cycles in linked lists",
                    Blog = scienceBlog
                };
                Post metGala = new()
                {
                    Title = "The outfits at Met Gala 2024 outdid themselves once again",
                    Content = "The Met Gala, famous for the celebrities' flamboyant outfits, did not disappoint this year again.",
                    Blog = fashionBlog
                };
                Post genAlphaFashion = new()
                {
                    Title = "Gen alpha fashion stuns everyone",
                    Content = "The alpha generation has brought back many elements of the past, combining them in new and creative ways",
                    Blog = fashionBlog
                };

                db.Posts.AddRange(pharaohsSnake, floydsTurtleAndHare, metGala, genAlphaFashion);
                db.SaveChanges();
            }

            PrintDatabaseContent();

            using (BloggingContext db = new())
            {
                var firstPost = db.Posts.FirstOrDefault();
                if (firstPost != null)
                {
                    firstPost.Title = "Entity Framework revolutionizes DB access";
                    firstPost.Content = "Database management has been cumbersome, but the new, powerful Entity framework makes it super easy for everyone!";
                    db.Update(firstPost);
                    db.SaveChanges();
                }
            }

            Console.WriteLine("Updated first post:");
            PrintDatabaseContent(1);

            using (BloggingContext db = new())
            {
                var firstPost = db.Posts.FirstOrDefault();
                if (firstPost != null)
                {
                    db.Posts.Remove(firstPost);
                    db.SaveChanges();
                }
            }

            Console.WriteLine("Removed first post, full posts list:");
            PrintDatabaseContent();
        }
    }
}