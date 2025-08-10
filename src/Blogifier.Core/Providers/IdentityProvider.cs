using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
    public interface IIdentityProvider
    {
        Task<List<User>> GetAuthors();
        Task<User> FindByEmail(string email);
        Task<bool> Verify(LoginModel model);
        Task<bool> Register(RegisterModel model);
        Task<bool> Add(User author);
        Task<bool> Update(User author);
        Task<bool> ChangePassword(RegisterModel model);
        Task<bool> Remove(int id);
    }

    public class IdentityProvider : IIdentityProvider
    {
        private readonly AppDbContext _db;
        private static string _salt;

        public IdentityProvider(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _salt = configuration.GetSection("Blogifier").GetValue<string>("Salt");
        }


        public Task<bool> Add(User author)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByEmail(string email)
        {
            return await Task.FromResult(_db.Users.Where(a => a.Email == email).FirstOrDefault());
        }

        public Task<List<User>> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(RegisterModel model)
        {
            bool isAdmin = false;
            var author = await _db.Users.Where(a => a.Email == model.Email).FirstOrDefaultAsync();
            if (author != null)
                return false;

            var blog = await _db.Blogs.Include(b => b.Authors).FirstOrDefaultAsync();
            if (blog == null)
            {
                isAdmin = true; // first blog record - set user as admin
                blog = new Blog
                {
                    Title = "Blog Title",
                    Description = "Short Blog Description",
                    Theme = "Standard",
                    ItemsPerPage = 10,
                    DateCreated = DateTime.UtcNow
                };

                _db.Blogs.Add(blog);
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Serilog.Log.Warning($"Error registering new blog: {ex.Message}");
                    return false;
                }
            }

            blog = await _db.Blogs.Include(b => b.Authors).FirstOrDefaultAsync();
            if (blog == null)
                return false;

            author = new User
            {
                Email = model.Email,
                PasswordHash = model.Password.Hash(_salt)             
            };

            // blog.Authors.Add(author);

            return await _db.SaveChangesAsync() > 0;
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User author)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Verify(LoginModel model)
        {

            Serilog.Log.Warning($"Verifying password for {model.Email}");

            User existing = await Task.FromResult(_db.Users.Where(a =>
                a.Email == model.Email).FirstOrDefault());

            if (existing == null)
            {
                Serilog.Log.Warning($"User with email {model.Email} not found");
                return false;
            }

            if (existing.PasswordHash == model.Password.Hash(_salt))
            {
                Serilog.Log.Warning($"Successful login for {model.Email}");
                return true;
            }
            else
            {
                Serilog.Log.Warning($"Password does not match");
                return false;
            }
        }
    }
}
