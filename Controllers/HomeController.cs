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
        public ActionResult Index(string tags)
        {
            string availibleGames;
            if (String.IsNullOrEmpty(tags)) {
                GamesListModel.AddTags(new string[] { "BestGameEver", "PromotedTag" });                
                availibleGames = String.Join(",", GamesListModel.AvailibleGames);
            }
            else
            {
                var tagsArr = NormalizeTags(tags);
                System.Diagnostics.Debug.WriteLine(String.Join(",", tagsArr));
                // System.Diagnostics.Debug.WriteLine();
                availibleGames = String.Join(",", GamesListModel.GetAvailibleGamesByTag(tagsArr));
            }
            var usedTags = String.Join(",", GamesListModel.Tags);
            ViewBag.tags = usedTags;
            System.Diagnostics.Debug.WriteLine("av games:" + availibleGames);
            ViewBag.availibleGames = availibleGames;
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
            var tagsArr = NormalizeTags(tags);
            GamesListModel.AddTags(tagsArr);
            foreach (var item in tagsArr)
            {
                System.Diagnostics.Debug.WriteLine("tagsArr: " + item);
            }
            var tagsList = tagsArr.Cast<string>().ToList();
            GamesListModel.games.Add(new GamesListElemModel(){ GameName = name, Tags = tagsList});
            System.Diagnostics.Debug.WriteLine(GamesListModel.games);
            ViewBag.gameName = name;
            GamesListModel.LogTags();
            return View();
        }

        public ActionResult JoinGame(string name)
        {
            System.Diagnostics.Debug.WriteLine("JoinGame " + name);
            ViewBag.gameName = name;
            return View("Game");
        }

        private string[] NormalizeTags(string tags)
        {
            var tagsArr = tags.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var result = new string[tagsArr.Length];
            for (int i = 0; i < tagsArr.Length; i++)
            {
                result[i] = tagsArr[i].Split('"')[3];
            }
            System.Diagnostics.Debug.WriteLine(result);
            return result;
        }


    }
}