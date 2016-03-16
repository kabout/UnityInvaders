using System;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Controllers
{
    public class GameController : IGameController
    {
        #region Constructors

        public GameController()
        { }

        #endregion

        #region Methods

        public int ShowMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("¡Bienvenidos a Unity Invaders!");
                Console.WriteLine("Seleccione una opción para empezar a jugar:");
                Console.WriteLine("1. User vs Computer");
                Console.WriteLine("2. User vs User");
                Console.WriteLine("3. Exit");

                ConsoleKeyInfo key = Console.ReadKey();

                switch(key.KeyChar)
                {
                    case '1': return 1;
                    case '2': return 2;
                    case '3': return 0;
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("¡Elige una opción de la lista tontolculo!");
                        }
                        break;
                }
            }
        }

        public void InitUserVsComputer()
        { }

        public void InitUserVsUsers()
        { }

        #endregion
    }
}
