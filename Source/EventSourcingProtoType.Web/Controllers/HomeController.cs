using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Web.Models;

namespace EventSourcingProtoType.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateSport(Sport sport)
        {
            ViewModel.Sports.Add(sport);
            await MvcApplication.Activator.Bus.Send(new CreateSportCommand(sport.Id, sport.Name));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UpdateSport(Sport sport)
        {
            var index = ViewModel.Sports.IndexOf(ViewModel.Sports.Single(s => s.Id == sport.Id));
            ViewModel.Sports[index] = sport;
            await  MvcApplication.Activator.Bus.Send(new UpdateSportCommand(sport.Id, sport.Name));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CreateCompetitor(Competitor competitor)
        {
            ViewModel.Competitors.Add(competitor);
            await MvcApplication.Activator.Bus.Send(new CreateCompetitorCommand(competitor.Id, competitor.Name));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UpdateCompetitor(Competitor competitor)
        {
            var index = ViewModel.Competitors.IndexOf(ViewModel.Competitors.Single(s => s.Id == competitor.Id));
            ViewModel.Competitors[index] = competitor;
            await  MvcApplication.Activator.Bus.Send(new UpdateCompetitorCommand(competitor.Id, competitor.Name));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CreateFixture(Fixture fixture)
        {
            ViewModel.Fixtures.Add(fixture);
            await MvcApplication.Activator.Bus.Send(new CreateFixtureCommand(
                fixture.Id, fixture.Title, fixture.Date, fixture.Sport.Id, fixture.Competitor1.Id, fixture.Competitor2.Id));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UpdateFixture(Fixture fixture)
        {
            var index = ViewModel.Fixtures.IndexOf(ViewModel.Fixtures.Single(s => s.Id == fixture.Id));

            ViewModel.Fixtures[index] = fixture;

            await MvcApplication.Activator.Bus.Send(new UpdateFixtureCommand(
                fixture.Id, fixture.Title, fixture.Date, fixture.Sport.Id, fixture.Competitor1.Id, fixture.Competitor2.Id));
            return RedirectToAction("Index");
        }
    }
}