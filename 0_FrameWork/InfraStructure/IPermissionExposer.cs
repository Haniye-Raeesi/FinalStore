namespace _0_FrameWork.InfraStructure
{
    public interface IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose();
    }
}
