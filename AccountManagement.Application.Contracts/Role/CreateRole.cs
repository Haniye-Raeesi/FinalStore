using _0_FrameWork.InfraStructure;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}
