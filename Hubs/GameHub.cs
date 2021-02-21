using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Hubs
{
    public class GameHub : Hub
    {
        public void Connect(string gameName)
        {
            var id = Context.ConnectionId;
            Predicate<GamesListElemModel> predicate = delegate (GamesListElemModel x) { return x.GameName == gameName; };
            CrossesModel currGame = GamesListModel.games.Find(predicate).gameModel;
            if(currGame?.zeroId == null)
            {
                currGame.zeroId = id;
            }
            else if(currGame?.crossId == null)
            {
                currGame.crossId = id;
            }
        }

        public void MakeMove(int x, int y, string gameName)
        {

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            Predicate<GamesListElemModel> predicate = delegate (GamesListElemModel x)
                { return x.gameModel.crossId == id || x.gameModel.zeroId == id; };
            GamesListModel.games.Remove(GamesListModel.games.Find(predicate));
            return base.OnDisconnected(stopCalled);
        }
    }
}