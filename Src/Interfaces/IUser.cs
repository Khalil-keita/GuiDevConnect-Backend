using backEnd.Src.Dtos;
using backEnd.Src.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace backEnd.Src.Interfaces
{
    public interface IUser
    {
        #region User
        Task<List<UserDto>> AllAsync();
        Task<UserDto> GetAsync(string user_id);
        Task<bool> DeleteAsync(string user_id);
        Task<bool> CreateAsync(UserCreateDto userCreateDto);
        Task<bool> UpdateAsync(string user_id, UserUpdateDto userUpdateDto);
        Task<bool> SetBio(string user_id, UpdateBioDto updateBioDto);
        Task<bool> SetAvatar(string user_id, UpdateAvatarDto updateAvatarDto);
        Task<bool> VerifyEmail(string user_id, EmailVerificationDto emailVerificationDto);
        Task<bool> SetPassword(string user_id, PasswordUpdateDto passwordUpdateDto);
        Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<bool> UpdateUserPreferences(string user_id, UserPreferencesUpdateDto userPreferencesUpdate);
        #endregion

        #region User bookmark 
        Task<List<BookMarkDto>> AllBookmarkAsync();
        Task<List<BookMarkDto>> GetBookmarkByUserAsync(string user_id);
        Task<BookMarkDto> GetBookmarkAsync(string bookmark_id);
        Task<bool> DeleteBookmarkAsync(string bookmark_id);
        Task<bool> CreateBookmarkAsync(CreateBookMarkDto bookMarkDto);
        Task<bool> UpdateBookmarkAsync(string bookmark_id, UpdateBookMarkDto bookMarkDto);
        #endregion

        #region User Activity 
        Task<List<UserActivityDto>> AllActivityAsync();
        Task<List<UserActivityDto>> GetActivityByUserAsync(string user_id);
        Task<UserActivityDto> GetActivityAsync(string activity_id);
        Task<bool> DeleteActivityAsync(string activity_id);
        Task<bool> CreateActivityAsync(UserActivityCreateDto user);
        #endregion
    }
}
