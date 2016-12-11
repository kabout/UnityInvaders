using System;

public class ConsoleGameController : IGameController
{
    #region Constructors

    public ConsoleGameController ()
    { }

    #endregion

    #region Methods

    public void InitGame()
    {
        int optionMainMenu = 0;

        do
        {
            optionMainMenu = ShowMainMenu();
            int optionModeGameMenu = 0;

            if(optionMainMenu != 0)
            {
                switch (optionMainMenu)
                {
                    case 0: break;
                    case 1:
                    {
                        optionModeGameMenu = ShowOnePlayerModeGameMenu();

                        switch (optionModeGameMenu)
                        {
                            case 0: break;
                            case 1: InitOnePlayerWithDefenses(); break;
                            case 2: InitOnePlayerWithAliens(); break;
                        }
                    }
                    break;
                    case 2: optionModeGameMenu = ShowTwoPlayersModeGameMenu(); break;
                }  
            }

        } while (optionMainMenu != 0);
    }

    private void InitOnePlayerWithAliens ()
    {
        //Console.Clear();
        //Console.WriteLine("¡Empieza el juego controlando a los Aliens!");

        //// Escoger Dificultad y Tamaño del mapa.
        //int width = SelectMapWidth();
        //int height = SelectMapHeight();

        //IDifficultController difficultController = new DifficultController(153);
        //IObjectManager objectManager = new ObjectManager(difficultController);
        //IDefenseController defenseController = new DefenseController(difficultController, objectManager);
        //IMapController mapController = new MapController(defenseController, difficultController, objectManager);

        //IMap map = mapController.GetEmptyMap(width);
        //mapController.InitMap(map);

        //bool finish = false;

        //while(!finish)
        //{
        //    UpdateWorld(ref finish);
        //    UpdateAliens();
        //    UpdateDefenses();
        //    UpdateMenu(ref finish);

        //}
    }

    private int SelectMapHeight ()
    {
        return 100;
    }

    private int SelectMapWidth ()
    {
        return 100;
    }

    private void UpdateWorld (ref bool finish)
    {
        if (!IsFinishCondition())
            return;
            
        ShowResults();

        finish = true;
        return;
    }

    private bool IsFinishCondition ()
    {
        return false;
    }

    private void ShowResults ()
    {
    }

    private void UpdateMenu (ref bool finish)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Q)
            {
                if(ConfirmQuitGame())
                    finish = true;
            }
        }
    }

    private bool ConfirmQuitGame ()
    {
        Console.WriteLine("¿Está seguro de querer salir del juego? (s/n)");
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

        } while (key.Key != ConsoleKey.S && key.Key != ConsoleKey.N);

        return key.Key == ConsoleKey.S;
    }

    private void UpdateDefenses ()
    {
    }

    private void UpdateAliens ()
    {
    }

    private void InitOnePlayerWithDefenses ()
    {

    }

    private int ShowTwoPlayersModeGameMenu ()
    {
        throw new NotImplementedException();
    }

    private int ShowOnePlayerModeGameMenu ()
    {
        do
        {
            Console.WriteLine();
            Console.WriteLine("Seleccione con que equipo quiere jugar:");
            Console.WriteLine("1. Defensas");
            Console.WriteLine("2. Aliens");
            Console.WriteLine("3. Atrás");

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1: return 1;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2: return 2;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3: return 0;
                default: ShowIncorrectOption(); break;
            }
        } while (true);
    }

    private void ShowIncorrectOption ()
    {
        Console.Clear();
        Console.WriteLine("¡Elige una opción de la lista tontolculo!");
    }

    private int ShowMainMenu ()
    {
        Console.Clear();

        do
        {
            Console.WriteLine();
            Console.WriteLine("¡Bienvenidos a Unity Invaders!");
            Console.WriteLine("Seleccione una opción para empezar a jugar:");
            Console.WriteLine("1. 1 Jugador");
            Console.WriteLine("2. 2 Jugadores");
            Console.WriteLine("3. Exit");

            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1: return 1;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2: return 2;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3: return 0;
                default: ShowIncorrectOption(); break;
            }
        } while (true);
    }

    #endregion
}
