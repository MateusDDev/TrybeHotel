using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var user = _context.Users.Where(u => u.Email == login.Email).FirstOrDefault();

            if (user == null)
                throw new Exception("Incorrect e-mail or password");
            
            if (user.Password != login.Password)
                throw new Exception("Incorrect e-mail or password");

            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                UserType = user.UserType
            };
        }
        public UserDto Add(UserDtoInsert user)
        {
            var newUser = _context.Users.Add(new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            }).Entity;
            _context.SaveChanges();

            return new UserDto
            {
                UserId = newUser.UserId,
                Email = newUser.Email,
                Name = newUser.Name,
                UserType = newUser.UserType
            };
        }

        public UserDto? GetUserByEmail(string userEmail)
        {
            var user = _context.Users.Where(u => u.Email == userEmail).FirstOrDefault();

            if (user == null)
                return null;

            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                UserType = user.UserType
            };
        }

        public IEnumerable<UserDto> GetUsers()
        {
           var users = _context.Users
           .Select(user => new UserDto
           {
            UserId = user.UserId,
            Email = user.Email,
            Name = user.Name,
            UserType = user.UserType
           })
           .ToList();
           return users;
        }

    }
}