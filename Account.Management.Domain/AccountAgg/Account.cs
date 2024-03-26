using _0_FrameWork.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account:EntityBase
    {
       
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public long RoleId { get; private set; }
        public string Mobile { get; private set; }
        public string ProfilePhoto { get; private set; }
        public Role Role { get; set; }
        public Account(string fullName, string userName, string password, long roleId, string mobile,string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            RoleId = roleId;
            if (roleId==0)
            {
                RoleId = 1;
            }
            
            Mobile = mobile;
            ProfilePhoto = profilePhoto;
            Password = password;
        }
        public void Edit(string fullName, string userName, long roleId, string mobile, string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            RoleId = roleId;
            Mobile = mobile;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
        }
        public void ChangePassword(string password) 
        {
            Password = password;
        }


    }
}
