using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Pagination;
using FlightDocs.Service.DocumentApi.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Net.Security;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private IClaimService _claimService;
        private ITimeService _timeService;
        private IApplicationUserService _applicationUserService;

        public DocumentService(IApplicationUserService applicationUserService, AppDbContext db, IMapper mapper, IClaimService claimService, ITimeService timeService)
        {
            _db = db;
            _mapper = mapper;
            _claimService = claimService;
            _timeService = timeService;
            _applicationUserService = applicationUserService;

        }

        public async Task<bool> CreateDocumentForFlight(string flighId, DocumentUploadModel documentUploadModel, int documentTypeId)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            var user = await _applicationUserService.GetUserById(currentUserId);
            //test
            var document = new Document
            {
                FileName = documentUploadModel.File.FileName,
                FileType = documentUploadModel.File.ContentType,
                // Other document properties
                FlightId = flighId,
                CreateBy = user.Name,
                DocumentTypeId = documentTypeId,
                CreateDate = _timeService.GetCurrentTimeInVietnam()

            };

            using (var memoryStream = new MemoryStream())
            {
                await documentUploadModel.File.CopyToAsync(memoryStream);
                document.FileData = memoryStream.ToArray();
            }
            await _db.Set<Document>().AddAsync(document);
            //await _db.SaveChangesAsync(); // Save changes to get the DocumentType.Id

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DocumentResponseDto>> GetDocumentByFlightIdAsync(string flightId)
        {
            var document = await _db.Documents.Where(d => d.FlightId == flightId && d.LastVersion == 1).ToListAsync();

            //return ((IEnumerable<DocumentResponseDto>)_mapper.Map<DocumentResponseDto>(document));
            return _mapper.Map<List<DocumentResponseDto>>(document);
            //_mapper.Map<List<ProductViewModel>>(result);
        }

        public async Task<DocumentResponseDto?> GetDocumentByIdAsync(int id)
        {
            var document = await _db.Documents.Where(d => d.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<DocumentResponseDto>(document);
        }

        public async Task<IEnumerable<DocumentResponseDto>> GetDocumentWasUpdatedByUser(string flightId)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return null;
            var user = await _applicationUserService.GetUserById(currentUserId);
            var updateBy = user.Email;
            var document = await _db.Documents.Where(d => d.FlightId == flightId && d.UpdateBy == updateBy).ToListAsync();

            //return ((IEnumerable<DocumentResponseDto>)_mapper.Map<DocumentResponseDto>(document));
            return _mapper.Map<List<DocumentResponseDto>>(document);
        }

        public async Task<Pagination<DocumentResponseAllDto>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var totalCount = await _db.Set<Document>().CountAsync();
            var items = await _db.Set<Document>()
                .AsNoTracking()
                .Include(d => d.DocumentType)



                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Document>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            //return result;

            return _mapper.Map<Pagination<DocumentResponseAllDto>>(result);
        }
    }
}
