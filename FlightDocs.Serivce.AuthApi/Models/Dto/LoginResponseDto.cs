﻿namespace FlightDocs.Serivce.AuthApi.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
        public bool? isLock { get; set; }
    }
}
