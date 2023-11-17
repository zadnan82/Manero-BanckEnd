using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Models;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Manero_BanckEnd.Services
{
    public class ProfileService
    {
        private readonly DataContext _dbContext;
        private readonly ProfileRepo _profileRepo;
        private readonly UserRepo _userRepo;

        public ProfileService(DataContext dbContext, ProfileRepo profileRepo, UserRepo userRepo)
        {
            _dbContext = dbContext;
            _profileRepo = profileRepo;
            _userRepo = userRepo;
        }

        public async Task<ServiceResponse> CreateProfile(string userEmail, ProfileCreateRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "User not found" };
                }

                var existingProfile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (existingProfile != null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Profile already exists for the user" };
                }

                var profileEntity = new ProfileEntity
                {
                    ImageUrl = request.ImageUrl,
                    Location = request.Location,
                    PhoneNumber = request.PhoneNumber,
                    User = user,
                    UserId = user.Id,
                };

                var prof = await _profileRepo.CreateAsync(profileEntity);

               

                if (prof != null)
                {
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;

                    await _userRepo.UpdateAsync(user);
                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.CREATED,
                        Message = "Created Successfully",
                    };
                }


                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error creating profile" };


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating profile: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error creating profile" };
            }
        }

        public async Task<ServiceResponse> GetProfile(string userEmail)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "User not found" };
                }

                var profileEntity = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (profileEntity == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Profile not found" };
                }

                // Optionally, you can create a Profile instance here if needed
                // var profile = new Profile { /* initialize properties */ };

                return new ServiceResponse { Status = ResponseStatusCode.OK, Result = profileEntity };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting profile: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error getting profile" };
            }
        }

        public async Task<ServiceResponse> UpdateProfile(string userEmail, ProfileUpdateRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "User not found" };
                }

                var existingProfile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (existingProfile == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Profile not found" };
                }

                existingProfile.ImageUrl = request.ImageUrl;
                existingProfile.Location = request.Location;
                existingProfile.PhoneNumber = request.PhoneNumber;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;   

                _dbContext.Profiles.Update(existingProfile);
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return new ServiceResponse { Status = ResponseStatusCode.OK, Message = "Profile updated successfully" };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating profile: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error updating profile" };
            }
        }
    }
}
