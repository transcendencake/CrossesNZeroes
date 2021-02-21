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

        public static List<GamesListElemModel> games = new List<GamesListElemModel>();
    }
}