﻿using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public string Mobile { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }

}
