using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Transforms
{
    public class ObstacleToUnity
    {
        #region Fields

        GameObject modelObstacle;

        #endregion

        #region Constructors

        public ObstacleToUnity(GameObject modelObstacle)
        {
            this.modelObstacle = modelObstacle;
        }

        #endregion
        
        #region Methods

        public UnityObstacle Convert(IObstacle obstacle)
        {
            GameObject gameObjectObstacle = (GameObject)GameObject.Instantiate(modelObstacle,
                new Vector3(obstacle.Position.X, 0, obstacle.Position.Y), Quaternion.identity);
            gameObjectObstacle.transform.localScale += new Vector3(obstacle.Width, 0, obstacle.Height);
            return new UnityObstacle(gameObjectObstacle);
        }

        #endregion
    }
}
