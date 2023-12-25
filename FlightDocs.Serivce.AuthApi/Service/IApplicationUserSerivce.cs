using AutoMapper;
using FlightDocs.Serivce.AuthApi.Data;
using FlightDocs.Serivce.AuthApi.Models;
using FlightDocs.Serivce.AuthApi.Models.Dto;
using FlightDocs.Serivce.AuthApi.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs.Serivce.AuthApi.Service
{
    public class ApplicationUserSerivce : IApplicationUserSerivce
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public ApplicationUserSerivce(AppDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;

        }
        public async Task<IEnumerable<UserDto>> GetUserAsync()
        {
            var users = await _db.ApplicationUsers.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var users = await _db.ApplicationUsers.Where(u => u.Email == email).FirstOrDefaultAsync();
            return _mapper.Map<UserDto>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(string id)
        {
            var users = await _db.ApplicationUsers.Where(u => u.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<UserDto>(users);
        }

        public async Task<bool> TerminalUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.MaxValue; // Lock the user out indefinitely
                 _db.Set<ApplicationUser>().Update(user);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> UnTerminalUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = null; 
                _db.Set<ApplicationUser>().Update(user);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
