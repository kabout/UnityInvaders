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
            GameObject gameObjectObstacle = (GameObject)Object.Instantiate(modelObstacle,
                new Vector3(obstacle.Position.X + obstacle.Radius, 0, obstacle.Position.Y + obstacle.Radius), Quaternion.Euler(-180, 90, 0));
            gameObjectObstacle.transform.localScale += new Vector3((obstacle.Radius * 2), obstacle.Radius, (obstacle.Radius * 2));
            return new UnityObstacle(gameObjectObstacle);
        }

        #endregion
    }
}
