using API.DTO;
using API.DTO.Permission;
using API.Entity;

namespace API.Services.Interfaces;

public interface IPermissionService
{
    Task<Permission> SetPermissionAsync(PermissionCreateDto dto);
    Task<PermissionDto> GetPermissionAsync(int documentId, int groupId);
    Task<List<PermissionDto>> GetPermissionsByDocumentAsync(int documentId);
    Task<bool> RemovePermissionAsync(int documentId, int groupId);
    Task<string> SavePermissionsAsync(PermissionDto model);
}