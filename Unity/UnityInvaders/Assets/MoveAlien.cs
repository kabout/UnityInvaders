using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using System;
using System.Linq;

public class MoveAlien : MonoBehaviour
{
    public Transform target;
    public GameObject map;
    public GameObject[] obstacles;
    public IStrategyAlienAttack strategyAlienAttack;
    private IMap iMap;
    private List<Vector3> positions = new List<Vector3>();
    private Vector3 actualGoal;
    private bool completeGoal = false;
    private IList<IObstacle> iObstacles;
    private IList<IDefense> iDefenses;

    // Use this for initialization
    void Start ()
    {
        map = GameObject.FindGameObjectWithTag("Floor");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        iMap = map.GetComponent<IMap>();
        iObstacles = iMap.Obstacles;
        
        StartAttack();
	}

    private void StartAttack()
    {
        try
        {
            target = GetTarget();

            if (target == null)
                return;

            positions = ConvertPosition.Convert(strategyAlienAttack.CalculatePath(
                ConvertPosition.Convert(transform.position),
                ConvertPosition.Convert(target.position),
                iObstacles, iMap.Defenses, iMap.Size, iMap.CellSize));

            for (int i = 0; i < positions.Count; i++)
                positions[i] = CellCenterToPosition(positions[i].x, positions[i].z, iMap.CellSize, iMap.CellSize);

            if (positions.Count > 1)
            {
                positions.RemoveAt(0);
                actualGoal = positions.First();
                transform.LookAt(actualGoal);
                completeGoal = false;
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private Transform GetTarget()
    {
        IDefense defense = strategyAlienAttack.GetNextDefenseToAttack(GetComponent<IAlien>().Position, 
            iObstacles, iMap.Defenses, iMap.Size, iMap.CellSize);        

        foreach(var defenseObject in GameObject.FindGameObjectsWithTag("Defense"))
        {
            if (defenseObject.GetComponent<IDefense>().Id == defense.Id)
                return defenseObject.transform;
        }

        return null;
    }

    private Vector3 CellCenterToPosition(float i, float j, float cellWidth, float cellHeight)
    {
        return new Vector3((i * cellWidth) + cellWidth * 0.5f, 3, (j * cellHeight) + cellHeight * 0.5f);
    }

    void FixedUpdate()
    {
        if (!target || completeGoal)
        {            
            if (!target)  
                StartAttack();

            return;
        }        

        if ((Mathf.Abs(transform.position.x - target.position.x)) < 20 && (Mathf.Abs(transform.position.z - target.position.z)) < 20)
        {
            transform.LookAt(target);
            GetComponent<UnityAlien>().Target = target;
            completeGoal = true;
        }

        if ((Mathf.Abs(transform.position.x - actualGoal.x)) < 1 && (Mathf.Abs(transform.position.z - actualGoal.z)) < 1)
        {
            if(positions.Any())
                positions.RemoveAt(0);

            if (positions.Count > 0)
            {
                actualGoal = positions.First();
                transform.LookAt(actualGoal);
            }
            else
            {
                completeGoal = true;
            }
        }

        Move();
    }

    private void Move()
    {
        Vector3 motion = actualGoal - transform.position;
        motion.Normalize();
        transform.position += motion * 0.5f;
    }

    public void ChangeTarget(Transform newTarget)
    {
        if (target == newTarget)
            return;

        target = newTarget;

        StartAttack();
    }


    private int ManhattanHeuristic(IPosition newNode, IPosition end)
    {
        return (Math.Abs((int)Math.Round(newNode.X - end.X)) + Math.Abs((int)Math.Round(newNode.Z - end.Z)));
    }
}
