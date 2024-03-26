namespace _0_FrameWork.InfraStructure
{
    public class NeedsPermissionsAttribute: Attribute
    {
        public int Permission { get; set; }
        public NeedsPermissionsAttribute(int permission)
        {
            Permission = permission;
        }

    }
}
