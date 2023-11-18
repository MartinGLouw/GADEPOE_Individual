using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RacerType
{
    Slow,
    Medium,
    Fast
}

public interface InterfaceRacer
{
    void Speed();
    RacerType Type { get; }
}

public class Slow : InterfaceRacer
{
    public void Speed()
    {
        
    }

    public RacerType Type => RacerType.Slow;
}

public class Medium : InterfaceRacer
{
    public void Speed()
    {
        
    }

    public RacerType Type => RacerType.Medium;
}

public class Fast : InterfaceRacer
{
    public void Speed()
    {
        
    }

    public RacerType Type => RacerType.Fast;
}

public interface RacerFactory
{
    InterfaceRacer CreateRacer();
}

public class Factory : RacerFactory
{
    public InterfaceRacer CreateRacer()
    {
        int racerType = UnityEngine.Random.Range(0, 3);
        switch (racerType)
        {
            case 0:
                return new Slow();
            case 1:
                return new Medium();
            case 2:
                return new Fast();
            default:
                throw new ApplicationException("Invalid racer type.");
        }
    }
}

public class FactoryRaceManager : MonoBehaviour
{
    public GameObject slow;
    public GameObject medium;
    public GameObject fast;
    private RacerFactory Factory = new Factory();
    public GameObject[] SpawnPoints;
    public InterfaceRacer[] racers = new InterfaceRacer[5];
    

    

    void Start()
    {
        racers = new InterfaceRacer[SpawnPoints.Length]; 
        CreateRacers();
        Spawning();
    }


    public void CreateRacers()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            InterfaceRacer Racer = Factory.CreateRacer();
            racers[i] = Racer;
            Racer.Speed();
        }
    }

    public WaypointHolder waypointHolder;

    public void Spawning()
    {
        for (int i = 0; i < racers.Length; i++)
        {
            GameObject racer = null;
            switch (racers[i].Type)
            {
                case RacerType.Slow:
                    racer = Instantiate(slow, SpawnPoints[i].transform.position, transform.rotation);
                    break;
                case RacerType.Medium:
                    racer = Instantiate(medium, SpawnPoints[i].transform.position, transform.rotation);
                    break;
                case RacerType.Fast:
                    racer = Instantiate(fast, SpawnPoints[i].transform.position, transform.rotation);
                    break;
                default:
                    throw new ApplicationException("Invalid racer type.");
            }

            // Assign the starting waypoint to the racer
            var moveToScript = racer.GetComponent<GraphMoveTo>();
            if (moveToScript != null)
            {
                moveToScript.startingWaypoint = waypointHolder.startingWaypoint;
            }
        }
    }


}