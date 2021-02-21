using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public static class GamesListModel
    {
        public static void AddTags(string[] tagsArr)
        {
            foreach (var tag in tagsArr)
            {
                if (!tags.Contains(tag))
                {
                    tags.Add(tag);
                }
            }
        }
        private static List<string> tags = new List<string>();

        public static List<string> Tags 
        {
            get { return tags; }
            set { tags = value; }
        }

        public static List<string> AvailibleGames
        {
            get
            {
                var result = new List<string>();
                foreach (var game in games)
                {
                    if(game.gameModel.crossId == null)
                    {
                        result.Add(game.GameName);
                    }
                }
                return result;
            }
        }
        public static List<string> GetAvailibleGamesByTag(string[] tags)
        {
            var result = new List<string>();
            foreach (var game in games)
            {
                if (game.gameModel.crossId == null && CheckContainsTags(tags, game))
                {
                    result.Add(game.GameName);
                }
            }
            return result;
        }
        private static bool CheckContainsTags(string[] tags, GamesListElemModel model)
        {
            foreach (var tag in tags)
            {
                if (!model.Tags.Contains(tag)) return false;
            }
            return true;
        }

        public static List<GamesListElemModel> games = new List<GamesListElemModel>();
        public static void LogTags()
        {
            foreach (var item in games)
            {
                System.Diagnostics.Debug.WriteLine(item.GameName + " " + String.Join(",", item.Tags.ToArray()));
            }
        }
    }
}