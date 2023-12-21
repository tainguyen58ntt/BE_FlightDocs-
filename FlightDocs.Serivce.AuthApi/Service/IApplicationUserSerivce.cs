using AutoMapper;
using FlightDocs.Serivce.AuthApi.Data;
using FlightDocs.Serivce.AuthApi.Models.Dto;
using FlightDocs.Serivce.AuthApi.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Serivce.AuthApi.Service
{
    public class ApplicationUserSerivce : IApplicationUserSerivce
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;


        public ApplicationUserSerivce(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task<IEnumerable<UserDto>> GetUserAsync()
        {
            var users = await _db.ApplicationUsers.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
