namespace backEnd.Src.Dtos
{
    #region User
    public class UserCreateDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Role { get; set; } 
    }

    public class UserUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }

    }

    public class UserDto: AbstractDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Role { get; set; }
        public bool EmailVerified { get; set; }
        public bool IsBanned { get; set; }
        public UserPreferencesDto? Preferences { get; set; }
        public UserStatisticsDto? Statistics { get; set; }
    }

    public class UserLoginResponseDto
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string RoleId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime TokenExpiration { get; set; }
    }

    public class ForgotPasswordDto
    {
        public required string Email { get; init; }
    }

    public class UpdateAvatarDto
    {
        public required IFormFile Avatar { get; set; }
    }

    public class UpdateBioDto
    {
        public required string Bio { get; set; }
    }

    public class ResetPasswordDto
    {
        public required string Email { get; init; }
        public required string Token { get; init; }
        public required string NewPassword { get; init; }
    }

    public class PasswordUpdateDto
    {
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }

    public class EmailVerificationDto
    {
        public required string Token { get; set; }
    }
    #endregion

    #region Preference
    public class UserPreferencesDto: AbstractDto
    {
        public required string Theme { get; set; }
        public required string Language { get; set; }
        public bool EmailNotifications { get; set; }
        public bool Newsletter { get; set; }
        public bool ShowEmail { get; set; }
    }

    public class UserPreferencesUpdateDto
    {
        public string? Theme { get; set; }
        public string? Language { get; set; }
        public bool EmailNotifications { get; set; }
        public bool Newsletter { get; set; }
        public bool ShowEmail { get; set; }
    }
    #endregion

    #region Statistics
    public class UserStatisticsDto: AbstractDto
    {
        public int? PostCount { get; set; }
        public int? CommentCount { get; set; }
        public int? LikesReceived { get; set; }
        public DateTime? LastPostDate { get; set; }
        public DateTime? LastCommentDate { get; set; }
    }
    #endregion

    #region Activity
    public class UserActivityCreateDto
    {
        public required string UserId { get; init; }
        public required string ActivityType { get; init; }
        public required string TargetId { get; init; }
        public required string TargetType { get; init; }
        public string? IpHash { get; init; }
        public string? UserAgent { get; init; }
        public Coordinates? Coordinates { get; init; }
    }

    public class UserActivityDto: AbstractDto
    {
        public required UserDto User { get; set; }
        public required string ActivityType { get; set; }
        public required object Target { get; set; }
        public required string TargetType { get; set; }
        public string? IpHash { get; set; }
        public string? UserAgent { get; set; }
        public Coordinates? Coordinates { get; set; }
    }

    #endregion

    #region Utils
    public class Coordinates
    {
        public Decimal Latitude { get; init; }
        public Decimal Longitude { get; init; }
        public int Acuracy { get; init; } 
    }
    #endregion
}