using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GamesListElemModel
    {
        public CrossesModel gameModel;
        public string GameName { get; set; }
        public List<string> Tags { get; set; }

        public GamesListElemModel()
        {
            gameModel = new CrossesModel();
        }
    }
}