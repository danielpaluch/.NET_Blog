using BlogAPI.Entities;
namespace BlogAPI
{
    public class BlogSeeder
    {
        private readonly BlogDbContext dbContext;

        public BlogSeeder(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if(!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    dbContext.SaveChanges();
                }
                if (!dbContext.Blogs.Any())
                {
                    var blogs = GetBlogs();
                    dbContext.Blogs.AddRange(blogs);
                    dbContext.SaveChanges();
                }
                if (!dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    dbContext.Categories.AddRange(categories);
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Blog> GetBlogs()
        {
            var blogs = new List<Blog>()
            {
                new Blog()
                {
                    Title = "Jak pisać w .NET",
                    Description = "Najlepszym sposobem na naukę .NET jest praktyka.",
                    Category = new Category()
                    {
                        Name = "IT"
                    }
                },
                new Blog()
                {
                    Title = "Rynek Pracy",
                    Description = "To zależy od umiejętności wchodzącego na rynek.",
                    Category = new Category()
                    {
                        Name = "Praca"
                    }
                },
                new Blog()
                {
                    Title = "Odnawialne źródła energii",
                    Description = "Zastanawiam się, czy jest to ważne aby stać się eko i zainwestować w panele słoneczne.",
                    Category = new Category()
                    {
                        Name = "Energia"
                    }
                }
            };
            return blogs;
        }
        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "IT"
                },
                new Category()
                {
                    Name = "Praca"
                },
                new Category()
                {
                    Name = "Życie"
                },
                new Category()
                {
                    Name = "Energia"
                },
            };
            return categories;
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
    }
}
