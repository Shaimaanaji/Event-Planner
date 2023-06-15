using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Models;

namespace ToDoList.Pages.Manage
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Login_Model input { get; set; }

        public void OnGet()
        {


        }
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                Manage_User.LoginUser(input);
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");


        }

    }
}
