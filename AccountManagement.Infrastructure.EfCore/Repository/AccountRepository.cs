using _0_FrameWork.Application;
using _0_FrameWork.InfraStructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.InfraStructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _Context;

        public AccountRepository(AccountContext context):base(context)
        {
            _Context = context;
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _Context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName
            }).ToList();
        }

        public Account GetBy(string username)
        {
            return _Context.Accounts.FirstOrDefault(x => x.UserName == username);
        }

        public EditAccount GetDetails(long Id)
        {
            return _Context.Accounts.Select(x => new EditAccount
            { 
                Id=x.Id,
                FullName=x.FullName,
                UserName=x.UserName,
                Password=x.Password,
                Mobile=x.Mobile,
                RoleId=x.RoleId,
            }).FirstOrDefault(x=>x.Id==Id);
        }

        public List<AccountViewModel> Search(AccountSearchModel SearchModel)
        {
            var query = _Context.Accounts
                .Include(x => x.Role)
                .Select(x => new AccountViewModel
                {
                    Id=x.Id,
                    FullName=x.FullName,
                    UserName=x.UserName,
                    ProfilePhoto=x.ProfilePhoto,
                    Mobile=x.Mobile,
                    Role=x.Role.Name,
                    CreationDate=x.CreationDate.ToFarsi(),
                    
                }).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(SearchModel.FullName))
                query = query.Where(x => x.FullName.Contains(SearchModel.FullName));
            if (!string.IsNullOrWhiteSpace(SearchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(SearchModel.Mobile));
            if (SearchModel.RoleId != 0)
                query = query.Where(x => x.RoleId == SearchModel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
