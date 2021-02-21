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
        private CrossesModel GetGameByName(string gameName)
        {
            Predicate<GamesListElemModel> predicate = delegate (GamesListElemModel x) { return x.GameName == gameName; };
            return GamesListModel.games.Find(predicate).gameModel;
        }
        public void Connect(string gameName)
        {
            var id = Context.ConnectionId;
            CrossesModel currGame = GetGameByName(gameName);
            if(currGame?.zeroId == null)
            {
                currGame.zeroId = id;
                System.Diagnostics.Debug.WriteLine(currGame.zeroId);
            }
            else if(currGame?.crossId == null)
            {
                currGame.crossId = id;
                System.Diagnostics.Debug.WriteLine(currGame.crossId);
            }
        }

        public void MakeMove(int x, int y, string gameName)
        {
            CrossesModel currGame = GetGameByName(gameName);
            var id = Context.ConnectionId;
            var opponentId = id == currGame.zeroId ? currGame.crossId : currGame.zeroId;
            System.Diagnostics.Debug.WriteLine(opponentId);
            int[] moveResult = currGame.MakeMove(Context.ConnectionId, x, y);
            if (moveResult[0] != 0)
            {
                System.Diagnostics.Debug.WriteLine(moveResult[1]);
                if (moveResult[1] < 0)
                {                    
                    string point = x.ToString() + ',' + y.ToString();
                    Clients.Client(opponentId).AddMove(point, currGame.zerosMove);
                    Clients.Caller.AddMove(point, currGame.zerosMove);
                    
                }
                else if (moveResult[1] == 0)
                {
                    Clients.Caller.FinishGame("Draw");
                    Clients.User(opponentId).FinishGame("Draw");
                }
                else
                {
                    Clients.Caller.FinishGame("You win");
                    Clients.User(opponentId).FinishGame("You lose");
                }
            }
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