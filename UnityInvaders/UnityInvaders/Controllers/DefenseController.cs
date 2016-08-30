using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Controllers
{
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

        public void PlaceDefenses(IMap map)
        {
            int numDefenses = difficultController.GetNumberOfDefenses(map);

            while (numDefenses > 0)
            {
                IDefense defense = objectManager.GenerateDefense(map, Constants.DEFAULT_DEFENSE_RADIO);

                if (defense == null)
                    return;

                map.AddDefense(defense);

                numDefenses--;
            }

            Debug.Assert(map.Defenses.Count > 0);
        }

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
}
