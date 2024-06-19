using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            var citiesDTO = _context.Cities.Select(city => new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State
            });
            return citiesDTO;
        }

        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();

            return new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State
            };
        }

        public CityDto UpdateCity(City city)
        {
            var dbCity = _context.Cities.Find(city.CityId) ??
            throw new Exception("City nor found");


            dbCity.Name = city.Name;
            dbCity.State = city.State;
            _context.SaveChanges();

            return new CityDto
            {
                CityId = dbCity.CityId,
                Name = dbCity.Name,
                State = dbCity.State
            };
        }

    }
}