using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using System;
using System.Linq;

public class MoveAlien : MonoBehaviour
{
	public Transform target;
	public GameObject map;
	public IStrategyAlienAttack strategyAlienAttack;
	private IMap iMap;
	private List<Vector3> positions = new List<Vector3>();
	private Vector3 actualGoal;
    private UnityAlien unityAlien;
	private bool completeGoal = false;
	private IList<IObstacle> iObstacles;
	private IList<IDefense> iDefenses = new List<IDefense>();

	// Use this for initialization
	void Start ()
	{
		map = GameObject.FindGameObjectWithTag("Floor");
		iMap = map.GetComponent<IMap>();
        unityAlien = GetComponent<UnityAlien>();
        iObstacles = iMap.Obstacles;
		//iDefenses = iMap.Defenses;
		
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
				iObstacles, iDefenses, iMap.Size, iMap.CellSize));

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
		}
	}

	private Transform GetTarget()
	{
		if (target == null)
		{
			IDefense defense = strategyAlienAttack.GetNextDefenseToAttack(GetComponent<IAlien>().Position,
				iObstacles, iMap.Defenses, iMap.Size, iMap.CellSize);

			foreach (var defenseObject in GameObject.FindGameObjectsWithTag("Defense"))
			{
				if (defenseObject.GetComponent<IDefense>().Id == defense.Id)
					return defenseObject.transform;
			}

			return null;
		}

		return target;
	}

	private Vector3 CellCenterToPosition(float i, float j, float cellWidth, float cellHeight)
	{
		return new Vector3((i * cellWidth) + (cellWidth * 0.5f), 3, (j * cellHeight) - (cellHeight * 0.5f));
	}

	void FixedUpdate()
	{
		if (!target || completeGoal)
		{            
			if (!target)  
				StartAttack();

			return;
		}        

		if ((Mathf.Abs(transform.position.x - target.position.x)) < unityAlien.Range &&
			(Mathf.Abs(transform.position.z - target.position.z)) < unityAlien.Range)
		{
			transform.LookAt(target);
            unityAlien.Target = target;
			completeGoal = true;
            return;
		}

		if ((Mathf.Abs(transform.position.x - actualGoal.x)) < 1 &&
			(Mathf.Abs(transform.position.z - actualGoal.z)) < 1)
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
                transform.position = GetRandomPositionInCell(transform.position);
                return;
			}
		}

		Move();
	}

    private Vector3 GetRandomPositionInCell(Vector3 position)
    {
        int xCell = Mathf.RoundToInt(position.x / iMap.CellSize);
        int zCell = Mathf.RoundToInt(position.z / iMap.CellSize);

        int newXPosition = RandomManager.GetRandomNumber(xCell, xCell + 1);
        int newZPosition = RandomManager.GetRandomNumber(zCell, zCell + 1);

        return new Vector3(newXPosition, position.y, newZPosition);
    }

    private void Move()
	{
		if (actualGoal != Vector3.zero)
		{
			Vector3 motion = actualGoal - transform.position;
			motion.Normalize();
			motion.y = 0;
			transform.position += motion * Time.deltaTime * 30f;
		}
	}

	public void ChangeTarget(Transform newTarget)
	{
		if (target == newTarget)
			return;

		target = newTarget;

		StartAttack();
	}
}
