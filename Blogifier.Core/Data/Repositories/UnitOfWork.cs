﻿using Blogifier.Core.Data.Interfaces;

namespace Blogifier.Core.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogifierDbContext _db;

        public UnitOfWork(BlogifierDbContext db)
        {
            _db = db;
            Assets = new AssetRepository(_db);
            Profiles = new ProfileRepository(_db);
            Categories = new CategoryRepository(_db);
            BlogPosts = new PostRepository(_db);
            CustomFields = new CustomRepository(_db);
        }

        public IAssetRepository Assets { get; private set; }
        public IProfileRepository Profiles { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IPostRepository BlogPosts { get; private set; }
        public ICustomRepository CustomFields { get; private set; }

        public int Complete()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
