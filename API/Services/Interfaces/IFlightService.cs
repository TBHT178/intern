using API.Entity;
using System.Threading.Tasks;
using API.DTO;

namespace API.Services.Interfaces
{
    public interface IFlightService
    {
        // CREATE Flight
        Task<Flight> CreateFlightAsync(FlightCreateDto dto);

        // READ Flight by ID
        Task<Flight> GetFlightByIdAsync(int flightId);

        // READ All Flights
        Task<IEnumerable<Flight>> GetAllFlightsAsync();

        // UPDATE Flight
        Task<Flight> UpdateFlightAsync(int flightId, FlightCreateDto dto);

        // DELETE Flight
        Task<bool> DeleteFlightAsync(int flightId);
    }
}