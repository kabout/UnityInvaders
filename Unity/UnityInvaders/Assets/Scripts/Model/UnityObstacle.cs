using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

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

    public IPosition Position
    {
        get
        {
            return ConvertPosition.Convert(transform.position);
        }
    }

    #endregion

    #region Methods

    public void ChangePosition(IPosition position)
    {
        transform.position = ConvertPosition.Convert(position);
    }

    #endregion
}
