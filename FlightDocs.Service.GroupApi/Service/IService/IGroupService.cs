

using FlightDocs.Service.GroupApi.Models.Dto;
using FlightDocs.Service.GroupApi.Pagination;

namespace FlightDocs.Service.GroupApi.Service.IService
{
    public interface IGroupService
    {
        //Task<IEnumerable<GroupResponseDto>> GetGroupAsync();
        Task<GroupResponseDto?> GetGroupByIdAsync(int id);
        Task<Pagination<GroupResponseDto>> GetPaginationAsync(int pageIndex, int pageSize);

        //Task<Pagination<GroupResponseDto>> GetByNamePaginationAsync(int pageIndex, int pageSize, string name);

        Task<IEnumerable<GroupResponseDto>> GetGroupByNameAsync(string name);

        Task<bool> CreatAsync(GroupRequestDto groupRequestDto);
        Task<bool> AssingMemeberToGroupAsync(int id,string email);

    }
}
