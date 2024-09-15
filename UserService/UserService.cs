using Microsoft.AspNetCore.Identity;
using Models;
using Repository;

namespace Service;

public interface IUserService
{
    Task<Guid> Authenticate(string username, string password);
    Task<bool> CreateUser(string username, string password);
}

public class UserService : IUserService
{
    private readonly IRepository<AppUserEntity> _appUserRepo;
    private PasswordHasher<AppUserEntity> _passwordHasher;

    public UserService(IRepository<AppUserEntity> appUserRepo)
    {
        _appUserRepo = appUserRepo;
    }

    public async Task<Guid> Authenticate(string username, string password)
    {
        _passwordHasher = new PasswordHasher<AppUserEntity>();

        var appUser = (await _appUserRepo.GetAll()).SingleOrDefault(x => x.Username == username);

        if (appUser == null)
        {
            return Guid.Empty;
        }

        var passwordCheck = _passwordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, password);

        if (passwordCheck == PasswordVerificationResult.Success)
            return appUser.Id;

        return Guid.Empty;
    }

    public async Task<bool> CreateUser(string username, string password)
    {
        var users = await _appUserRepo.GetAll();

        if (users.Any(x => x.Username.ToLower() == username.ToLower()))
            return false;

        _passwordHasher = new PasswordHasher<AppUserEntity>();

        var appUser = new AppUserEntity
        {
            Username = username,
        };

        appUser.PasswordHash = _passwordHasher.HashPassword(appUser, password);

        return await _appUserRepo.Create(appUser);
    }
}
