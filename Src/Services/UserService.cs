using AutoMapper;
using backEnd.Core.Mongo;
using backEnd.Src.Dtos;
using backEnd.Src.Interfaces;
using backEnd.Src.Models;
using BackEnd.Core.Exceptions;

namespace backEnd.Src.Services
{
    public class UserService(IMongoDbContext dbContext, ILogger<UserService> logger, IMapper mapper) : IUser
    {
        private readonly IMongoDbContext _dbContext = dbContext;
        private readonly ILogger<UserService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        #region User
        public async Task<List<UserDto>> AllAsync()
        {
            try
            {
                _logger.LogInformation($"Debut de la récuperation des utilisateurs.");

                var users = await User.Query(_dbContext)
                        .GetAsync();

                if (users.Count == 0) return [];

                var presferencesTask = UserPreferences.Query(_dbContext).GetAsync();
                var statisticsTask = UserStatistics.Query(_dbContext).GetAsync();

                await Task.WhenAll(presferencesTask, statisticsTask);

                var preferencesDict = (await presferencesTask)
                        ?.ToDictionary(p => p.Id!, p => _mapper.Map<UserPreferencesDto>(p));
                var statisticsDict = (await statisticsTask)
                        ?.ToDictionary(s => s.Id!, s => _mapper.Map<UserStatisticsDto>(s));

                var usersDto = users.Select(u =>
                {
                    var dto = _mapper.Map<UserDto>(u);

                    if (preferencesDict!.TryGetValue(u.PreferencesId!, out var preferences))
                        dto.Preferences = preferences;

                    if (statisticsDict!.TryGetValue(u.StatisticsId!, out var statistics))
                        dto.Statistics = statistics;

                    return dto;
                }).ToList();

                _logger.LogInformation($"Récuperation des utilisateurs terminée avec succès.");

                return usersDto;

            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récuperation des utilisateurs. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> CreateAsync(UserCreateDto userCreateDto)
        {
            //var session = await _dbContext.StartSessionAsync();
            try
            {
                _logger.LogInformation("Debut de la création du utilisateur.");

                var existEmailTask =  User.Query(_dbContext)
                    .Where(u => u.Email == userCreateDto.Email)
                    .FirstAsync();

                var existUserNameTask = User.Query(_dbContext)
                    .Where(u => u.Username == userCreateDto.Username)
                    .FirstAsync();

                await Task.WhenAll(existEmailTask, existUserNameTask);

                var existEmail = (await existEmailTask);
                var existUserName = (await existUserNameTask);

                if (existEmail != null)
                    throw new BadRequestException("Cet email est déja utilisé par un autre membre.");

                if (existUserName != null)
                    throw new BadRequestException("Ce nom utilisateur est déja utilisé par un autre membre.");

                //_ = new UserPreferences(_dbContext).SaveAsync(session);
                //_ = new UserStatistics(_dbContext).SaveAsync(session);
                
                var preferences = new UserPreferences(_dbContext);
                var statistics = new UserStatistics(_dbContext);

               await Task.WhenAll(preferences.SaveAsync(), statistics.SaveAsync());

                var user = new User(_dbContext)
                {
                    Email = userCreateDto.Email,
                    FirstName = userCreateDto.FirstName,
                    LastName = userCreateDto.LastName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password, BCrypt.Net.SaltRevision.Revision2X),
                    Username = userCreateDto.Username,
                    PreferencesId = preferences.Id,
                    StatisticsId = statistics.Id
                };

                //await user.SaveAsync(session);
                await user.SaveAsync();

                //await session.CommitTransactionAsync();

                _logger.LogInformation("Création du utilisateur terminée avec succès.");

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la création du utilisateur. Errer: {ex} ", ex);
                //if(session.IsInTransaction)
                //    await session.AbortTransactionAsync();
                
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string user_id)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            //var session = await _dbContext.StartSessionAsync();
            try
            {
                _logger.LogInformation("Debut de la suppréssion de l'utilisateur.");

                var user = await User.Query(_dbContext).Where(u => u.Id == user_id).FirstAsync()
                    ?? throw new NotFoundException("user", user_id);

                var preferencesTask = UserPreferences.Query(_dbContext)
                    .Where(u => u.Id == user.PreferencesId).FirstAsync();

                var statisticsTask = UserStatistics.Query(_dbContext)
                   .Where(u => u.Id == user.StatisticsId).FirstAsync();

                await Task.WhenAll(statisticsTask, preferencesTask);

                var statistics = (await statisticsTask);
                var preferences = (await preferencesTask);

                await Task.WhenAll(
                    //statistics.DeleteAsync(session);
                     statistics.SetDbContext(_dbContext).DeleteAsync(),

                    //preferences.DeleteAsync(session);
                     preferences.SetDbContext(_dbContext).DeleteAsync(),

                    //user.DeleteAsync(session);
                     user.SetDbContext(_dbContext).DeleteAsync()
                 );

                //await session.CommitTransactionAsync();

                _logger.LogInformation("Suppréssion de l'utilisateur terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la suppréssion de l'utilisateur. Errer: {ex} ", ex);
                //if(session.IsInTransaction)
                //    await session.AbortTransactionAsync();
                throw;
            }
        }

        //NotImplemented
        public Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetAsync(string user_id)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            try
            {
                _logger.LogInformation("Debut de la récupération de l'utilisateur.");

                var user = await User.Query(_dbContext).Where(u => u.Id == user_id).FirstAsync()
                    ?? throw new NotFoundException("user", user_id);

                var preferencesTask = UserPreferences.Query(_dbContext)
                    .Where(u => u.Id == user.PreferencesId).FirstAsync();

                var statisticsTask = UserStatistics.Query(_dbContext)
                   .Where(u => u.Id == user.StatisticsId).FirstAsync();

                await Task.WhenAll(statisticsTask, preferencesTask);

                var statistics = (await statisticsTask);
                var preferences = (await preferencesTask);

                var dto = _mapper.Map<UserDto>(user);
                dto.Statistics = _mapper.Map<UserStatisticsDto>(statistics);
                dto.Preferences = _mapper.Map<UserPreferencesDto>(preferences);

                _logger.LogInformation("Récupération de l'utilisateur terminée avec succès.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récupération de l'utilisateur. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                _logger.LogInformation("Debut de la reinitialisation du mot de passe de l'utilisateur.");

                var user = await User.Query(_dbContext).Where(u => u.Email == resetPasswordDto.Email).FirstAsync()
                    ?? throw new NotFoundException("user", resetPasswordDto.Email);

                if (user.PasswordResetToken != resetPasswordDto.Token)
                    throw new BadRequestException("Le token de réinitialisation est invalid.");

                if (user.PasswordResetExpires!.Value < DateTime.UtcNow)
                    throw new BadRequestException("Le token de réinitialisation est expiré.");

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);

                await user.SetDbContext(_dbContext).SaveAsync();

                _logger.LogInformation("Reinitialisation du mot de passe de l'utilisateur terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la reinitialisation du mot de passe de l'utilisateur. Errer: {ex} ", ex);
                throw;
            }
        }

        //NotImplemented
        public Task<bool> SetAvatar(string user_id, UpdateAvatarDto updateAvatarDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetBio(string user_id, UpdateBioDto updateBioDto)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            try
            {
                _logger.LogInformation("Debut de la mise a jour de la bio de l'utilisateur.");

                var user = await User.Query(_dbContext).Where(u => u.Id == user_id).FirstAsync()
                    ?? throw new NotFoundException("user", user_id);

                user.Bio = updateBioDto.Bio;

                await user.SetDbContext(_dbContext).SaveAsync();

                _logger.LogInformation("Mise a jour de la bio de l'utilisateur terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la mise a jour de la bio de l'utilisateur. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> SetPassword(string user_id, PasswordUpdateDto passwordUpdateDto)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            try
            {
                _logger.LogInformation("Debut de la mise a jour du mot de passe de l'utilisateur.");

                var user = await User.Query(_dbContext).Where(u => u.Id == user_id).FirstAsync()
                    ?? throw new NotFoundException("user", user_id);

                var verify = BCrypt.Net.BCrypt.Verify(passwordUpdateDto.OldPassword, user.PasswordHash);

                if (!verify)
                    throw new BadRequestException("Mot de passe invalid.");

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordUpdateDto.NewPassword);

                await user.SetDbContext(_dbContext).SaveAsync();

                _logger.LogInformation("Mise a jour du mot de passe de l'utilisateur terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la mise a jour du mot de passe de l'utilisateur. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(string user_id, UserUpdateDto userUpdateDto)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            try
            {
                _logger.LogInformation("Debut de la mise a jour de l'utilisateur.");

                var user = await User.Query(_dbContext).Where(u => u.Id == user_id).FirstAsync()
                    ?? throw new NotFoundException("user", user_id);

                user.FirstName = userUpdateDto.FirstName!;
                user.LastName = userUpdateDto.LastName!;
                user.Email = userUpdateDto.Email;

                await user.SetDbContext(_dbContext).SaveAsync();

                _logger.LogInformation("Mise a jour l'utilisateur terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la mise a jour de l'utilisateur. Errer: {ex} ", ex);
                throw;
            }
        }

        //NotImplemented
        public Task<bool> UpdateUserPreferences(string user_id, UserPreferencesUpdateDto userPreferencesUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyEmail(string user_id, EmailVerificationDto emailVerificationDto)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            try
            {
                _logger.LogInformation("Debut de la verification du mail de l'utilisateur.");

                var user = await User.Query(_dbContext).Where(u => u.Id == user_id).FirstAsync()
                    ?? throw new NotFoundException("user", user_id);

                if (user.EmailVerificationToken != emailVerificationDto.Token)
                    throw new BadRequestException("Le token de verification est invalid.");

                if (user.EmailVerificationExpires!.Value < DateTime.UtcNow )
                    throw new BadRequestException("Le token de verification est expiré.");

                user.EmailVerified = true;

                await user.SetDbContext(_dbContext).SaveAsync();

                _logger.LogInformation("Verification du mail de l'utilisateur terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la verification du mail de l'utilisateur. Errer: {ex} ", ex);
                throw;
            }
        }
        #endregion


        #region BookMark
        public async Task<List<BookMarkDto>> AllBookmarkAsync()
        {
            try
            {
                _logger.LogInformation($"Debut de la récuperation des bookmarks.");

                var bookmarks = await UserBookmark.Query(_dbContext)
                        .GetAsync();

                if (bookmarks.Count == 0) return [];

                var usersIds = bookmarks.Where(b => b.UserId != null).Select(b => b.UserId).ToList();
                
                var postIds = bookmarks.Where(b => b.TargetType != "post" && b.TargetId != null)
                        .Select(b => b.TargetId).ToList();

                var threadIds = bookmarks.Where(b => b.TargetType != "thread" && b.TargetId != null)
                       .Select(b => b.TargetId).ToList();

                var threadsTask = Models.Thread.Query(_dbContext).Where(t => threadIds.Contains(t.Id!)).GetAsync();
                var PostTask = Post.Query(_dbContext).Where(p => postIds.Contains(p.Id!)).GetAsync();
                var usersTask =  User.Query(_dbContext).Where(u => usersIds.Contains(u.Id!)).GetAsync();

                await Task.WhenAll(usersTask, PostTask, threadsTask);

                var users = await usersTask;
                var posts = await PostTask;
                var threads = await threadsTask;

                var usersDict = users?.ToDictionary(u => u.Id!, u => _mapper.Map<UserDto>(u));
                var threadsDict = threads?.ToDictionary(t => t.Id!, t => _mapper.Map<ThreadDto>(t));
                var postsDict = posts?.ToDictionary(p => p.Id!, p => _mapper.Map<UserDto>(p));

                var dtos = bookmarks.Select(b => {
                    var dto = _mapper.Map<BookMarkDto>(b);

                    if (usersDict!.TryGetValue(b.UserId, out var user))
                        dto.User = user;

                    if (b.TargetType == "post" && postsDict!.TryGetValue(b.TargetId, out var post))
                        dto.Target = post;

                    if (b.TargetType == "thread" && threadsDict!.TryGetValue(b.TargetId, out var thread))
                        dto.Target = thread;

                    return dto;
                }).ToList();

                _logger.LogInformation($"Récuperation des bookmarks terminée avec succès.");

                return dtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récuperation des bookmarks. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<List<BookMarkDto>> GetBookmarkByUserAsync(string user_id)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            try
            {
                _logger.LogInformation($"Debut de la récuperation des bookmarks pour user {user_id}.");

                var bookmarks = await UserBookmark.Query(_dbContext)
                        .Where(b => b.UserId == user_id)
                        .GetAsync();

                if (bookmarks.Count == 0) return [];

                var usersIds = bookmarks.Where(b => b.UserId != null).Select(b => b.UserId).ToList();

                var postIds = bookmarks.Where(b => b.TargetType != "post" && b.TargetId != null)
                        .Select(b => b.TargetId).ToList();

                var threadIds = bookmarks.Where(b => b.TargetType != "thread" && b.TargetId != null)
                       .Select(b => b.TargetId).ToList();

                var threadsTask = Models.Thread.Query(_dbContext).Where(t => threadIds.Contains(t.Id!)).GetAsync();
                var PostTask = Post.Query(_dbContext).Where(p => postIds.Contains(p.Id!)).GetAsync();
                var usersTask = User.Query(_dbContext).Where(u => usersIds.Contains(u.Id!)).GetAsync();

                await Task.WhenAll(usersTask, PostTask, threadsTask);

                var users = await usersTask;
                var posts = await PostTask;
                var threads = await threadsTask;

                var usersDict = users?.ToDictionary(u => u.Id!, u => _mapper.Map<UserDto>(u));
                var threadsDict = threads?.ToDictionary(t => t.Id!, t => _mapper.Map<ThreadDto>(t));
                var postsDict = posts?.ToDictionary(p => p.Id!, p => _mapper.Map<UserDto>(p));

                var dtos = bookmarks.Select(b => {
                    var dto = _mapper.Map<BookMarkDto>(b);

                    if (usersDict!.TryGetValue(b.UserId, out var user))
                        dto.User = user;

                    if (b.TargetType == "post" && postsDict!.TryGetValue(b.TargetId, out var post))
                    {
                        dto.Target = post;
                        dto.TargetType = b.TargetType;
                    }

                    if (b.TargetType == "thread" && threadsDict!.TryGetValue(b.TargetId, out var thread))
                    {
                        dto.Target = thread;
                        dto.TargetType = b.TargetType;
                    }

                    return dto;
                }).ToList();

                _logger.LogInformation($"Récuperation des bookmarks pour user terminée avec succès.");

                return dtos;

            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récuperation des bookmarks pour user. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> CreateBookmarkAsync(CreateBookMarkDto bookMarkDto)
        {
            try
            {
                _logger.LogInformation("Debut de la création du bookmark.");

                var bookmark = new UserBookmark(_dbContext)
                {
                    TargetId = bookMarkDto.TargetId,
                    TargetType = bookMarkDto.TargetType,
                    UserId = bookMarkDto.UserId,
                    Notes = bookMarkDto.Notes
                };

                await bookmark.SaveAsync();

                _logger.LogInformation("Création du bookmark terminée avec succès.");

                return true;

            }catch(Exception ex)
            {
                _logger.LogError("Erreur lors de la création du bookmark. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> DeleteBookmarkAsync(string bookmark_id)
        {
            if (string.IsNullOrEmpty(bookmark_id))
                throw new ArgumentNullException(nameof(bookmark_id));

            try
            {
                _logger.LogInformation("Debut de la suppréssion du bookmark.");

                var bookmark = await UserBookmark.Query(_dbContext).Where(b => b.Id == bookmark_id).FirstAsync()
                    ?? throw new NotFoundException("bookmark", bookmark_id);

                await bookmark.SetDbContext(_dbContext).DeleteAsync();

                _logger.LogInformation("Suppréssion du bookmark terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la suppréssion du bookmark. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<BookMarkDto> GetBookmarkAsync(string bookmark_id)
        {
            if (string.IsNullOrEmpty(bookmark_id))
                throw new ArgumentNullException(nameof(bookmark_id));

            try
            {
                _logger.LogInformation("Debut de la récupération du bookmark.");

                var bookmark = await UserBookmark.Query(_dbContext).Where(b => b.Id == bookmark_id).FirstAsync()
                    ?? throw new NotFoundException("bookmark", bookmark_id);

                var user = await User.Query(_dbContext).Where(u => u.Id == bookmark.UserId).FirstAsync();

                var dto = _mapper.Map<BookMarkDto>(bookmark);
                dto.User = _mapper.Map<UserDto>(user);

                if (bookmark.TargetType == "post")
                {
                    var post = await Post.Query(_dbContext).Where(p => p.Id == bookmark.TargetId).FirstAsync();
                    dto.Target = _mapper.Map<Post>(post);
                }
                else
                {
                    var thread = await Models.Thread.Query(_dbContext).Where(t => t.Id == bookmark.TargetId).FirstAsync();
                    dto.Target = _mapper.Map<ThreadDto>(thread);
                }

                _logger.LogInformation("Récupération du bookmark terminée avec succès.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récupération du bookmark. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> UpdateBookmarkAsync(string bookmark_id, UpdateBookMarkDto bookMarkDto)
        {
            if (string.IsNullOrEmpty(bookmark_id))
                throw new ArgumentNullException(nameof(bookmark_id));

            try
            {
                _logger.LogInformation("Debut de la mise a jour du bookmark.");

                var bookmark = await UserBookmark.Query(_dbContext).Where(b => b.Id == bookmark_id).FirstAsync()
                    ?? throw new NotFoundException("bookmark", bookmark_id);

                bookmark.Notes = bookMarkDto.Notes;

                await bookmark.SetDbContext(_dbContext).SaveAsync();

                _logger.LogInformation("Mise a jour du bookmark terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la mise a jour du bookmark. Errer: {ex} ", ex);
                throw;
            }
        }
        #endregion


        #region Activity
        public async Task<List<UserActivityDto>> AllActivityAsync()
        {
            try
            {
                _logger.LogInformation($"Debut de la récuperation des activies.");

                var activities = await UserActivity.Query(_dbContext)
                        .GetAsync();

                if (activities.Count == 0) return [];

                var usersIds = activities.Where(a => a.UserId != null).Select(a => a.UserId).ToList();

                var users = await User.Query(_dbContext).Where(u => usersIds.Contains(u.Id!)).GetAsync();
                var usersDict = users?.ToDictionary(u => u.Id!, u => _mapper.Map<UserDto>(u));

                var dtos = activities.Select(a =>
                {
                    var dto = _mapper.Map<UserActivityDto>(a);
                    if (usersDict!.TryGetValue(a.UserId, out var user))
                        dto.User = user;

                    return dto;

                }).ToList();

                _logger.LogInformation($"Récuperation des activities terminée avec succès.");

                return dtos;

            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récuperation des activities. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<List<UserActivityDto>> GetActivityByUserAsync(string user_id)
        {
            if (string.IsNullOrEmpty(user_id))
                throw new ArgumentNullException(nameof(user_id));

            try
            {
                _logger.LogInformation($"Debut de la récuperation des activies pour user {user_id}.");

                var activities = await UserActivity.Query(_dbContext)
                        .Where(b => b.UserId == user_id)
                        .GetAsync();

                if (activities.Count == 0) return [];

                var usersIds = activities.Where(a => a.UserId != null).Select(a => a.UserId).ToList();

                var users = await User.Query(_dbContext).Where(u => usersIds.Contains(u.Id!)).GetAsync();
                var usersDict = users?.ToDictionary(u => u.Id!, u => _mapper.Map<UserDto>(u));

                var dtos = activities.Select(a =>
                {
                    var dto = _mapper.Map<UserActivityDto>(a);
                    if (usersDict!.TryGetValue(a.UserId, out var user))
                        dto.User = user;

                    return dto;

                }).ToList();

                _logger.LogInformation($"Récuperation des activities pour user {user_id}. terminée avec succès.");

                return dtos;

            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récuperation des activities pour user. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> CreateActivityAsync(UserActivityCreateDto activityDto)
        {
            try
            {
                _logger.LogInformation("Debut de la création de l'activité.");

                var activity = new UserActivity(_dbContext)
                {
                    ActivityType = activityDto.ActivityType,
                    TargetId = activityDto.TargetId,
                    TargetType = activityDto.TargetType,
                    UserId = activityDto.UserId,
                    Coordinates = activityDto.Coordinates,
                    IpHash = activityDto.IpHash,
                    UserAgent = activityDto.UserAgent
                };

                await activity.SaveAsync();

                _logger.LogInformation("Création de l'activité terminée avec succès.");

                return true ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la création du bookmark. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<bool> DeleteActivityAsync(string activity_id)
        {
            if (string.IsNullOrEmpty(activity_id))
                throw new ArgumentNullException(nameof(activity_id));

            try
            {
                _logger.LogInformation("Debut de la suppréssion de l'activity.");

                var activity = await UserActivity.Query(_dbContext).Where(a => a.Id == activity_id).FirstAsync()
                    ?? throw new NotFoundException("activity", activity_id);

                await activity.SetDbContext(_dbContext).DeleteAsync();

                _logger.LogInformation("Suppréssion de l'activity terminée avec succès.");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la suppréssion de l'activity. Errer: {ex} ", ex);
                throw;
            }
        }

        public async Task<UserActivityDto> GetActivityAsync(string activity_id)
        {
            if (string.IsNullOrEmpty(activity_id))
                throw new ArgumentNullException(nameof(activity_id));

            try
            {
                _logger.LogInformation("Debut de la récupération de l'activity.");

                var activity = await UserActivity.Query(_dbContext).Where(a => a.Id == activity_id).FirstAsync()
                    ?? throw new NotFoundException("activity", activity_id);

                var user = await User.Query(_dbContext).Where(u => u.Id == activity.UserId).FirstAsync();

                var dto = _mapper.Map<UserActivityDto>(activity);
                dto.User = _mapper.Map<UserDto>(user);

                _logger.LogInformation("Récupération de l'activity terminée avec succès.");

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erreur lors de la récupération de l'activity. Errer: {ex} ", ex);
                throw;
            }
        }
        #endregion
   
    }
}
