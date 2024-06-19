using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        private readonly IHotelRepository _repository;
        public RoomRepository(ITrybeHotelContext context, IHotelRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var rooms = _context.Rooms
                .Where(room => room.HotelId == HotelId)
                .Include(rm => rm.Hotel)
                .ThenInclude(hotel => hotel!.City)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Image = r.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = r.Hotel!.HotelId,
                        Name = r.Hotel.Name,
                        Address = r.Hotel.Address,
                        CityId = r.Hotel.CityId,
                        CityName = r.Hotel.City!.Name,
                        State = r.Hotel.City!.State
                    }
                });
            return rooms;
        }

        public RoomDto AddRoom(Room room) {
            _context.Rooms.Add(room);
            _context.SaveChanges();

            var newRoom = _context.Rooms
                .Where(r => r.RoomId == room.RoomId)
                .Include(rm => rm.Hotel)
                .ThenInclude(hotel => hotel!.City)
                .First();

            return new RoomDto
            {
                RoomId = newRoom.RoomId,
                Name = newRoom.Name,
                Capacity = newRoom.Capacity,
                Image = newRoom.Image,
                Hotel = new HotelDto
                {
                    HotelId = newRoom.Hotel!.HotelId,
                    Name = newRoom.Hotel?.Name,
                    Address = newRoom.Hotel?.Address,
                    CityId = newRoom.Hotel!.CityId,
                    CityName = newRoom.Hotel.City!.Name,
                    State = newRoom.Hotel.City!.State
                }
            };
        }

        public void DeleteRoom(int RoomId) {
            var room = _context.Rooms.Find(RoomId) ?? throw new Exception("Room not found");

            _context.Rooms.Remove(room);
        }
    }
}