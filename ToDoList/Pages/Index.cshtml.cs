using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Models;

namespace ToDoList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<EventPlanner> EventsAll { get; set; }
        public List<Notification> AllNotification { get; set; }

        public void OnGet()
        {
            EventsAll = Event.GetData();

            foreach (var item in EventsAll)
            {

                string createDateStr = item.CreateDate;

                if (!string.IsNullOrEmpty(createDateStr))
                {
                    if (DateTime.TryParse(createDateStr, out DateTime createDate))
                    {

                        if (createDate == DateTime.Now)
                        {
                            Event.RemiderUser(item);
                        }
                        else if (createDate < DateTime.Now)
                        {

                        }
                        else
                        {
                            Event.RemiderUser(item);
                        }
                    }
                    else
                    {

                    }
                }

                AllNotification = Event.GetNotification();

            }
        }
    }
}