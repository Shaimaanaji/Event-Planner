using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Pages.Event_Planner
{
    public class IndexModel : PageModel
    {
        public List<EventPlanner> Events { get; set; }
        public List<EventPlanner> EventsAll { get; set; }


        [BindProperty]
        public EventPlanner enevtPlann { get; set; }
        [BindProperty(SupportsGet = true)]
        public string EventTile { get; set; } = "";

        public List<SelectListItem> lstEvents { get; set; } = default!;

        public void OnGet()
        {
            EventsAll = Event.GetData();
            lstEvents = EventsAll.Select(x => new SelectListItem() { Value = x.Title, Text = x.Title }).ToList();

            if (EventTile == "")
            {          
                Events = Event.GetData();

            }

            else
            {
                Events = Event.FilterData(EventTile);
            }

        }
        public async Task OnPostAdd()
        {
            EventsAll = Event.GetData();
            lstEvents = EventsAll.Select(x => new SelectListItem() { Value = x.Title, Text = x.Title }).ToList();
            Event.InsertData(enevtPlann);
            Events = Event.GetData();
            RedirectToPage("./Event_Planner/index");
        }
        public async Task OnPostEdit()
        {
            EventsAll = Event.GetData();
            lstEvents = EventsAll.Select(x => new SelectListItem() { Value = x.Title, Text = x.Title }).ToList();
            Event.UpdateData(enevtPlann);
            Events = Event.GetData();
            RedirectToPage("./Event_Planner/index");
        }
        public async Task OnPostDelete(int id)
        {
            EventsAll = Event.GetData();
            lstEvents = EventsAll.Select(x => new SelectListItem() { Value = x.Title, Text = x.Title }).ToList();
            Event.DeleteData(id);
            Events = Event.GetData();
            RedirectToPage("./Event_Planner/index");
        }
      

    }
}
