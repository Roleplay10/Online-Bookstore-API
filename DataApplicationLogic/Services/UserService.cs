using DataAccessLayer.Data;
using DataAccessLayer.Data.Entities;
using DataBusinessLogic.Builders;
using DataBusinessLogic.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace DataBusinessLogic.Services
{
    public interface IUserService
    {
        Task<string> registerUser([FromBody] UserFormDTO register);
        string loginUser([FromBody] UserFormDTO login);
        UserInfoDTO getUserInfobyId(int id);
        Task<bool> updateUserbyId(int id, UserUpdateDTO user);
    }
    public class UserService : IUserService
    {
        private readonly BookStoreDb _db;
        private readonly IConfiguration _config;
        private readonly IUserDTOBuilder _userDTOBuilder;
        public UserService(BookStoreDb db, IConfiguration config, IUserDTOBuilder userDTOBuilder)
        {
            _db = db;
            _config = config;
            _userDTOBuilder = userDTOBuilder;
        }
        public async Task<string> registerUser([FromBody] UserFormDTO register)
        {
            var check = _db.Users
                .Where(u => u.Email == register.Email)
                .SingleOrDefault();
                if (check is not null) { return null; }
          
            var newUser = _userDTOBuilder.createUserCredentials(register);
            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return GenerateJSONWebToken(newUser);
}
        public string loginUser([FromBody] UserFormDTO login)
        {
            var credentials = _userDTOBuilder.createUserCredentials(login);
            var user = _db.Users
                .Where(u => u.Email == credentials.Email && u.HashedPassword == credentials.HashedPassword)
                .SingleOrDefault();
            if(user is not null)
            {
                return GenerateJSONWebToken(user);
            }
            return null;
        }
        public  UserInfoDTO getUserInfobyId(int id)
        {
            if (id == 0) { return null; }

            var user = _db.Users
                .Where(u => u.UserId == id)
                .SingleOrDefault();

            if(user is null){return null;}

            var _user = _userDTOBuilder.getUserInfo(user);
            return _user;
        }
        public async Task<bool> updateUserbyId(int id, UserUpdateDTO user)
        {
            var userToUpdate = _db.Users
                .Where(u => u.UserId == id)
                .SingleOrDefault();
            if(userToUpdate is null) 
            {
                return false;
            }
            userToUpdate.Username = user.Username;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Address = user.Address;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            await _db.SaveChangesAsync();
            return true;
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,userInfo.Email),
                
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
