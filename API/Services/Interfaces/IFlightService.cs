using API.Entity;
using System.Threading.Tasks;
using API.DTO;
using API.DTO.Flight;

namespace API.Services.Interfaces
{
    public interface IFlightService
    {
        // CREATE Flight
        Task<Flight> CreateFlightAsync(FlightCreateDto dto);

        // READ Flight by ID
        Task<Flight> GetFlightByIdAsync(int flightId);

        // READ All Flights
        Task<List<FlightDto>> GetAllFlightsAsync();
        // UPDATE Flight
        Task<Flight> UpdateFlightAsync(int flightId, FlightCreateDto dto);

        // DELETE Flight
        Task<bool> DeleteFlightAsync(int flightId);
        
        // Add document to flight
        Task<bool> AddDocumentToFlight(int flightId, int documentId);
        
        
        Task<Flight> UpdateFlightCompletionStatusAsync(int flightId, bool isCompleted);
    }
}