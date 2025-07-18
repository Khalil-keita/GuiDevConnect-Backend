using backEnd.Src.Models;
using MongoDB.Driver;

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
            CreateBadgeIndexes(database);
            CreateMessageIndexes(database);
            CreateNotificationIndexes(database);
            CreatePollIndexes(database);
            CreateReactionIndexes(database);
            CreateUserActivityIndexes(database);
            CreateUserBookmarkIndexes(database);
            CreateUserPreferenceIndexes(database);
            CreateUserStatisticsIndexes(database);
            CreateReportLogIndexes(database);
            CreateBanIndexes(database);
            CreateRankingIndexes(database);
            CreateCategoryIndexes(database);
        }

        private static void CreateUserIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<User>("users");
            var indexes = new List<CreateIndexModel<User>>
            {
                new(Builders<User>.IndexKeys.Ascending(u => u.Username), new CreateIndexOptions { Unique = true }),
                new(Builders<User>.IndexKeys.Ascending(u => u.Email), new CreateIndexOptions { Unique = true }),
                new(Builders<User>.IndexKeys.Ascending(u => u.Role)),
                new(Builders<User>.IndexKeys.Descending(u => u.CreatedAt)),
                new(Builders<User>.IndexKeys.Ascending(u => u.IsBanned))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreatePostIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Post>("posts");
            var indexes = new List<CreateIndexModel<Post>>
            {
                new(Builders<Post>.IndexKeys.Ascending(p => p.AuthorId)),
                new(Builders<Post>.IndexKeys.Ascending(p => p.ThreadId)),
                new(Builders<Post>.IndexKeys.Descending(p => p.CreatedAt)),
                new(Builders<Post>.IndexKeys.Text(p => p.Content)),
                new(Builders<Post>.IndexKeys.Combine(
                    Builders<Post>.IndexKeys.Ascending(p => p.ThreadId),
                    Builders<Post>.IndexKeys.Descending(p => p.IsPinned),
                    Builders<Post>.IndexKeys.Descending(p => p.CreatedAt)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateThreadIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<backEnd.Src.Models.Thread>("threads");
            var indexes = new List<CreateIndexModel<backEnd.Src.Models.Thread>>
            {
                new(Builders<backEnd.Src.Models.Thread>.IndexKeys.Ascending(t => t.CategoryId)),
                new(Builders<backEnd.Src.Models.Thread>.IndexKeys.Ascending(t => t.CreatorId)),
                new(Builders<backEnd.Src.Models.Thread>.IndexKeys.Ascending(t => t.IsLocked)),
                new(Builders<backEnd.Src.Models.Thread>.IndexKeys.Descending(t => t.InitialPostId)),
                new(Builders<backEnd.Src.Models.Thread>.IndexKeys.Descending(t => t.LastPostDate)),
                new(Builders<backEnd.Src.Models.Thread>.IndexKeys.Text(t => t.Title)),
                new(Builders<backEnd.Src.Models.Thread>.IndexKeys.Combine(
                    Builders<backEnd.Src.Models.Thread>.IndexKeys.Ascending(t => t.CategoryId),
                    Builders<backEnd.Src.Models.Thread>.IndexKeys.Descending(t => t.IsLocked),
                    Builders<backEnd.Src.Models.Thread>.IndexKeys.Descending(t => t.IsPinned),
                    Builders<backEnd.Src.Models.Thread>.IndexKeys.Descending(t => t.InitialPostId),
                    Builders<backEnd.Src.Models.Thread>.IndexKeys.Descending(t => t.LastPostDate)))
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
                new(Builders<Comment>.IndexKeys.Descending(c => c.CreatedAt)),
                new(Builders<Comment>.IndexKeys.Combine(
                    Builders<Comment>.IndexKeys.Ascending(c => c.PostId),
                    Builders<Comment>.IndexKeys.Descending(c => c.CreatedAt)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateTagIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<backEnd.Src.Models.Tag>("tags");
            var indexes = new List<CreateIndexModel<backEnd.Src.Models.Tag>>
            {
                new(Builders<backEnd.Src.Models.Tag>.IndexKeys.Ascending(t => t.Name), new CreateIndexOptions { Unique = true }),
                new(Builders<backEnd.Src.Models.Tag>.IndexKeys.Ascending(t => t.Slug), new CreateIndexOptions { Unique = true }),
                new(Builders<backEnd.Src.Models.Tag>.IndexKeys.Descending(t => t.UsageCount))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateReportIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Report>("reports");
            var indexes = new List<CreateIndexModel<Report>>
            {
                new(Builders<Report>.IndexKeys.Ascending(r => r.ContentId)),
                new(Builders<Report>.IndexKeys.Ascending(r => r.ContentType)),
                new(Builders<Report>.IndexKeys.Ascending(r => r.Status)),
                new(Builders<Report>.IndexKeys.Ascending(r => r.ReporterId)),
                new(Builders<Report>.IndexKeys.Ascending(r => r.ModeratorId)),
                new(Builders<Report>.IndexKeys.Combine(
                    Builders<Report>.IndexKeys.Ascending(r => r.Status),
                    Builders<Report>.IndexKeys.Descending(r => r.CreatedAt)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateBadgeIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Badge>("badges");
            var indexes = new List<CreateIndexModel<Badge>>
            {
                new(Builders<Badge>.IndexKeys.Ascending(b => b.Name), new CreateIndexOptions { Unique = true }),
                new(Builders<Badge>.IndexKeys.Ascending(b => b.Rarity))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateMessageIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Message>("messages");
            var indexes = new List<CreateIndexModel<Message>>
            {
                new(Builders<Message>.IndexKeys.Ascending(m => m.SenderId)),
                new(Builders<Message>.IndexKeys.Ascending(m => m.RecipientId)),
                new(Builders<Message>.IndexKeys.Descending(m => m.CreatedAt)),
                new(Builders<Message>.IndexKeys.Combine(
                    Builders<Message>.IndexKeys.Ascending(m => m.SenderId),
                    Builders<Message>.IndexKeys.Ascending(m => m.RecipientId))),
                new(Builders<Message>.IndexKeys.Combine(
                    Builders<Message>.IndexKeys.Ascending(m => m.RecipientId),
                    Builders<Message>.IndexKeys.Descending(m => m.CreatedAt)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateNotificationIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Notification>("notifications");
            var indexes = new List<CreateIndexModel<Notification>>
            {
                new(Builders<Notification>.IndexKeys.Ascending(n => n.UserId)),
                new(Builders<Notification>.IndexKeys.Ascending(n => n.IsRead)),
                new(Builders<Notification>.IndexKeys.Combine(
                    Builders<Notification>.IndexKeys.Ascending(n => n.UserId),
                    Builders<Notification>.IndexKeys.Descending(n => n.CreatedAt))),
                new(Builders<Notification>.IndexKeys.Combine(
                    Builders<Notification>.IndexKeys.Ascending(n => n.UserId),
                    Builders<Notification>.IndexKeys.Ascending(n => n.IsRead)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreatePollIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Poll>("polls");
            var indexes = new List<CreateIndexModel<Poll>>
            {
                new(Builders<Poll>.IndexKeys.Ascending(p => p.ThreadId), new CreateIndexOptions { Unique = true }),
                new(Builders<Poll>.IndexKeys.Descending(p => p.ExpiresAt))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateReactionIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Reaction>("reactions");
            var indexes = new List<CreateIndexModel<Reaction>>
            {
                new(Builders<Reaction>.IndexKeys.Ascending(r => r.UserId)),
                new(Builders<Reaction>.IndexKeys.Combine(
                    Builders<Reaction>.IndexKeys.Ascending(r => r.TargetType),
                    Builders<Reaction>.IndexKeys.Ascending(r => r.TargetId))),
                new(Builders<Reaction>.IndexKeys.Combine(
                    Builders<Reaction>.IndexKeys.Ascending(r => r.TargetId),
                    Builders<Reaction>.IndexKeys.Ascending(r => r.Emoji)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateUserActivityIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<UserActivity>("user_activities");
            var indexes = new List<CreateIndexModel<UserActivity>>
            {
                new(Builders<UserActivity>.IndexKeys.Ascending(a => a.UserId)),
                new(Builders<UserActivity>.IndexKeys.Descending(a => a.CreatedAt)),
                new(Builders<UserActivity>.IndexKeys.Combine(
                    Builders<UserActivity>.IndexKeys.Ascending(a => a.UserId),
                    Builders<UserActivity>.IndexKeys.Descending(a => a.CreatedAt)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateUserBookmarkIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<UserBookmark>("user_bookmarks");
            var indexes = new List<CreateIndexModel<UserBookmark>>
            {
                new(Builders<UserBookmark>.IndexKeys.Ascending(b => b.UserId)),
                new(Builders<UserBookmark>.IndexKeys.Combine(
                    Builders<UserBookmark>.IndexKeys.Ascending(b => b.UserId),
                    Builders<UserBookmark>.IndexKeys.Ascending(b => b.TargetType),
                    Builders<UserBookmark>.IndexKeys.Ascending(b => b.TargetId)),
                    new CreateIndexOptions { Unique = true })
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateUserPreferenceIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<UserPreference>("user_preferences");
            var indexes = new List<CreateIndexModel<UserPreference>>
            {
                new(Builders<UserPreference>.IndexKeys.Ascending(p => p.UserId), new CreateIndexOptions { Unique = true })
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateUserStatisticsIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<UserStatistic>("user_statistics");
            var indexes = new List<CreateIndexModel<UserStatistic>>
            {
                new(Builders<UserStatistic>.IndexKeys.Ascending(s => s.UserId), new CreateIndexOptions { Unique = true }),
                new(Builders<UserStatistic>.IndexKeys.Descending(s => s.Reputation))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateReportLogIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<ReportLog>("report_logs");
            var indexes = new List<CreateIndexModel<ReportLog>>
            {
                new(Builders<ReportLog>.IndexKeys.Ascending(l => l.ReportId)),
                new(Builders<ReportLog>.IndexKeys.Ascending(l => l.ModeratorId)),
                new(Builders<ReportLog>.IndexKeys.Ascending(l => l.ReporterId)),
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateBanIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Ban>("bans");
            var indexes = new List<CreateIndexModel<Ban>>
            {
                new(Builders<Ban>.IndexKeys.Ascending(b => b.UserId)),
                new(Builders<Ban>.IndexKeys.Ascending(b => b.IsActive)),
                new(Builders<Ban>.IndexKeys.Descending(b => b.EndDate)),
                new(Builders<Ban>.IndexKeys.Combine(
                    Builders<Ban>.IndexKeys.Ascending(b => b.UserId),
                    Builders<Ban>.IndexKeys.Ascending(b => b.IsActive)))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateRankingIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Ranking>("rankings");
            var indexes = new List<CreateIndexModel<Ranking>>
            {
                new(Builders<Ranking>.IndexKeys.Ascending(r => r.UserId), new CreateIndexOptions { Unique = true }),
                new(Builders<Ranking>.IndexKeys.Descending(r => r.MinPoints))
            };
            collection.Indexes.CreateMany(indexes);
        }

        private static void CreateCategoryIndexes(IMongoDatabase database)
        {
            var collection = database.GetCollection<Category>("categories");
            var indexes = new List<CreateIndexModel<Category>>
            {
                new(Builders<Category>.IndexKeys.Ascending(c => c.Slug), new CreateIndexOptions { Unique = true }),
                new(Builders<Category>.IndexKeys.Ascending(c => c.Order))
            };
            collection.Indexes.CreateMany(indexes);
        }
    }
}