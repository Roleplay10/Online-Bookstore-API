using DataAccessLayer.Data.Entities;
using DataBusinessLogic.DTOs.UserDTOs;
using System.Security.Cryptography;
using System.Text;

namespace DataBusinessLogic.Builders
{
    public interface IUserDTOBuilder
    {
        User createUserCredentials(UserFormDTO userRegisterDTO);
        UserInfoDTO getUserInfo(User userInfo);
    }
    public class UserDTOBuilder : IUserDTOBuilder
    {
        public User createUserCredentials(UserFormDTO userRegisterDTO)
        {
            return new User
            {
                Email = userRegisterDTO.Email,
                HashedPassword = createHashedPassword(userRegisterDTO.Password)
            };
        }
        public UserInfoDTO getUserInfo(User userInfo)
        {
            return new UserInfoDTO
            {
                Username = userInfo.Username,
                Email = userInfo.Email,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Address = userInfo.Address,
                PhoneNumber = userInfo.PhoneNumber
            };
        }
        private string createHashedPassword(string password)
        {
            string base64hashedPasswordBytes;
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
                base64hashedPasswordBytes = Convert.ToBase64String(hashedPasswordBytes);
            }
            
            return base64hashedPasswordBytes;
        }
    }
}
