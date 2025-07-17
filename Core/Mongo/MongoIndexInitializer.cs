using backEnd.Src.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace backEnd.Core.Mongo
{
    public static class MongoIndexInitializer
    {
        public static void InitializeAllIndexes(IMongoDatabase database)
        {
            CreateUserIndexes(database);
            CreatePostIndexes(database);
            CreateThreadIndexes(database);
            CreateCommentIndexes(database);
            CreateTagIndexes(database);
            CreateReportIndexes(database);
        }



        private static void CreateUserIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<User>("users");

            var indexes = new List<CreateIndexModel<User>>
            {
                new(Builders<User>.IndexKeys.Ascending(u => u.Username)),
                new(Builders<User>.IndexKeys.Ascending(u => u.Email)),
                new(Builders<User>.IndexKeys.Ascending(u => u.Role))
            };

            collection.Indexes.CreateMany(indexes);
        }

        private static void CreatePostIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Post>("posts");

            var indexes = new List<CreateIndexModel<Post>>
            {
                new(Builders<Post>.IndexKeys.Ascending(p => p.AuthorId)),
                new(Builders<Post>.IndexKeys.Ascending(p => p.CategoryId)),
                new(Builders<Post>.IndexKeys.Ascending(p => p.TagIds)),
                new(Builders<Post>.IndexKeys.Descending(p => p.CreatedAt)),
                new(Builders<Post>.IndexKeys.Descending(p => p.LastCommentDate))
            };

            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateThreadIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Src.Models.Thread>("threads");

            var indexes = new List<CreateIndexModel<Src.Models.Thread>>
            {
                new(Builders<Src.Models.Thread>.IndexKeys.Ascending(t => t.CategoryId)),
                new(Builders<Src.Models.Thread>.IndexKeys.Descending(t => t.LastPostDate)),
                new(Builders<Src.Models.Thread>.IndexKeys.Descending(t => t.IsPinned)),
                new(Builders<Src.Models.Thread>.IndexKeys
                    .Ascending(t => t.CategoryId)
                    .Descending(t => t.LastPostDate))
            };

            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateCommentIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Comment>("comments");

            var indexes = new List<CreateIndexModel<Comment>>
            {
                new(Builders<Comment>.IndexKeys.Ascending(c => c.PostId)),
                new(Builders<Comment>.IndexKeys.Ascending(c => c.AuthorId)),
                new(Builders<Comment>.IndexKeys.Ascending(c => c.ParentId)),
                new(Builders<Comment>.IndexKeys.Descending(c => c.CreatedAt))
            };

            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateTagIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Src.Models.Tag>("tags");

            var indexes = new List<CreateIndexModel<Src.Models.Tag>>
            {
                new(Builders<Src.Models.Tag>.IndexKeys.Ascending(t => t.Name)),
                new(Builders<Src.Models.Tag>.IndexKeys.Ascending(t => t.Slug)),
                new(Builders<Src.Models.Tag>.IndexKeys.Descending(t => t.UsageCount)),
                new(Builders<Src.Models.Tag>.IndexKeys.Ascending(t => t.IsOfficial))
            };

            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateReportIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Report>("reports");

            var indexes = new List<CreateIndexModel<Report>>
            {
                new(Builders<Report>.IndexKeys.Ascending(r => r.ContentId)),
                new(Builders<Report>.IndexKeys.Ascending(r => r.Status)),
                new(Builders<Report>.IndexKeys
                    .Ascending(r => r.Status)
                    .Descending(r => r.CreatedAt)),
                new(Builders<Report>.IndexKeys.Ascending(r => r.ReporterId))
            };

            collection.Indexes.CreateMany(indexes);
        }
    }
}