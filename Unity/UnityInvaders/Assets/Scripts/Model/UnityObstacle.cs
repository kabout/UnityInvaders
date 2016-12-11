using UnityEngine;
using System.Collections;

public class UnityObstacle : MonoBehaviour, IObstacle
{
    #region Fields

    public int id;

    public int Id
    {
        get { return id; }
    }

    #endregion

    #region Properties

    public float Radius
    {
        get { return transform.localScale.x / 2; }
    }    

    public Vector3 Position
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
