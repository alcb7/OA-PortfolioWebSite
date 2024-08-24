using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Domain.Entities.Data;
using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Domain.Entities;

public static class SeedData
{
    public static void Initialize(DataAPIDbContext dataDbContext)
    {
        SeedProjects(dataDbContext);

        SeedPersonelInfo(dataDbContext);
        //SeedExperiences( dataDbContext); // İki context'i de geçiriyoruz
        SeedAboutMe(dataDbContext);
        SeedEducations(dataDbContext);
        // SeedBlogPostsAndComments(authDbContext, dataDbContext);
    }

    //public static void SeedUsers(AuthAPIDbContext authDbContext)
    //{
    //    if (!authDbContext.Users.Any())
    //    {
    //        var user1 = new User
    //        {
    //            Username = "admin",
    //            Name = "Asdasda",
    //            SurName = "Xxxxxxxxx",
    //            PasswordHash = "fakepasswordhash1",
    //            PasswordSalt = "assasdasdsaszzz",
    //            Role = "admin",

    //        };

    //        var user2 = new User
    //        {
    //            Username = "commenter",
    //            Name = "Ahmet",
    //            SurName = "Xxxxxxxxx",
    //            PasswordHash = "fakepasswordhash2",
    //            PasswordSalt = "assasdasdsa",
    //            Role = "commenter",

    //        };

    //        authDbContext.Users.AddRange(user1, user2);
    //        authDbContext.SaveChanges();
    //    }
    //}
    public static void SeedProjects(DataAPIDbContext dataDbContext)
    {
        if (!dataDbContext.Projects.Any())
        {
            var projects = new List<Projects>
        {
            new Projects { Title = "Project Alpha", Description = "This is the first project description.", ImageUrl = "alpha_project.jpeg" },
            new Projects { Title = "Project Beta", Description = "This is the second project description.", ImageUrl = "beta_project.jpeg" },
            new Projects{ Title = "Project Gamma", Description = "This is the third project description.", ImageUrl = "gamma_project.jpeg"}
        };

            dataDbContext.Projects.AddRange(projects);
            dataDbContext.SaveChanges();
        }
    }

    public static void SeedAboutMe(DataAPIDbContext dataDbContext)
    {
        if (!dataDbContext.AboutMe.Any())
        {
            var aboutMe = new AboutMe { Introduction = "Hello, I am...", ImageUrl1 = "başfoto.jpeg", ImageUrl2 = "kurs.png" };
            dataDbContext.AboutMe.Add(aboutMe);
            dataDbContext.SaveChanges();
        }
    }
    public static void SeedEducations(DataAPIDbContext dataDbContext)
    {
        // Eğer veritabanında herhangi bir eğitim verisi yoksa
        if (!dataDbContext.Educations.Any())
        {
            var educations = new List<Educations>
        {
            new Educations
            {
                Degree = "Bachelor of Science in Computer Science",
                School = "University of Example",
                StartDate = new DateTime(2015, 9, 1),
                EndDate = new DateTime(2019, 6, 1),
                UserId = 1
            },
            new Educations
            {
                Degree = "Master of Business Administration",
                School = "Business School of Example",
                StartDate = new DateTime(2020, 9, 1),
                EndDate = new DateTime(2022, 6, 1),
                UserId = 1
            },
            new Educations
            {
                Degree = "Doctor of Philosophy in Engineering",
                School = "Example Institute of Technology",
                StartDate = new DateTime(2010, 9, 1),
                EndDate = new DateTime(2014, 6, 1),
                UserId = 2
            }
        };

            dataDbContext.Educations.AddRange(educations);
            dataDbContext.SaveChanges();
        }
    }
    //public static void SeedExperiences(DataAPIDbContext dataDbContext)
    //{
    //    if (!dataDbContext.Experiences.Any())
    //    {
    //        var experience = new BlogPosts
    //        {
    //            Title = "Software Engineer",
    //            Company = "Tech Corp",
    //            StartDate = new DateTime(2024, 1, 1),
    //            EndDate = new DateTime(2024, 12, 31),
    //            Description = "Developed software applications.",
    //            UserId = 0
    //        };
    //        dataDbContext.Experiences.Add(experience);
    //        dataDbContext.SaveChanges();
    //    }
    //}

    public static void SeedPersonelInfo(DataAPIDbContext dataDbContext)
    {
        if (!dataDbContext.PersonalInfo.Any())
        {
            var personalInfo = new PersonalInfo
            {
                Name = "Ali Rıza",
                Surname = "Canbulan",
                About = "John is a software engineer with a passion for developing innovative solutions.",
                BirthDate = new DateTime(2000, 04, 19),
                Adress = "Denizli",
                Email = "canbulan1849@gmail.com",
                MyProperty = "5",
                Phone = "05434190330",




            };

            dataDbContext.PersonalInfo.Add(personalInfo);
            dataDbContext.SaveChanges();
        }
    }

    public static void SeedBlogPostsAndComments(AuthAPIDbContext authDbContext, DataAPIDbContext dataDbContext)
    {
        if (!dataDbContext.BlogPosts.Any())
        {
            var user1 = authDbContext.Users.FirstOrDefault(u => u.Username == "admin");

            if (user1 != null)
            {
                var blogPost = new BlogPosts
                {
                    Title = "My First Blog Post",
                    Content = "This is my first blog post.",
                    PublishDate = DateTime.Now,
                    AuthorId = user1.Id // Doğru AuthorId atandı
                };

                dataDbContext.BlogPosts.Add(blogPost);
                dataDbContext.SaveChanges();

                var comment = new Comments
                {
                    Content = "Great post!",
                    CreatedDate = DateTime.Now,
                    IsApproved = true,
                    BlogPostId = blogPost.Id // Doğru BlogPostId atandı
                };

                dataDbContext.Comments.Add(comment);
                dataDbContext.SaveChanges();
            }
        }
    }

    //public static void SeedUsers(AuthAPIDbContext authDbContext)
    //{
    //    if (!authDbContext.Users.Any())
    //    {
    //        var user1 = new User
    //        {
    //            Username = "admin",
    //            Name = "Asdasda",
    //            SurName = "Xxxxxxxxx",
    //            PasswordHash = "fakepasswordhash1",
    //            PasswordSalt = "assasdasdsaszzz",
    //            Role = "admin",
    //            BlogPosts = new List<BlogPosts>(), // Boş liste
    //            Educations = new List<Educations>(), // Boş liste
    //            Projects = new List<Projects>(), // Boş liste
    //            BlogPosts = new List<BlogPosts>(), // Boş liste
    //            Comments = new List<Comments>() // Boş liste
    //        };

    //        var user2 = new User
    //        {
    //            Username = "commenter",
    //            Name = "Ahmet",
    //            SurName = "Xxxxxxxxx",
    //            PasswordHash = "fakepasswordhash2",
    //            PasswordSalt = "assasdasdsa",
    //            Role = "commenter",
    //            BlogPosts = new List<BlogPosts>(), // Boş liste
    //            Educations = new List<Educations>(), // Boş liste
    //            Projects = new List<Projects>(), // Boş liste
    //            BlogPosts = new List<BlogPosts>(), // Boş liste
    //            Comments = new List<Comments>() // Boş liste
    //        };

    //        authDbContext.Users.AddRange(user1, user2);
    //        authDbContext.SaveChanges();
    //    }
    //}

    //public static void SeedAboutMe(DataAPIDbContext dataDbContext)
    //{
    //    if (!dataDbContext.AboutMe.Any())
    //    {
    //        var aboutMe = new AboutMe { Introduction = "Hello, I am...", ImageUrl1 = "image1.jpg", ImageUrl2 = "image2.jpg" };
    //        dataDbContext.AboutMe.Add(aboutMe);
    //        dataDbContext.SaveChanges();
    //    }
    //}

    //public static void SeedExperiences(DataAPIDbContext dataDbContext)
    //{
    //    if (!dataDbContext.BlogPosts.Any())
    //    {
    //        var experience = new BlogPosts
    //        {
    //            Title = "Software Engineer",
    //            Company = "Tech Corp",
    //            StartDate = new DateTime(2024, 1, 1),
    //            EndDate = new DateTime(2024, 12, 31),
    //            Description = "Developed software applications.",
    //            UserId = 1
    //        };
    //        dataDbContext.BlogPosts.Add(experience);
    //        dataDbContext.SaveChanges();
    //    }
    //}

    //public static void SeedBlogPostsAndComments(DataAPIDbContext dataDbContext)
    //{
    //    if (!dataDbContext.BlogPosts.Any())
    //    {
    //        var blogPost = new BlogPosts
    //        {
    //            Title = "My First Blog Post",
    //            Content = "This is my first blog post.",
    //            PublishDate = DateTime.Now,
    //            //AuthorId = 1
    //        };

    //        var comment = new Comments
    //        {
    //            Content = "Great post!",
    //            CreatedDate = DateTime.Now,
    //            IsApproved = true,
    //            //BlogPostId = 1,
    //        };

    //        dataDbContext.BlogPosts.Add(blogPost);
    //        dataDbContext.Comments.Add(comment);
    //        dataDbContext.SaveChanges();
    //    }
    //}
}
