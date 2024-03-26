using _0_Framework.Application;
using _0_FrameWork.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository accountRepository,
            IFileUploader fileUploader, IPasswordHasher passwordHasher,
            IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var account = _accountRepository.Get(command.Id);
            var Operation = new OperationResult();
            if (_accountRepository.Exists(x => x.FullName == command.Password && x.Id != command.Id))
            {
                return Operation.Failed(ApplicationMessages.Duplicated);
            }
            if (account == null)
            {
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (command.Password != command.RePassword)
            {
                return Operation.Failed(ApplicationMessages.PassNotMatched);
            }
            var pass = _passwordHasher.Hash(command.Password);
            account.ChangePassword(pass);
            _accountRepository.Save();
            return Operation.Successful();
        }

        public OperationResult Create(CreateAccount command)
        {
            var Operation = new OperationResult();
            if (_accountRepository.Exists(x => x.FullName == command.FullName))
             return Operation.Failed(ApplicationMessages.Duplicated); 
            else
            {
                var PicturePath = $"{command.FullName}";
                var fileName = _fileUploader.Upload(command.ProfilePhoto, PicturePath);
                var password = _passwordHasher.Hash(command.Password);

                var account = new Account(command.FullName, command.UserName, password, command.RoleId
                    , command.Mobile, fileName);
                _accountRepository.Create(account);
                _accountRepository.Save();
                return Operation.Successful();
            }


        }

        public OperationResult Edit(EditAccount command)
        {
            var account = _accountRepository.Get(command.Id);
            var Operation = new OperationResult();
            if (_accountRepository.Exists(x => x.FullName == command.FullName && x.Id != command.Id))
            {
                return Operation.Failed(ApplicationMessages.Duplicated);
            }
            if (account == null)
            {
                return Operation.Failed(ApplicationMessages.RecordNotFound);
            }
            var PicturePath = $"{command.FullName}";
            var fileName = _fileUploader.Upload(command.ProfilePhoto, PicturePath);

            account.Edit(command.FullName,command.UserName,command.RoleId,command.Mobile,fileName);
            _accountRepository.Save();
            return Operation.Successful();
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public EditAccount GetDetails(long Id)
        {
            return _accountRepository.GetDetails(Id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);
            if (account == null)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);
            var permissions = _roleRepository.Get(account.RoleId).Permissions.Select(x=>x.Code).ToList();

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.FullName
                , account.UserName, account.Mobile,account.ProfilePhoto,permissions);

            _authHelper.Signin(authViewModel);
            return operation.Successful();


        }

        public void LogOut()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel SearchModel)
        {
            return _accountRepository.Search(SearchModel);
        }
    }
}
