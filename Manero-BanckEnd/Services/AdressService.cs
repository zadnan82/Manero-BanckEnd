using Manero_BanckEnd.Contexts;
using Manero_BanckEnd.Entities;
using Manero_BanckEnd.Helpers;
using Manero_BanckEnd.Repositories;
using Manero_BanckEnd.Schemas;
using System.Diagnostics;
using Manero_BanckEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using AddressEntity = Manero_BanckEnd.Entities.AddressEntity;
using ServiceResponse = Manero_BanckEnd.Helpers.ServiceResponse;
using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Services
{
    public class AddressService
    {
        private readonly DataContext _dbContext;
        private readonly AddressRepo _addressRepo;

        public AddressService(DataContext dbContext, AddressRepo addressRepo)
        {
            _dbContext = dbContext;
            _addressRepo = addressRepo;
        }

        public async Task<ServiceResponse> CreateNewAddress(string userEmail, AddressCreateRequest request)
        {
            try
            {
                var currentUser = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
                if (currentUser == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "User not found" };
                }

                var addressEntity = new AddressEntity
                {
                    StreetName = request.StreetName,
                    City = request.City,
                    Zipcode = request.Zipcode,

                };

                await _addressRepo.CreateAsync(addressEntity);


                var addressTypeEntity = new AddressTypeEntity
                {
                    UserId = currentUser.Id,
                    AddressId = addressEntity.Id,
                    Title = request.Title,
                };
                _dbContext.AddressTypes.Add(addressTypeEntity);
                _dbContext.SaveChanges();

                if (addressEntity != null)
                {
                    return new ServiceResponse
                    {
                        Status = ResponseStatusCode.CREATED,
                        Message = "Address created",
                        Result = new AddressEntity
                        {
                            City = request.City,
                            Zipcode = request.Zipcode,
                            StreetName = request.StreetName,
                        }
                    };
                }


            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine($"Database error: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Database error occurred" };
            }
            catch (ValidationException ex)
            {
                Debug.WriteLine($"Validation error: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Validation error occurred" };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Unexpected error occurred" };
            }
            return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "creating address error" };
        }

        public async Task<ServiceResponse> GetAddress(string streetName)
        {
            try
            {
                var address = await _dbContext.Addresses.FirstOrDefaultAsync(u => u.StreetName == streetName);

                if (address == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Address not found" };
                }

                var addressEntity = await _dbContext.Addresses.FirstOrDefaultAsync(p => p.Id == address.Id);

                if (addressEntity == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Address not found" };
                }

                var addressType = await _dbContext.AddressTypes.FirstOrDefaultAsync(a => a.AddressId == addressEntity.Id);

                if (addressType == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Address type not found" };
                }

                return new ServiceResponse { Status = ResponseStatusCode.OK, Result = new { Address = addressEntity, Title = addressType.Title } };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting address: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error getting address" };
            }
        }

        public async Task<ServiceResponse> GetAllAddresses(string userEmail)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "User not found" };
                }

                var addresses = _dbContext.AddressTypes
                    .Where(a => a.UserId == user.Id)
                    .Select(a => new { Address = a.Address, Title = a.Title })
                    .ToList();

                if (addresses == null || !addresses.Any())
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Addresses not found" };
                }

                foreach (var address in addresses)
                {
                    Console.WriteLine($"Address: {address.Address.StreetName}, {address.Address.Zipcode}, {address.Address.City}, Title: {address.Title}");
                }

                return new ServiceResponse { Status = ResponseStatusCode.OK, Result = addresses };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting addresses: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error getting addresses" };
            }
        }

        public async Task<ServiceResponse> UpdateAddress(string streetName, string title, AddressUpdateRequest request)
        {
            try
            {
                var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(p => p.StreetName == streetName);
                var addressType = await _dbContext.AddressTypes.FirstOrDefaultAsync(t => t.Title == title);

                if (existingAddress == null || addressType == null)
                {
                    return new ServiceResponse { Status = ResponseStatusCode.NOTFOUND, Message = "Address or title not found" };
                }

                existingAddress.City = request.City;
                existingAddress.StreetName = request.StreetName;
                existingAddress.Zipcode = request.Zipcode;

                addressType.Title = request.Title;

                _dbContext.AddressTypes.Update(addressType);
                _dbContext.Addresses.Update(existingAddress);

                await _dbContext.SaveChangesAsync();

                return new ServiceResponse { Status = ResponseStatusCode.OK, Message = "Address updated successfully" };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating address: {ex}");
                return new ServiceResponse { Status = ResponseStatusCode.ERROR, Message = "Error updating address" };
            }
        }
    }
}
