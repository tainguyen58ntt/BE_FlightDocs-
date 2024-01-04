using AutoMapper;
using FlightDocs.Serivce.DocumentApi.Data;
using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Service.IService;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class GroupService : IGroupService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private IClaimService _claimService;
        private ITimeService _timeService;
        private IApplicationUserService _applicationUserService;
        public GroupService(IHttpClientFactory httpClientFactory, AppDbContext db, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _db = db;
            _mapper = mapper;

        }
        public async Task<GroupResponseDto?> GetGroupByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("Group");
            var response = await client.GetAsync($"/api/GroupApi/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                try
                {
                    var resp = JsonConvert.DeserializeObject<GroupResponseDto>(apiContent);
                    return resp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // Log the error or handle it appropriately
                    throw; // Rethrow the exception if needed
                }
            }
            else
            {
                // Log the non-successful status code
                Console.WriteLine($"Error: {response.StatusCode}");
                response.EnsureSuccessStatusCode(); // Throw an exception for non-successful status codes
                return null; // or return an appropriate response
            }
        }

        public async Task<GroupResponseDto?> GetGroupByIdInMicroserviceAsync(int id)
        {
            var group = await _db.Groups.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (group == null) return null;
            return _mapper.Map<GroupResponseDto>(group);
        }
    }
}
