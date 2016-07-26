using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Transforms
{
    public class DefenseToUnity
    {
        #region Fields

        GameObject modelDefense;

        #endregion

        #region Constructors

        public DefenseToUnity(GameObject modelDefense)
        {
            this.modelDefense = modelDefense;
        }

        #endregion
        
        #region Methods

        public  UnityDefense Convert(IDefense defense)
        {
            GameObject gameObjectDefense = (GameObject)Object.Instantiate(modelDefense,
                new Vector3(defense.Position.X + (defense.Width / 2), 0, defense.Position.Y + (defense.Height / 2)), Quaternion.identity);
            gameObjectDefense.transform.localScale += new Vector3(defense.Width, 0, defense.Height);
            return new UnityDefense(gameObjectDefense, defense);
        }

        #endregion
    }
}
