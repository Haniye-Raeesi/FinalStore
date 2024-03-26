using _0_FrameWork.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
       public OperationResult Create(CreateAccount command);
       public  OperationResult Edit(EditAccount command);
       public EditAccount GetDetails(long Id);
       public List<AccountViewModel>  Search(AccountSearchModel Search);
        public OperationResult ChangePassword(ChangePassword Command);
        public OperationResult Login(Login command);
        public void LogOut();
        List<AccountViewModel> GetAccounts();


    }
}
