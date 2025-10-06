using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Identity.Domain.Abtractions;
using Identity.Domain.Entities;
using Identity.Domain.Enums;

namespace Identity.Application.Services
{
    public class UserCommands : IUserCommands
    {
        private readonly Domain.Abtractions.IUserRepository _repo;
        private readonly Domain.Abtractions.IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;
        private readonly IFileStorage _fileStorage;
        public UserCommands(IUserRepository repo, IUnitOfWork uow, IWebHostEnvironment env, IFileStorage fileStorage)
        {
            _repo = repo;
            _uow = uow;
            _env = env;
            _fileStorage = fileStorage;
        }
        public Task ChangePasswordAsync(int userId, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken = default)
        {
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.UserPassword);

            var user = User.Create(request.UserEmail, request.UserPhone, hashPassword);

            await _repo.AddAsync(user, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            string? avatarUrl = null;
            if (request.Avatar != null)
            {
                var fileExt = Path.GetExtension(request.Avatar.FileName);
                var fileName = $"avatar_{user.UserId}{fileExt}";
                using (var stream = request.Avatar.OpenReadStream())
                {
                    avatarUrl = await _fileStorage.SaveFileAsync($"uploads/users/{user.UserId}", fileName, stream, cancellationToken);
                }
            }

            string? cicUrl = null;
            if (request.CitizenIdCard != null)
            {
                var fileExt = Path.GetExtension(request.CitizenIdCard.FileName);
                var fileName = $"cic_{user.UserId}{fileExt}";
                using var stream = request.CitizenIdCard.OpenReadStream();
                cicUrl = await _fileStorage.SaveFileAsync(
                    $"uploads/users/{user.UserId}",
                    fileName,
                    stream,
                    cancellationToken
                );
            }

            user.AddProfile(
                request.UserFullName,
                request.UserAddress,
                request.UserBirthday,
                avatarUrl,
                cicUrl
            );

            await _uow.SaveChangesAsync(cancellationToken);

            return user.UserId;
        }

        //(string fullname, string address, DateTime? birthday, string? avatarUrl, string? citizenIdCard)



        public Task DeleteUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DisableUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task EnableUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UnverifyUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(CreateUserDto request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task VerifyUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
