using AutoMapper;
using backEnd.Src.Dtos;
using backEnd.Src.Interfaces;
using backEnd.Utils;
using BackEnd.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IUser user) : ControllerBase
    {
        private readonly IUser _user = user;

        #region User
        [HttpGet]
        public async Task<IActionResult> AllAsync()
        {
            ResponseData? response;
            try
            {
                var users = await _user.AllAsync();
                response = ResponseData.Success("Utilisateurs récuperés avec succès", users);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserCreateDto userCreateDto)
        {
            ResponseData? response;
            try
            {
                await _user.CreateAsync(userCreateDto);
                response = ResponseData.Success("Utilisateur crée avec succès.");
                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpDelete("{user_id}")]
        public async Task<IActionResult> DeleteAsync(string user_id)
        {
            ResponseData? response;
            try
            {
                await _user.DeleteAsync(user_id);
                response = ResponseData.Success("Utilisateur supprimé avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            ResponseData? response;
            try
            {
                await _user.ForgotPassword(forgotPasswordDto);
                response = ResponseData.Success("Vous allez récevoir un mail de réinitialisation.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetAsync(string user_id)
        {
            ResponseData? response;
            try
            {
                var user = await _user.GetAsync(user_id);
                response = ResponseData.Success("Utilisateur récuperé avec succès.", user);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            ResponseData? response;
            try
            {
                await _user.ResetPassword(resetPasswordDto);
                response = ResponseData.Success("Mot de passe réinitialisé succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (BadRequestException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("{user_id}/set-avatar")]
        public async Task<IActionResult> SetAvatar(string user_id, [FromForm] UpdateAvatarDto updateAvatarDto)
        {
            ResponseData? response;
            try
            {
                await _user.SetAvatar(user_id, updateAvatarDto);
                response = ResponseData.Success("Avatar mise a jour avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (BadRequestException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("{user_id}/set-bio")]
        public async Task<IActionResult> SetBio(string user_id, UpdateBioDto updateBioDto)
        {
            ResponseData? response;
            try
            {
                await _user.SetBio(user_id, updateBioDto);
                response = ResponseData.Success("Bio mise a jour avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("{user_id}/set-password")]
        public async Task<IActionResult> SetPassword(string user_id, PasswordUpdateDto passwordUpdateDto)
        {
            ResponseData? response;
            try
            {
                await _user.SetPassword(user_id, passwordUpdateDto);
                response = ResponseData.Success("Mot de passe mise a jour avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (BadRequestException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPut("{user_id}")]
        public async Task<IActionResult> UpdateAsync(string user_id, UserUpdateDto userUpdateDto)
        {
            ResponseData? response;
            try
            {
                await _user.UpdateAsync(user_id, userUpdateDto);
                response = ResponseData.Success("Profil mise a jour avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPut("{user_id}/preferences")]
        public async Task<IActionResult> UpdateUserPreferences(string user_id, UserPreferencesUpdateDto userPreferencesUpdate)
        {
            ResponseData? response;
            try
            {
                await _user.UpdateUserPreferences(user_id, userPreferencesUpdate);
                response = ResponseData.Success("Préferences mise a jour avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("email-verify")]
        public async Task<IActionResult> VerifyEmail(string user_id, EmailVerificationDto emailVerificationDto)
        {
            ResponseData? response;
            try
            {
                await _user.VerifyEmail(user_id, emailVerificationDto);
                response = ResponseData.Success("Email vérifié avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (BadRequestException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }
        #endregion

        #region BookMark
        [HttpGet("bookmarks")]
        public async Task<IActionResult> AllBookmarkAsync()
        {
            ResponseData? response;
            try
            {
                var booksmarks = await _user.AllBookmarkAsync();
                response = ResponseData.Success("Favoris récuperés avec succès", booksmarks);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpGet("{user_id}/bookmarks")]
        public async Task<IActionResult> GetBookmarkByUserAsync(string user_id)
        {
            ResponseData? response;
            try
            {
                var booksmarks = await _user.GetBookmarkByUserAsync(user_id);
                response = ResponseData.Success("Favoris récuperés avec succès", booksmarks);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("bookmarks")]
        public async Task<IActionResult> CreateBookmarkAsync(CreateBookMarkDto bookMarkDto)
        {
            ResponseData? response;
            try
            {
                await _user.CreateBookmarkAsync(bookMarkDto);
                response = ResponseData.Success("Favoris crée avec succès.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpDelete("bookmarks/{bookmark_id}")]
        public async Task<IActionResult> DeleteBookmarkAsync(string bookmark_id)
        {
            ResponseData? response;
            try
            {
                await _user.DeleteBookmarkAsync(bookmark_id);
                response = ResponseData.Success("Favoris supprimé avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpGet("bookmarks/{bookmark_id}")]
        public async Task<IActionResult> GetBookmarkAsync(string bookmark_id)
        {
            ResponseData? response;
            try
            {
                var booksmark = await _user.GetBookmarkAsync(bookmark_id);
                response = ResponseData.Success("Favoris récuperé avec succès.", booksmark);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPut("bookmarks/{bookmark_id}")]
        public async Task<IActionResult> UpdateBookmarkAsync(string bookmark_id, UpdateBookMarkDto bookMarkDto)
        {
            ResponseData? response;
            try
            {
                await _user.UpdateBookmarkAsync(bookmark_id, bookMarkDto);
                response = ResponseData.Success("Favoris mise a jour avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }
        #endregion

        #region Activites
        [HttpGet("activities")]
        public async Task<IActionResult> AllActivityAsync()
        {
            ResponseData? response;
            try
            {
                var activities = await _user.AllActivityAsync();
                response = ResponseData.Success("Activités récuperés avec succès", activities);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpGet("{user_id}/activities")]
        public async Task<IActionResult> GetActivityByUserAsync(string user_id)
        {
            ResponseData? response;
            try
            {
                var activities = await _user.GetBookmarkByUserAsync(user_id);
                response = ResponseData.Success("Activités récuperés avec succès", activities);

                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("activities")]
        public async Task<IActionResult> CreateActivityAsync([FromBody] UserActivityCreateDto activityDto)
        {
            ResponseData? response;
            try
            {
                await _user.CreateActivityAsync(activityDto);
                response = ResponseData.Success("Activité crée avec succès.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpDelete("activities/{activity_id}")]
        public async Task<IActionResult> DeleteActivityAsync(string activity_id)
        {
            ResponseData? response;
            try
            {
                await _user.DeleteActivityAsync(activity_id);
                response = ResponseData.Success("Activité supprimé avec succès.");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpGet("activities/{activity_id}")]
        public async Task<IActionResult> GetActivityAsync(string activity_id)
        {
            ResponseData? response;
            try
            {
                var activity = await _user.GetActivityAsync(activity_id);
                response = ResponseData.Success("Activité récuperé avec succès.", activity);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response = ResponseData.With(ex.Message, ex.StatusCode, ex.Data);
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response = ResponseData.Error(ex);
                return StatusCode((int) response.StatusCode, response);
            }
        }
        #endregion
    }
}
