using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.InfraStructure.EfCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _Context;

        public RoleRepository(AccountContext context):base(context)
        {
            _Context = context;
        }
        public List<RoleViewModel> List()
        {
            return _Context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
        }
        public List<RoleViewModel> GetRoles()
        {
            return _Context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
        }
        public EditRole GetDetails(long id)
        {
            var role = _Context.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                MappedPermissions=MapPermissions(x.Permissions),
            }).AsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();

            return role;
        }
        private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }

    }
}
