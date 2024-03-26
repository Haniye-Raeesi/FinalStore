using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
       [TempData]
        public string LoginMessage { get; set; }
        [TempData]
        public string RegisterMessage { get; set; }
        private readonly IAccountApplication _accountApplication;


        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPostLogin(Login Command)
        {
            var result = _accountApplication.Login(Command);
            if (result.IsSuccedded)
            {
                return RedirectToPage("/Index");
            }
            LoginMessage= result.Message;
            return RedirectToPage("/Account");

        }
        public IActionResult OnGetLogOut()
        {
             _accountApplication.LogOut();
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostRegister(CreateAccount Command)
        {
            var result = _accountApplication.Create(Command);
            if (result.IsSuccedded) 
            {
                RegisterMessage = result.Message;
                return RedirectToPage("/Account");
            }
            RegisterMessage=result.Message;
            return RedirectToPage("/Account");

        }
    }
}
