using API.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;

namespace API.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(DocumentCreateDto dto, string filePath);
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<Document> UpdateDocumentAsync(int documentId, DocumentUpdateDto dto);
        Task<bool> DeleteDocumentAsync(int documentId);
        
        Task<Document> AddDocumentToFlightAsync(int flightId, DocumentCreateDto dto);
    }
}