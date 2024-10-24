using API.Entity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTO;
using API.DTO.Document;

namespace API.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(DocumentCreateDto dto, IFormFile file, ClaimsPrincipal user);
           Task<Document> GetDocumentByIdAsync(int documentId);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<Document> UpdateDocumentAsync(int documentId, DocumentUpdateDto dto, IFormFile file, ClaimsPrincipal user);

        Task<bool> DeleteDocumentAsync(int documentId);
        
        Task<Document> AddDocumentToFlightAsync(int flightId, DocumentCreateDto dto);
    }
}