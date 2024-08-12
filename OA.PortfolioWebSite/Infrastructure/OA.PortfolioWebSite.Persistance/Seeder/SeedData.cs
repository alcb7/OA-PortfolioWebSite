﻿using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Domain.Entities.Data;
using OA.PortfolioWebSite.Domain.Entities.Auth;
using OA.PortfolioWebSite.Domain.Entities;

public static class SeedData
{
    public static void Initialize(AuthAPIDbContext authDbContext, DataAPIDbContext dataDbContext)
    {
        SeedUsers(authDbContext);
        SeedAboutMe(dataDbContext);
        SeedExperiences( dataDbContext); // İki context'i de geçiriyoruz
        SeedBlogPostsAndComments(authDbContext, dataDbContext);
    }
    public static void SeedUsers(AuthAPIDbContext authDbContext)
    {
        if (!authDbContext.Users.Any())
        {
            var user1 = new User
            {
                Username = "admin",
                Name = "Asdasda",
                SurName = "Xxxxxxxxx",
                PasswordHash = "fakepasswordhash1",
                PasswordSalt = "assasdasdsaszzz",
                Role = "admin",
               
            };

            var user2 = new User
            {
                Username = "commenter",
                Name = "Ahmet",
                SurName = "Xxxxxxxxx",
                PasswordHash = "fakepasswordhash2",
                PasswordSalt = "assasdasdsa",
                Role = "commenter",
                
            };

            authDbContext.Users.AddRange(user1, user2);
            authDbContext.SaveChanges();
        }
    }

    public static void SeedAboutMe(DataAPIDbContext dataDbContext)
    {
        if (!dataDbContext.AboutMe.Any())
        {
            var aboutMe = new AboutMe { Introduction = "Hello, I am...", ImageUrl1 = "image1.jpg", ImageUrl2 = "image2.jpg" };
            dataDbContext.AboutMe.Add(aboutMe);
            dataDbContext.SaveChanges();
        }
    }

    public static void SeedExperiences(DataAPIDbContext dataDbContext)
    {
        if (!dataDbContext.Experiences.Any())
        {
            var experience = new Experiences
            {
                Title = "Software Engineer",
                Company = "Tech Corp",
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 12, 31),
                Description = "Developed software applications.",
                UserId = 0
            };
            dataDbContext.Experiences.Add(experience);
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
    //            Experiences = new List<Experiences>(), // Boş liste
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
    //            Experiences = new List<Experiences>(), // Boş liste
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
    //    if (!dataDbContext.Experiences.Any())
    //    {
    //        var experience = new Experiences
    //        {
    //            Title = "Software Engineer",
    //            Company = "Tech Corp",
    //            StartDate = new DateTime(2024, 1, 1),
    //            EndDate = new DateTime(2024, 12, 31),
    //            Description = "Developed software applications.",
    //            UserId = 1
    //        };
    //        dataDbContext.Experiences.Add(experience);
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
