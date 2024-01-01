using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models.Dto;
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
        
        public DocumentService(AppDbContext db, IMapper mapper, IClaimService claimService, ITimeService timeService )
        {
            _db = db;
            _mapper = mapper;
            _claimService = claimService;
            _timeService = timeService;
            
        }
    
        public async Task<IEnumerable<DocumentResponseDto>> GetDocumentByFlightIdAsync(string flightId)
        {
            var document = await _db.Documents.Where(d => d.FlightId == flightId).ToListAsync();

            //return ((IEnumerable<DocumentResponseDto>)_mapper.Map<DocumentResponseDto>(document));
            return _mapper.Map<List<DocumentResponseDto>>(document);
            //_mapper.Map<List<ProductViewModel>>(result);
        }

        public async Task<DocumentResponseDto?> GetDocumentByIdAsync(int id)
        {
            var document = await _db.Documents.Where(d => d.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<DocumentResponseDto>(document);
        }
    }
}
