using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Web.Models;

namespace EventSourcingProtoType.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = MvcApplication.Sports;
            return View(model);
        }

        public async Task<ActionResult> CreateSport(Sport sport)
        {
            MvcApplication.Sports.Add(sport);
            await MvcApplication.Activator.Bus.Send(new CreateSportCommand(sport.Id, sport.Name));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UpdateSport(Sport sport)
        {
            var index = MvcApplication.Sports.IndexOf(MvcApplication.Sports.Single(s => s.Id == sport.Id));
            MvcApplication.Sports[index] = sport;
            await  MvcApplication.Activator.Bus.Send(new UpdateSportCommand(sport.Id, sport.Name));
            return RedirectToAction("Index");
        }
    }
}