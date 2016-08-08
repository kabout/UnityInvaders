using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityInvaders.Controllers;
using UnityInvaders.Interfaces;

namespace ConsoleUnityInvaders
{
    class Program
    {
        static void Main (string[] args)
        {
            IGameController gameController = new ConsoleGameController();
            gameController.InitGame();
        }
    }
}
