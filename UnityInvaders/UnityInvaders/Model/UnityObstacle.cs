using UnityEngine;

namespace UnityInvaders.Model
{
    public class UnityObstacle
    {
        #region Fields

        public GameObject obstacle;

        #endregion

        #region Properties

        public int Width
        {
            get { return (int)obstacle.transform.localScale.x; }
        }

        public int Height
        {
            get { return (int)obstacle.transform.localScale.z; }
        }

        public Position Position
        {
            get { return new Position((int)obstacle.transform.localPosition.x, (int)obstacle.transform.localPosition.z); }
        }

        #endregion

        #region Constructors

        public UnityObstacle(GameObject obstacle)
        {
            this.obstacle = obstacle;
        }

        #endregion

        #region Methods

        public void ChangePosition(Position position)
        {
            obstacle.transform.localScale += new Vector3(position.X, 0, position.Y);
        }

        #endregion
    }
}