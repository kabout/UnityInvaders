using UnityEngine;
using System.Collections;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Controllers;
using System.Collections.Generic;
using StrategyAlienAttack;

public class MoveAlien : MonoBehaviour {

    public Transform source;
    public Transform target;
    public GameObject map;
    public GameObject[] obstacles;
    private List<Vector3> positions;
    private Vector3 actualGoal;
    private bool completeGoal = false;
    private int cellSize = 10;
    List<IObstacle> iObstacles;

    // Use this for initialization
    void Start ()
    {
        iObstacles = new List<IObstacle>();

        for(int i =0; i < obstacles.Length; i++)
            iObstacles.Add(obstacles[i].GetComponent<UnityObstacle>());

        IStrategyAlienAttack strategyAlienAttack = new StrategyAlienAttack.StrategyAlienAttack();
        positions = strategyAlienAttack.CalculatePath(source.position, target.position, iObstacles, new List<IDefense>(), 300, cellSize);

        for(int i = 0; i < positions.Count; i++)
            positions[i] = CellCenterToPosition(positions[i].x, positions[i].z, cellSize, cellSize);

        positions.RemoveAt(0);
        actualGoal = positions[0];
	}

    private Vector3 CellCenterToPosition(float i, float j, float cellWidth, float cellHeight)
    {
        return new Vector3((i * cellWidth) + cellWidth * 0.5f, 0, (j * cellHeight) + cellHeight * 0.5f);
    }
    // Update is called once per frame
    void Update ()
    {
        if (completeGoal)
            return;

        if ((Mathf.Abs(source.position.x - target.position.x)) < 20 && (Mathf.Abs(source.position.z - target.position.z)) < 20)
        {
            source.LookAt(target);
            completeGoal = true;
        }

        if ((Mathf.Abs(source.position.x - actualGoal.x)) < 1 && (Mathf.Abs(source.position.z - actualGoal.z)) < 1)
        {
            positions.RemoveAt(0);

            if (positions.Count > 0)
            {
                actualGoal = positions[0];
                source.LookAt(actualGoal);
            }
            else
                completeGoal = true;
        }

        Vector3 motion = actualGoal - source.position;
        motion.Normalize();
        source.position += motion * 0.5f;
    }

    public void ChangeTarget(Transform newTarget)
    {
        if (target == newTarget)
            return;

        target = newTarget;

        IStrategyAlienAttack strategyAlienAttack = new StrategyAlienAttack.StrategyAlienAttack();
        positions = strategyAlienAttack.CalculatePath(source.position, target.position, iObstacles, new List<IDefense>(), 300, cellSize);

        for (int i = 0; i < positions.Count; i++)
            positions[i] = CellCenterToPosition(positions[i].x, positions[i].z, cellSize, cellSize);

        if (positions.Count > 0)
        {
            positions.RemoveAt(0);
            actualGoal = positions[0];
            source.LookAt(actualGoal);
            completeGoal = false;
        }
    }

}
