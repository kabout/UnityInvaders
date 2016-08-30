using System;
using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityObstacle : MonoBehaviour, IObstacle
    {
        #region Fields

        public int id;

        #endregion

        #region Properties

        public float Radius
        {
            get { return transform.localScale.x / 2; }
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        Vector3 IEntity.Position
        {
            get
            {
                return transform.position;
            }
        }

        #endregion        

        #region Methods

        public void ChangePosition(Vector3 position)
        {
            transform.position = position;
        }

        #endregion
    }
}