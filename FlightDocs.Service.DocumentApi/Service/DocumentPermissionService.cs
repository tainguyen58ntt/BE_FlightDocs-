using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class DocumentPermissionService : IDocumentPermissionService
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private IClaimService _claimService;
        private ITimeService _timeService;
        private IApplicationUserService _applicationUserService;

        public DocumentPermissionService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> CountGroupPermissionByDocumentTypeIdAsync(int documentTypeId)
        {
            var count = await _db.DocumentPermissions
       .Where(d => d.DocumentTypeId == documentTypeId)
       .Select(d => d.GroupId)
       .Distinct()
       .CountAsync();

            //return ((IEnumerable<DocumentResponseDto>)_mapper.Map<DocumentResponseDto>(document));
            return count;
        }
    }
}
