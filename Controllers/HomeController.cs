using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            GamesListModel.AddTags(new string[] { "tag1", "tag2"});
            var tags = String.Join(",",GamesListModel.Tags);
            ViewBag.tags = tags;
            var availbleGames = String.Join(",", GamesListModel.AvailibleGames);
            ViewBag.availibleGames = availbleGames;
            System.Diagnostics.Debug.WriteLine(availbleGames);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string tags)
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewGame()
        {
            var tags = String.Join(",", GamesListModel.Tags);
            ViewBag.tags = tags;
            return View();
        }

        [HttpPost]
        public ActionResult Game(string name, string tags)
        {
            System.Diagnostics.Debug.WriteLine(tags);
            var tagsArr = tags.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            GamesListModel.AddTags(tagsArr);
            GamesListModel.games.Add(new GamesListElemModel(){ GameName = name, Tags = tagsArr.ToList()});
            System.Diagnostics.Debug.WriteLine(GamesListModel.games);
            ViewBag.gameName = name;
            return View();
        }

        [HttpPost]
        public ActionResult JoinGame(string name)
        {
            System.Diagnostics.Debug.WriteLine("JoinGame " + name);
            ViewBag.gameName = name;
            return View("Game");
        }

    }
}