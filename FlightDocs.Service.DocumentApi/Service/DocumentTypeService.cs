using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Service.IService;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private IClaimService _claimService;
        private ITimeService _timeService;
        private IApplicationUserService _applicationUserService;

        private IGroupService _groupService;
        public DocumentTypeService(AppDbContext db, IMapper mapper, IClaimService claimService, ITimeService timeService, IGroupService groupService, IApplicationUserService applicationUserService)
        {
            _db = db;
            _mapper = mapper;
            _claimService = claimService;
            _timeService = timeService;
            _applicationUserService = applicationUserService;
            _groupService = groupService;
        }


        public async Task<bool> CreatAsync(DocumentTypeRequest documentTypeRequest)
        {


            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            // conver to dto -> entity
            var documentType = _mapper.Map<DocumentType>(documentTypeRequest);
            // get create by by login - admin role 
            var applicationUser = await _applicationUserService.GetUserById(currentUserId);
            if (applicationUser == null) return false;

            documentType.CreateBy = applicationUser.Email;
            documentType.CreateDate = _timeService.GetCurrentTimeInVietnam();

            // Add DocumentType to the context
            await _db.Set<DocumentType>().AddAsync(documentType);
            await _db.SaveChangesAsync(); // Save changes to get the DocumentType.Id

            // Assign permissions
            if (documentTypeRequest.GroupPermissionRequestDtos != null)
            {
                foreach (var group in documentTypeRequest.GroupPermissionRequestDtos)
                {

                    // check group and insert matching data
                    var x = await _groupService.GetGroupByIdAsync(group.Id);
                    Group g = _mapper.Map<Group>(x);
                    await _db.Set<Group>().AddAsync(g);
                    await _db.SaveChangesAsync();

                    // Create a new DocumentPermissions with the correct DocumentTypeId
                    DocumentPermissions d = new DocumentPermissions
                    {
                        DocumentTypeId = documentType.Id,
                        GroupId = g.Id, // Replace with the correct GroupId
                        PermissionLevel = group.PermissionLevel
                    };

                    // Insert into DocumentPermissions
                    await _db.Set<DocumentPermissions>().AddAsync(d);
                }

                // Save changes after adding DocumentPermissions
                await _db.SaveChangesAsync();
            }

            return true;
        }



    }
}
