using AutoMapper;
using FlightDocs.Serivce.GroupApi.Data;
using FlightDocs.Service.GroupApi.Models;
using FlightDocs.Service.GroupApi.Models.Dto;
using FlightDocs.Service.GroupApi.Pagination;
using FlightDocs.Service.GroupApi.Service;
using FlightDocs.Service.GroupApi.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Service.GroupApi.Service
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private IClaimService _claimService;
        private ITimeService _timeService;


        public GroupService(AppDbContext db, IMapper mapper, IClaimService claimService, ITimeService timeService)
        {
            _db = db;
            _mapper = mapper;
            _claimService = claimService;
            _timeService = timeService;
        }

        public async Task<bool> CreatAsync(GroupRequestDto groupRequestDto)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            // conver to dto -> entity
            var group = _mapper.Map<Group>(groupRequestDto);
            // get create by by login - admin role 
            //group.CreateBy = ;
            group.CreateDate = _timeService.GetCurrentTimeInVietnam();
            await _db.Set<Group>().AddAsync(group);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Pagination<GroupResponseDto>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            //var result = await _unitOfWork.ProductRepository.GetPaginationAsync(pageIndex, pageSize);

            //
            var totalCount = await _db.Set<Group>().CountAsync();
            var items = await _db.Set<Group>()
                .AsNoTracking()
             

                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            var result = new Pagination<Group>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            //return result;

            return _mapper.Map<Pagination<GroupResponseDto>>(result);
        }
    }

}
