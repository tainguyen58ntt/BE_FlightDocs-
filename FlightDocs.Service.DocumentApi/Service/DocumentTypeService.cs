using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Pagination;
using FlightDocs.Service.DocumentApi.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private IClaimService _claimService;
        private IDocumentPermissionService _documentPermissionService;
        private ITimeService _timeService;
        private IApplicationUserService _applicationUserService;
        private IGroupService _groupService;
        public DocumentTypeService(IDocumentPermissionService documentPermissionService, AppDbContext db, IMapper mapper, IClaimService claimService, ITimeService timeService, IGroupService groupService, IApplicationUserService applicationUserService)
        {
            _db = db;
            _mapper = mapper;
            _documentPermissionService = documentPermissionService;
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
                    var x = await _groupService.GetGroupByIdAsync(group.GroupId);
                    Group g = _mapper.Map<Group>(x);
                    Group groupInMicroservice = null;
                    // check if exist group in db document
                    var checkExistGroup = await _groupService.GetGroupByIdInMicroserviceAsync(g.Id);
                    if (checkExistGroup != null)
                    {

                        groupInMicroservice = _mapper.Map<Group>(checkExistGroup);
                    }
                    else
                    {
                        await _db.Set<Group>().AddAsync(g);
                        await _db.SaveChangesAsync();
                    }



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

        public async Task<DocumentTypeResponse?> GetDocumentTypeByIdAsync(int id)
        {
            var documentType = await _db.DocumentTypes.Where(d => d.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<DocumentTypeResponse>(documentType);
        }

        public async Task<Pagination<DocumentTypeResponse>> GetPaginationAsync(int pageIndex, int pageSize)
        {

            var totalCount = await _db.Set<DocumentType>().CountAsync();
            var items = await _db.Set<DocumentType>()
                .AsNoTracking()



                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            //



            var result = new Pagination<DocumentType>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };




            var rs = _mapper.Map<Pagination<DocumentTypeResponse>>(result);
            foreach (var item in rs.Items)
            {
                var count = await _documentPermissionService.CountGroupPermissionByDocumentTypeIdAsync(item.Id);
                item.NumberOfGroupPermissions = count;
            }
            return rs;


        }
    }
}
