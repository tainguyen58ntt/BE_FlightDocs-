﻿using FlightDocs.Service.DocumentApi.Models.Dto;

namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IDocumentPermissionService
    {
        Task<int> CountGroupPermissionByDocumentTypeIdAsync(int documentTypeId);
        Task<bool> CheckPermissionCanModifyDocx(int documentTypeId);
    }
}
