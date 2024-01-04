using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Constraint;
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
        private IGroupService _groupService;

        public DocumentPermissionService(AppDbContext db, IGroupService groupService, IClaimService claimService, ITimeService timeService)
        {
            _db = db;
            _groupService = groupService;
            _claimService = claimService;
            _timeService = timeService;
        }

        public async Task<bool> CheckPermissionCanModifyDocx(int documentTypeId)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            // get group id by userid
            var groupId = await _groupService.GetGroupIdByUserIdAsync(currentUserId);
            var permission = await _db.DocumentPermissions.Where(dp => dp.GroupId == groupId && dp.DocumentTypeId == documentTypeId).FirstOrDefaultAsync();  // and == doycutype id
            if (permission == null) return false;
            if (permission.PermissionLevel == PermissionLevel.ReadAndModify.ToString()) return true;


            return false;

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
