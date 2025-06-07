using MetricService.BLL.DTO;
using MetricService.Domain.Models;

namespace MetricService.BLL.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Age = user.Age,
                DateOfBirth = user.DateOfBirth,
                Height = user.Height,
                Id = user.Id,
                Weight = user.Weight,
            };
        }

        public static User ToUser(this UserDTO userDto)
        {
            return new User
            {
                Weight = userDto.Weight,
                Id = userDto.Id,
                Height = userDto.Height,
                DateOfBirth = userDto.DateOfBirth,
            };
        }

        public static IEnumerable<UserDTO> ToUserDTO(this IEnumerable<User> user)
        {
            var result = new List<UserDTO>();
            foreach (var item in user)
            {
                result.Add(ToUserDTO(item));
            }

            return result;
        }
    }
}
