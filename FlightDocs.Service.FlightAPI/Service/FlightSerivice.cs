using AutoMapper;
using FlightDocs.Serivce.FlightAPI.Data;
using FlightDocs.Service.FlightAPI.Models;
using FlightDocs.Service.FlightAPI.Models.Dto;
using FlightDocs.Service.FlightAPI.Pagination;
using FlightDocs.Service.FlightAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Service.FlightAPI.Service
{
    public class FlightSerivice : IFlightService
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;


        public FlightSerivice(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<IEnumerable<FlightResponseDto>> GetFlightAsync()
        {
            var flights = await _db.Fligh.ToListAsync();
            return _mapper.Map<List<FlightResponseDto>>(flights);
        }

        public async Task<FlightResponseDto?> GetFlightByIdAsync(string id)
        {
            var flight = await _db.Fligh.Where(f => f.FlightId == id).FirstOrDefaultAsync();
            return _mapper.Map<FlightResponseDto>(flight);
        }

        public async Task<Pagination<FlightResponseDto>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            //var result = await _unitOfWork.ProductRepository.GetPaginationAsync(pageIndex, pageSize);

            //
            var totalCount = await _db.Set<Flight>().CountAsync();
            var items = await _db.Set<Flight>()
                .AsNoTracking()
             

                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Flight>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            //return result;

            return _mapper.Map<Pagination<FlightResponseDto>>(result);
        }
    }

}
