using Entities.Domain_Models;
using Entities.Dto;
using Entities.ResponseModel;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IDeliveryBoyRepository _deliveryBoyRepository;
        private readonly IAdminRepository _adminRepository;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ICustomerRepository customerRepository,
            IVendorRepository vendorRepository,
            IDeliveryBoyRepository deliveryBoyRepository,
            IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _customerRepository = customerRepository;
            _vendorRepository = vendorRepository;
            _deliveryBoyRepository = deliveryBoyRepository;
            _adminRepository = adminRepository;
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRegisterDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                PasswordHash = userDto.Password
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            await _roleRepository.AssignRoleAsync(createdUser.UserId, userDto.RoleName);


            switch (userDto.RoleName.ToLower())
            {
                case "customer":
                    await _customerRepository.CreateCustomerAsync(new Customer
                    {
                        UserId = createdUser.UserId
                    });
                    break;

                case "vendor":
                    await _vendorRepository.CreateVendorAsync(new Vendor
                    {
                        UserId = createdUser.UserId
                    });
                    break;

                case "deliveryboy":
                    await _deliveryBoyRepository.CreateDeliveryBoyAsync(new DeliveryBoy
                    {
                        UserId = createdUser.UserId
                    });
                    break;

                case "admin":
                    await _adminRepository.CreateAdminAsync(new Admin
                    {
                        UserId = createdUser.UserId
                    });
                    break;

                default:
                    throw new ArgumentException("Invalid role specified.");
            }
            var createdUserDto = new UserResponseDto
            {
                UniqueKey = createdUser.UserGuid,
                FullName = createdUser.FullName,
                Email = createdUser.Email,
                PhoneNumber = createdUser.PhoneNumber
            };
            return createdUserDto;
        }

        public async Task<UserResponseDto?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.LoginAsync(email, password);

            if (user == null) return null;

            return new UserResponseDto
            {
                UniqueKey = user.UserGuid,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
    }
}
