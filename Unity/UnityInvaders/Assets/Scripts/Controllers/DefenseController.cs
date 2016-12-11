using System.Collections.Generic;

public class DefenseController : IDefenseController
{
    #region Fields

    IDifficultController difficultController;
    IObjectManager objectManager;

    #endregion

    #region Constructors

    public DefenseController(IDifficultController difficultController, IObjectManager objectManager)
    {
        this.difficultController = difficultController;
        this.objectManager = objectManager;
    }

    #endregion

    #region Methods

    public IList<IAlien> GetAliensInRange (IMap map, IDefense defense)
    {
        //int x = defense.Position.x;
        //int xEnd = x + defense.Range;
        //int y = defense.Position.Y;
        //int yEnd = y + defense.Range;

        IList<IAlien> aliens = new List<IAlien>();

        //foreach(IAlien alien in map.Aliens)
        //{
        //    if (alien.Position.X >= x && alien.Position.X < xEnd &&
        //       alien.Position.Y >= y && alien.Position.Y < yEnd)
        //        aliens.Add(alien);
        //}

        return aliens;
    }

    #endregion
}
