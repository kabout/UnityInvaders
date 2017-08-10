using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum EntityType
{
    Defense,
    Alien
}


public class DiedEntity
{
    public int Id { get; set; }
    public float X { get; set; }
    public float Z { get; set; }
    public float Time { get; set; }
    public EntityType Type { get; set; }

    public DiedEntity(int id, float x, float z, float time, EntityType type)
    {
        Id = id;
        X = x;
        Z = z;
        Time = time;
        Type = type;
    }
}

public class GameStatistics : MonoBehaviour
{
    public Text EventText;

    HashSet<DiedEntity> diedAliens = new HashSet<DiedEntity>();
    HashSet<DiedEntity> destroyedDefenses = new HashSet<DiedEntity>();

    public int NumberOfDiedAliens { get { return diedAliens.Count; } }
    public int NumberOfDestroyedDefenses { get { return destroyedDefenses.Count; } }

    public void AddDefense(int id, float x, float z, float time)
    {
        if (destroyedDefenses.Add(new DiedEntity(id, x, z, time, EntityType.Defense)))
            EventText.text = string.Format("Defense {0} destroyed in position ({1},{2})", id, x , z);
    }

    public void AddAlien(int id, float x, float z, float time)
    {
        if(diedAliens.Add(new DiedEntity(id, x, z, time, EntityType.Alien)))
            EventText.text = string.Format("Alien {0} died in position ({1},{2})", id, x, z);
    }

    internal List<DiedEntity> GetDiedEntitiesOrderByTime()
    {
        List<DiedEntity> entities = new List<DiedEntity>(diedAliens);
        entities.AddRange(destroyedDefenses);
        entities.OrderBy(x => x.Time);

        return entities;
    } 
}
