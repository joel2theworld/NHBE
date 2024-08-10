
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NeighbourhoodHelp.Model.DTOs;
using NeighbourhoodHelp.Model.Entities;

namespace NeighbourhoodHelp.Core.IServices
{
    public interface IUserServices
    {
        Task<CompleteSignUpDto> UserSignUpAsync(SignUpDto signUpDto);
        Task<ErrandDto> GetUserByErrandIdAsync(Guid errandId);

        Task<List<GetAppUserDto>> GetAllUsers();
     
        Task<object> LoginService(LoginDto loginDto);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(string email, string token, string newPassword);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task<string> UpdateUserProfile(string Id, UpdateUserProfileDto userProfileDto);
        Task<GetUserByIdDto> GetUserDetailsByUserId(string userId);
    }
}
