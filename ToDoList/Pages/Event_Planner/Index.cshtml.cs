using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Pages.Event_Planner
{
    public class IndexModel : PageModel
    {
        public List<EventPlanner> Events { get; set; }
        [BindProperty]
        public EventPlanner enevtPlann { get; set; }
        public void OnGet()
        {
            Events= Event.GetData();
        }
        public async Task OnPostAdd()
        {
            enevtPlann.Id = Event.GetData().Count() + 1;
             Event.InsertData(enevtPlann);
            Events = Event.GetData();
            RedirectToPage("./Event_Planner/index");
        }
        public async Task OnPostEdit()
        {
            Event.UpdateData(enevtPlann);
            Events = Event.GetData();
            RedirectToPage("./Event_Planner/index");
        }
        public async Task OnPostDelete(int id)
        {
            Event.DeleteData(id);
            Events = Event.GetData();
            RedirectToPage("./Event_Planner/index");
        }
      

    }
}
