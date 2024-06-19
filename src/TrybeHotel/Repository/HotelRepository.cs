using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            var hotels = _context.Hotels
                .Include(hotel => hotel.City)
                .Select(h => new HotelDto
                {
                    HotelId = h.HotelId,
                    Name = h.Name,
                    Address = h.Address,
                    CityId = h.CityId,
                    CityName = h.City!.Name,
                    State = h.City!.State
                }).ToList();
            return hotels;
        }
        
        public HotelDto AddHotel(Hotel hotel)
        {
            var newHotel = _context.Hotels.Add(hotel).Entity;
            _context.SaveChanges();

            return _context.Hotels
                .Where(ht => ht.HotelId == hotel.HotelId)
                .Include(hotel => hotel.City)
                .Select(h => new HotelDto
                {
                    HotelId = h.HotelId,
                    Name = h.Name,
                    Address = h.Address,
                    CityId = h.CityId,
                    CityName = h.City!.Name,
                    State = h.City!.State
                }).First();
        }
    }
}