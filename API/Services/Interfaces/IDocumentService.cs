using API.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;

namespace API.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> CreateDocumentAsync(int flightId, Document document, List<int> permissionGroupIds);
        
        Task<Document> UpdateDocumentAsync(int documentId, Document updatedDocument);
    }
}