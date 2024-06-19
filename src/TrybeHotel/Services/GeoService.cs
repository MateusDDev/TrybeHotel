using System.Net.Http;
using TrybeHotel.Dto;
using TrybeHotel.Repository;

namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
         private readonly HttpClient _client;
        public GeoService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
        }

        private async Task<object> RequestApi<T>(string path)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("User-Agent", "aspnet-user-agent");

            var response = await _client.SendAsync(requestMessage);
            if (!response.IsSuccessStatusCode)
                return null!;

            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;    
        }

        public async Task<object> GetGeoStatus()
        {
            var response = await RequestApi<object>("status.php?format=json");

            if (response == null)
                return default!;

            return response;
        }
        
        public async Task<GeoDtoResponse?> GetGeoLocation(GeoDto geoDto)
        {
            string path = $"search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1";

            var coordinates = await RequestApi<List<GeoDtoResponse>>(path) as List<GeoDtoResponse>;
            if (coordinates == null)
                return default;


            return coordinates.First();
        }

        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var hotels = repository.GetHotels();
            var hotelTasks = hotels
                .Select(async h =>
                {
                    GeoDto hotelGeo = new()
                    {
                        Address = h.Address,
                        City = h.CityName,
                        State = h.State
                    };

                    GeoDtoResponse? coordinates = await GetGeoLocation(geoDto);
                    GeoDtoResponse? hotelCoordinates = await GetGeoLocation(hotelGeo);
                    int distance = 0;
                    if (coordinates != null && hotelCoordinates != null)
                        distance = CalculateDistance(coordinates.lat!, coordinates.lon!, hotelCoordinates.lat!, hotelCoordinates.lon!);

                    return new GeoDtoHotelResponse
                    {
                        HotelId = h.HotelId,
                        Name = h.Name,
                        Address = h.Address,
                        CityName = h.CityName,
                        State = h.State,
                        Distance = distance
                    };
                });

            var geoHotelsDto = await Task.WhenAll(hotelTasks);
            return geoHotelsDto.ToList();
        }

       

        public int CalculateDistance (string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny) {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.',','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.',','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.',','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.',','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double distance = R * c;
            return int.Parse(Math.Round(distance,0).ToString());
        }

        public double radiano(double degree) {
            return degree * Math.PI / 180;
        }

    }
}