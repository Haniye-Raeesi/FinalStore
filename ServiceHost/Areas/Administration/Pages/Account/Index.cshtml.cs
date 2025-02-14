using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Account
{
    public class IndexModel : PageModel
        {
            public List<AccountViewModel> Accounts;
            private readonly IAccountApplication _accountApplication;
            private readonly IRoleApplication _roleApplication;
            public AccountSearchModel SearchModel;
            public SelectList Roles;
            [TempData]
            public string Message { get; set; }

            public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
            {
                _accountApplication = accountApplication;
            _roleApplication = roleApplication;
            }

            public void OnGet(AccountSearchModel searchModel)
            {
                Roles = new SelectList(_roleApplication.List(), "Id", "Name");

                Accounts = _accountApplication.Search(searchModel);
            }
            public IActionResult OnGetCreate()
            {
                var command = new CreateAccount
                {
                    Roles = _roleApplication.List()
                };
                return Partial("./Create", command);
            }

            public JsonResult OnPostCreate(CreateAccount command)
            {
                var result = _accountApplication.Create(command);
                return new JsonResult(result);

            }
            public IActionResult OnGetEdit(long Id)
            {
            var account = _accountApplication.GetDetails(Id);
                account.Roles = _roleApplication.List();
                return Partial("./Edit", account);
            }
            public JsonResult OnPostEdit(EditAccount command)
            {
                var result = _accountApplication.Edit(command);
                return new JsonResult(result);
            }
        public IActionResult OnGetChangePassword(long Id)
        {
            var command = new ChangePassword { Id = Id };
            return Partial("ChangePassword", command);
        }
        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }


    }


    }

