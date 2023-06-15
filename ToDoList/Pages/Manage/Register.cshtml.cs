using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Models;

namespace ToDoList.Pages.Manage
{
    public class RegisterModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public User_Model input { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
         

            if (ModelState.IsValid)
            {
                Manage_User.RegesterUser(input);
                return RedirectToPage("/Index");
            }
             return RedirectToPage("/Index");


        }
    }
}
