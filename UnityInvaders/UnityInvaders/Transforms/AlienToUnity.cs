using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Transforms
{
    public class AlienToUnity
    {
        #region Fields

        GameObject modelAlien;

        #endregion

        #region Constructors

        public AlienToUnity(GameObject modelAlien)
        {
            this.modelAlien = modelAlien;
        }

        #endregion
        
        #region Methods

        public  UnityAlien Convert(IAlien alien)
        {
            GameObject gameObjectDefense = (GameObject)Object.Instantiate(modelAlien,
                new Vector3(alien.Position.X + (alien.Width / 2), 0, alien.Position.Y + (alien.Height / 2)), Quaternion.identity);
            gameObjectDefense.transform.localScale += new Vector3(alien.Width, 0, alien.Height);
            return new UnityAlien(gameObjectDefense, alien);
        }

        #endregion
    }
}
