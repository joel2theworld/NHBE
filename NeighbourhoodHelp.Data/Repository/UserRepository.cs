using Microsoft.EntityFrameworkCore;
using NeighbourhoodHelp.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Model.Entities;
using NeighbourhoodHelp.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NeighbourhoodHelp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NeighbourhoodHelp.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICloudService _cloudService;

        public UserRepository(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper, IEmailService emailService, ICloudService cloudService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailService = emailService;
            _cloudService = cloudService;
        }
        public async Task<CompleteSignUpDto> CreateUserAsync(SignUpDto signUpDto)
        {
         
                var appUser = _mapper.Map<AppUser>(signUpDto);

                var createUserResult = await _userManager.CreateAsync(appUser, signUpDto.Password);
                if (!createUserResult.Succeeded)
                {
                    return new CompleteSignUpDto
                    {
                        Message = "Failed to create user"
                    };
                }

                IdentityResult roleUp = null;

                if (signUpDto.Role.Equals("user", StringComparison.OrdinalIgnoreCase))
                    roleUp = await _userManager.AddToRoleAsync(appUser, "User");
                else
                {
                    roleUp = await _userManager.AddToRoleAsync(appUser, "Agent");
                }

                if (!roleUp.Succeeded)
                {
                    return new CompleteSignUpDto
                    {
                        Message = "Failed to add user to role."
                    };
                }
                    
                Random rand = new Random();

                string newOtp = rand.Next(100000, 999999).ToString(); //Generates a random 6 digit number as OTP
                appUser.Otp = newOtp;

                var email = new EmailDto
                {
                    To = signUpDto.Email,
                    Subject = "Verify Your Email",
                    UserName = signUpDto.FirstName,
                    Otp = newOtp
                };

                await _emailService.SendEmailAsync(email);

                await _context.SaveChangesAsync();

                return new CompleteSignUpDto
                {
                    UserId = appUser.Id,
                    Message = $"Role is {appUser.Role}",
                    Email = appUser.Email
                };
        }
        

        public async Task<ErrandDto> GetUserByErrandIdAsync(Guid errandId)
        {

            var Errands = await _context.Errands.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == errandId);
            var userByErrandId = new ErrandDto
            {
                FirstName = Errands.AppUser.FirstName,
                LastName = Errands.AppUser.LastName,
                Street = Errands.AppUser.Street,
                City = Errands.AppUser.City,
                State = Errands.AppUser.State,
                PhoneNumber = Errands.AppUser.PhoneNumber,
                Email = Errands.AppUser.Email,
               

            };
            return userByErrandId;
        }

        public async Task<object> Login(LoginDto loginDto)
        {

            //check if user is valid
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email); //compares the Email with existing Email

            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return null;

            // Get user role
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            // Generate token
            var token = _tokenService.CreateToken(user);
            var loggedin = new LoggedInUserDto
            {
                token = token,
                email = loginDto.Email,
                role = role,
                id = user.Id
            };

            //return
            return loggedin;

        }

        public async Task<string> ForgotPassword(string email)
        {
            // Find the user by email
            var user = await _context.appUsers.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                // User not found
                return "User not found";
            }

            // Generate a new password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Construct the password reset link with token
            var resetLink = $"http://localhost:3000/resetpassword?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            // You can send an email with the password reset link to the user
            var emailContent = new EmailDto
            {
                To = email,
                Subject = "Password Reset",
                Body = resetLink
            };

            await _emailService.SendForgotPasswordEmailAsync(emailContent);

            return token;
        }

        public async Task<string> ResetPassword(string email, string token, string newPassword)
        {
            // Find the user by email
            var user = await _context.appUsers.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                // User not found
                return "User not found";
            }

            // Reset the password
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (resetPasswordResult.Succeeded)
            {
                return "Password reset successfully";
            }
            else
            {
                // Password reset failed
                return "Failed to reset password";
            }
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false; // User not found
            }

            if (user.Otp.Equals(otp))
            {
                // OTP matched, set IsVerified to true and update the user
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                // Reset the OTP after verification
                user.Otp = "";
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true; // OTP verified successfully
                }

                
            }

            return false; // OTP verification failed
        }

        public async Task<string> UpdateUserProfile(string id, UpdateUserProfileDto userProfileDto)
        {
            var existingUser = await _context.appUsers.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (existingUser == null)
            {
                return null; // or throw an exception, return BadRequest, etc.
            }


            string pictureUrl = existingUser.Image;
            if (userProfileDto.Image != null)
            {
                var picture = await _cloudService.AddPhotoAsync(userProfileDto.Image);
                pictureUrl = picture.Url.ToString();
            }

            // Update user fields
            existingUser.FirstName = userProfileDto.FirstName ?? existingUser.FirstName;
            existingUser.LastName = userProfileDto.LastName ?? existingUser.LastName;
            existingUser.Email = userProfileDto.Email ?? existingUser.Email;
            existingUser.PostalCode = userProfileDto.PostalCode ?? existingUser.PostalCode;
            existingUser.PhoneNumber = userProfileDto.PhoneNumber ?? existingUser.PhoneNumber;
          /*  existingUser.Image = pictureUrl;*/
            existingUser.State = userProfileDto.State ?? existingUser.State;
            existingUser.City = userProfileDto.City ?? existingUser.City;
            existingUser.Street = userProfileDto.Street ?? existingUser.Street;

            if (userProfileDto.Image != null)
            {
                existingUser.Image = pictureUrl;
            }

            await _context.SaveChangesAsync();
            return "Updated Successfully";
        }
         
                    

        

        public async Task<GetUserByIdDto> GetUserDetailsByUserId(string userId)
        {
            var user = await _context.appUsers.FirstOrDefaultAsync(x => x.Id.Equals(userId));

            if (user == null)
            {
                return null;
            }


        
            var userDetails = new GetUserByIdDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PostalCode = user.PostalCode,
                Image = user.Image,
                Street = user.Street,
                City = user.City,
                State = user.State
            };
            return userDetails;
        }
        public async Task<List<GetAppUserDto>> GetAllUsers()
        {
            var users = await _context.appUsers.ToListAsync();
            return _mapper.Map<List<GetAppUserDto>>(users);


        }
    }
}
