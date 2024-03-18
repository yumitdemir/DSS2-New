using Forum.Application.Dto;
using Forum.Application.Repositories;
using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<(long? Id, string Error)> CreateUserAsync(
            CreateUserDto user, 
            Role role = Role.User)
        {
            if (!user.ConfirmPassword!.Equals(user.Password))
            {
                return (null, "Passwords do not match!");
            }

            var exist = await _userRepository
                .IsExistingUsernameAsync(user.Username!);

            if (exist)
            {
                return (null, "Username already exists!");
            }

            exist = await _userRepository
               .IsExistingEmailAsync(user.Username!);

            if (exist)
            {
                return (null, "Email already exists!");
            }

            var dbUser = new User
            {
                Email = user.Email!.ToLower(),
                CreateDate = DateTime.Now,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = role,
                Password = GetHash(user.Password!),
            };

            await _userRepository.SaveAsync(dbUser);
            return (dbUser.Id, string.Empty);
        }

        public async Task<(long? Id, string Error)> UpdatedUserAsync(
            long? id,
            UpdateUserDto user)
        {
            var exist = await _userRepository
                .IsExistingUsernameAsync(id!.Value, user.Username!);

            if (exist)
            {
                return (null, "Username already exists!");
            }

            exist = await _userRepository
               .IsExistingEmailAsync(id!.Value, user.Username!);

            if (exist)
            {
                return (null, "Email already exists!");
            }

            var dbUser = await _userRepository.GetByIdAsync(id!.Value);

            if (dbUser is null)
            {
                return (null, "Invalid user id!");
            }

            dbUser.Email = user.Email!.ToLower();
            dbUser.UpdateDate = DateTime.Now;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Username = user.Username;

            await _userRepository.UpdateAsync(dbUser);

            return (dbUser.Id, string.Empty);
        }

        public async Task<(UserDetailsDto? User, string Error)> DeleteUserAsync(
            long? id)
        {
            var user = await _userRepository.GetByIdAsync(id!.Value);

            if (user is null)
            {
                return (null, "Invalid user id!");
            }

            user.IsDeleted = true;

            await _userRepository.UpdateAsync(user);

            var dtoUser = new UserDetailsDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Username = user.Username,
            };

            return (dtoUser, string.Empty);
        }

        public async Task<(IEnumerable<UserShortDto> Users, string Error)> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var dtUsers = users.Select(e => new UserShortDto
            {
                Id = e.Id,
                FullName = string.Format("{0} {1}", e.FirstName, e.LastName),
                Username = e.Username
            })
            .ToArray();

            return (dtUsers, string.Empty);
        }

        public async Task<(UserDetailsDto? User, string Error)> GetUsersAsync(long? userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
            {
                return (null, $"User with id {userId} not found");
            }

            var dtoUser = new UserDetailsDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Username = user.Username,
            };

            return (dtoUser, string.Empty);
        }

        private string GetHash(string text)
        {
            using(var sh256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hashBytes = sh256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes);
            }
        }
    }
}
