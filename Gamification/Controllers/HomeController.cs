using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Gamification.Controllers
{
    public class HomeController : Controller
    {

        DAL.DALFacade _dalFacde = new DAL.DALFacade();

        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        public ActionResult RCRs()
        {
            DAL.RCR ej = _dalFacde.getRCRbyID("ARG-MMC-2011-0001");
            return View(ej);
        }

        public ActionResult getRCRTypes()
        {
            
            List<String> rcrTypesList = _dalFacde.getRCRTypes();
            rcrTypesList.Add("Todos");
            string json = JsonConvert.SerializeObject(rcrTypesList);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getReportByDate()  // return time by user in a range of dates
        {
            Double time = _dalFacde.getReportByDate(DateTime.Today.AddDays(-30), DateTime.Today, "1393");
            string json = JsonConvert.SerializeObject(time);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult getUsernames()
        {
            List<String> usernames = _dalFacde.getUsernames();
            string json = JsonConvert.SerializeObject(usernames);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getUserData()
        {
            DAL.gamif_user user = _dalFacde.getUserData("Nicolas Gaitan");
            string json = JsonConvert.SerializeObject(user);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getTotalRCRsDonebyUser()
        {
            Int32 totalRCRs = _dalFacde.getTotalRCRsDonebyUser("Edgar Nomesque", DateTime.Today.AddDays(-520), DateTime.Today.AddDays(0));
            string json = JsonConvert.SerializeObject(totalRCRs);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getRCRsDoneByUser()
        {
            List<DAL.RCR> listRCRsDoneByUser = _dalFacde.getRCRsDoneByUser("Edgar Nomesque", DateTime.Today.AddDays(-520), DateTime.Today.AddDays(0));
            string json = JsonConvert.SerializeObject(listRCRsDoneByUser);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult getLeaderboard1(Models.leaderboardViewModel leaderboard)
        {
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult getLeaderboard(Models.leaderboardViewModel leaderboard)
        {
            List<Models.leaderboardViewModel> leaderBoardToPaint = new List<Models.leaderboardViewModel>();
            Models.leaderboardViewModel aux = new Models.leaderboardViewModel();
            aux.position = "1";
            aux.username = "Edgar Nomesque";
            aux.iconPath = "../Images/Avatars/coenomesque.png";
            aux.rcrsDone = "10";
            aux.score = "10.5";
            aux.efficiency = "1.2";
            leaderBoardToPaint.Add(aux);
            aux = new Models.leaderboardViewModel();
            aux.position = "2";
            aux.username = "Nicolas Gaitan";
            aux.iconPath = "../Images/Avatars/congaitan.png";
            aux.rcrsDone = "1";
            aux.score = "0.3";
            aux.efficiency = "0.1";
            leaderBoardToPaint.Add(aux);
            List<Models.leaderboardViewModel> leaderBoardToPaint1 = _dalFacde.getLeaderBoard(leaderboard.rcrTypes, leaderboard.dateStart, leaderboard.dateEnd);
            string json = JsonConvert.SerializeObject(leaderBoardToPaint1);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
