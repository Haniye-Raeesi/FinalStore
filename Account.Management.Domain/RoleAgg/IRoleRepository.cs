﻿using _0_FrameWork.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository:IRepository<long,Role>
    {
        List<RoleViewModel> GetRoles();
        EditRole GetDetails(long id);
        List<RoleViewModel> List();
    }
}
