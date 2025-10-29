using Entities.Dto;
using Entities.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
   public interface IUserService
    {
        Task<UserResponseDto> RegisterUserAsync(UserRegisterDto userDto);
        Task<UserResponseDto?> LoginAsync(string email, string password);

        Task<UserResponseDto?> GetUserByEmailAsync(string email);
    }
}
