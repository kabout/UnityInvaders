using System;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Controllers
{
    public class DefenseController : IDefenseController
    {
        #region Fields

        IDifficultController difficultController;

        #endregion

        #region Constructors

        public DefenseController(IDifficultController difficultController)
        {
            this.difficultController = difficultController;
        }

        #endregion

        #region Methods

        public void PlaceDefenses(IMap map)
        {
            int numDefenses = difficultController.GetNumberOfDefenses(map);

            while (numDefenses > 0)
            {
                IDefense defense = GenerateDefense(Constants.DEFENSE_SIZE, map);
                map.AddDefense(defense);
                numDefenses--;
            }
        }

        private IDefense GenerateDefense(int sizeDefense, IMap map, int maxUcos = int.MaxValue)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int maxHeight = map.Height - sizeDefense;
            int maxWidth = map.Width - sizeDefense;
            int minHeight = 0 + sizeDefense;
            int minWidth = minHeight;

            Position position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
            LevelDefense levelDefense = difficultController.GetLevelDefense();
            IDefense defense = new Defense(Constants.DEFENSE_HEALTH, sizeDefense, levelDefense,
                difficultController.GetDefenseDamage(levelDefense), position);

            while (!map.IsValidPosition(defense))
            {
                position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
                defense.ChangePosition(position);
            }

            return defense;
        }

        #endregion
    }
}
