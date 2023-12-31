﻿using FlightDocs.Service.FlightAPI.Models.Dto;
using FlightDocs.Service.FlightAPI.Pagination;

namespace FlightDocs.Service.FlightAPI.Service.IService
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightResponseDto>> GetFlightAsync();
        Task<FlightResponseDto?> GetFlightByIdAsync(string id);
        Task<Pagination<FlightResponseDto>> GetPaginationAsync(int pageIndex, int pageSize);

    }
}
