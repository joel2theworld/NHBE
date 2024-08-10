using NeighbourhoodHelp.Model.DTOs;

namespace NeighbourhoodHelp.Data.IRepository
{
    public interface IUserRepository
    {
        Task<CompleteSignUpDto> CreateUserAsync(SignUpDto signUpDto);
        Task<ErrandDto> GetUserByErrandIdAsync(Guid errandId);
        Task<object> Login(LoginDto loginDto);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(string email, string token, string newPassword);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task<string> UpdateUserProfile(string Id, UpdateUserProfileDto userProfileDto);
        Task<GetUserByIdDto> GetUserDetailsByUserId(string userId);
      
    
        Task<List<GetAppUserDto>> GetAllUsers();
    }
}
