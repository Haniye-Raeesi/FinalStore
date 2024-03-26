using _0_FrameWork.Application;
using _0_FrameWork.Domain;
using AccountManagement.Application.Contracts.Account;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long, Account>
    {
        EditAccount GetDetails(long Id);
        List<AccountViewModel> Search(AccountSearchModel Search);
        Account GetBy(string username);
        List<AccountViewModel> GetAccounts();

    }
}
