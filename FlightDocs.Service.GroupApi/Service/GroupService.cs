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
        private IApplicationUserService _applicationUserService;


        public GroupService(AppDbContext db, IMapper mapper, IClaimService claimService, ITimeService timeService, IApplicationUserService applicationUserService)
        {
            _db = db;
            _mapper = mapper;
            _claimService = claimService;
            _timeService = timeService;
            _applicationUserService = applicationUserService;
        }

        public async Task<bool> AssingMemeberToGroupAsync(int id, string email)
        {
            var group = await _db.Groups.Where(g => g.Id == id).FirstOrDefaultAsync();
            if (group == null) return false;
            var userDTo = await _applicationUserService.GetUserByEmail(email); 
            if (userDTo == null) return false;
            var user = _mapper.Map<ApplicationUser>(userDTo);
            user.GroupId = group.Id;
            await _db.Set<ApplicationUser>().AddAsync(user);
            return await _db.SaveChangesAsync() > 0;

        }

        public async Task<bool> CreatAsync(GroupRequestDto groupRequestDto)
        {
            string currentUserId = _claimService.GetCurrentUserId();
            if (currentUserId == null) return false;
            // conver to dto -> entity
            var group = _mapper.Map<Group>(groupRequestDto);
            // get create by by login - admin role 
            var applicationUser =  await _applicationUserService.GetUserById(currentUserId);   
            if(applicationUser == null) return false;
            group.CreateBy = applicationUser.Email;
            group.CreateDate = _timeService.GetCurrentTimeInVietnam();
            await _db.Set<Group>().AddAsync(group);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<GroupResponseDto?> GetGroupByIdAsync(int id)
        {
              var group = await _db.Groups.Where(g => g.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<GroupResponseDto>(group);
            

        }

        public async Task<Pagination<GroupResponseDto>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            //var result = await _unitOfWork.ProductRepository.GetPaginationAsync(pageIndex, pageSize);

            //
            var totalCount = await _db.Set<Group>().CountAsync();
            var items = await _db.Set<Group>()
                .AsNoTracking()
                .Include(g => g.ApplicationUsers)
             

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
