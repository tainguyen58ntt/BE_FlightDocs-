

using FlightDocs.Service.GroupApi.Models.Dto;
using FlightDocs.Service.GroupApi.Pagination;

namespace FlightDocs.Service.GroupApi.Service.IService
{
    public interface IGroupService
    {
        //Task<IEnumerable<GroupResponseDto>> GetGroupAsync();
        Task<Pagination<GroupResponseDto>> GetPaginationAsync(int pageIndex, int pageSize);

        Task<bool> CreatAsync(GroupRequestDto groupRequestDto);

    }
}
