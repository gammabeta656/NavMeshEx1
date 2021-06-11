using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class EnemyChaser : MonoBehaviour
{
    public GameObject player;

    public Vector3 lastPlayerPos;

    public float speed = 5f;

    public Vector3 Display;

    public bool visao;

    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject waypoint3;
    public GameObject waypoint4;

    public GameObject randomPosition1;
    public GameObject randomPosition2;

    public GameObject previousRandomPosition;

    public GameObject previousPosition;

    [Task]
    bool CanSeePlayer()
    {
        if (visao == true)
        {
            lastPlayerPos = player.transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    [Task]
    void GoToLastPlayerPos()
    {
        if (lastPlayerPos.y == 0)
        {
            Task.current.Succeed();
        }
        transform.position = Vector3.MoveTowards(transform.position, lastPlayerPos, speed / 2);
        if (transform.position.x == lastPlayerPos.x && transform.position.y == lastPlayerPos.y)
        {
            randomPosition1.transform.position = randomPos(lastPlayerPos);
            randomPosition2.transform.position = randomPos(lastPlayerPos);
            lastPlayerPos.y = 0;
            Task.current.Succeed();
        }
    }

    [Task]
    void Search()
    {
        if (previousRandomPosition.transform.position.y == 0)
        {
            Task.current.Succeed();
        }
        switch (previousRandomPosition.name)
        {
            case "randomWaypoint1":
                transform.position = Vector3.MoveTowards(transform.position, randomPosition2.transform.position, speed / 2);
                if (transform.position.x == randomPosition2.transform.position.x && transform.position.z == randomPosition2.transform.position.z)
                {
                    previousRandomPosition = randomPosition2;
                    Task.current.Succeed();
                }
                break;
            case "randomWaypoint2":
                Task.current.Succeed();
                break;
        }
    }

    Vector3 randomPos(Vector3 LastPlayerPos)
    {
        Vector3 minRange = new Vector3(LastPlayerPos.x, LastPlayerPos.y, LastPlayerPos.z);
        Vector3 maxRange = new Vector3(LastPlayerPos.x + 20, LastPlayerPos.y, LastPlayerPos.z + 20);

        return new Vector3(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y), Random.Range(minRange.z, maxRange.z));
    }

    [Task]
    void Wander()
    {
        switch (previousPosition.name)
        {
            case "Waypoint 1":
                transform.position = Vector3.MoveTowards(transform.position, waypoint2.transform.position, speed / 2);
                if (transform.position.x == waypoint2.transform.position.x && transform.position.z == waypoint2.transform.position.z)
                {
                    previousPosition = waypoint2;
                    Task.current.Succeed();
                }
                break;
            case "Waypoint 2":
                transform.position = Vector3.MoveTowards(transform.position, waypoint3.transform.position, speed / 2);
                if (transform.position.x == waypoint3.transform.position.x && transform.position.z == waypoint3.transform.position.z)
                {
                    previousPosition = waypoint3;
                    Task.current.Succeed();
                }
                break;
            case "Waypoint 3":
                transform.position = Vector3.MoveTowards(transform.position, waypoint4.transform.position, speed / 2);
                if (transform.position.x == waypoint4.transform.position.x && transform.position.z == waypoint4.transform.position.z)
                {
                    previousPosition = waypoint4;
                    Task.current.Succeed();
                }
                break;
            case "Waypoint 4":
                transform.position = Vector3.MoveTowards(transform.position, waypoint1.transform.position, speed / 2);
                if (transform.position.x == waypoint1.transform.position.x && transform.position.z == waypoint1.transform.position.z)
                {
                    previousPosition = waypoint1;
                    Task.current.Succeed();
                }
                break;
        }

    }


    [Task]
    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed / 32);
        Task.current.Succeed();
    }


    [Task]
    void ReportLastSeen()
    {
        lastPlayerPos = player.transform.position;
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, 20) && hit.collider.CompareTag("Player"))
        {
            visao = true;
        }
        else
        {
            visao = false;
        }
    }
}
