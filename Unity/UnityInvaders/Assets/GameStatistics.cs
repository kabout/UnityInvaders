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

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        DiedEntity diedEntity = obj as DiedEntity;

        if (diedEntity == null)
            return false;

        return this.Id == diedEntity.Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }
}

public class GameStatistics : MonoBehaviour
{
    public Text EventText;

    List<DiedEntity> diedAliens = new List<DiedEntity>();
    List<DiedEntity> destroyedDefenses = new List<DiedEntity>();
    private static object lockDefenses = new object();
    private static object lockAliens = new object();

    public int NumberOfDiedAliens { get { return diedAliens.Count; } }
    public int NumberOfDestroyedDefenses { get { return destroyedDefenses.Count; } }

    public void AddDefense(int id, float x, float z, float time)
    {
        lock(lockDefenses)
        {
            var diedEntity = new DiedEntity(id, x, z, time, EntityType.Defense);

            if (!destroyedDefenses.Contains(diedEntity))
            {
                destroyedDefenses.Add(diedEntity);
                EventText.text = string.Format("Defense {0} destroyed in position ({1:0.00},{2:0.00})", id, x, z);
            }
        }
    }

    public void AddAlien(int id, float x, float z, float time)
    {
        lock(lockAliens)
        {
            var diedEntity = new DiedEntity(id, x, z, time, EntityType.Alien);

            if (!diedAliens.Contains(diedEntity))
            {
                diedAliens.Add(diedEntity);
                EventText.text = string.Format("Alien {0} died in position ({1:0.00},{2:0.00})", id, x, z);
            }
        }
    }

    internal List<DiedEntity> GetDiedEntitiesOrderByTime()
    {
        List<DiedEntity> entities = new List<DiedEntity>(diedAliens);
        entities.AddRange(destroyedDefenses);
        entities = entities.OrderBy(x => x.Time).ToList();

        return entities;
    } 
}
