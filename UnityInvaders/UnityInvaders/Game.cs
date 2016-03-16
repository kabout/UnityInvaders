
using UnityInvaders.Controllers;
using UnityInvaders.Interfaces;

namespace UnityInvaders
{
    public class Game
    {
        public Game()
        {
            int option = 1;

            IGameController gameController = new GameController();

            while (option != 0)
            {
                option = gameController.ShowMenu();

                switch (option)
                {
                    case 0: break;
                    case 1: gameController.InitUserVsComputer(); break;
                    case 2: gameController.InitUserVsUsers(); break;
                    case 3: break;
                }
            }
        }
    }
}
