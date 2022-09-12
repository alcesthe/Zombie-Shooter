using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingArea : MonoBehaviour
{
    private Transform waypoints;
    private Transform[] listOfWaypoints;

    void Awake()
    {
        waypoints = gameObject.transform.GetChild(1);
        listOfWaypoints = waypoints.GetComponentsInChildren<Transform>();
    }

    public Transform[] GetListOfWaypoints()
    {
        return listOfWaypoints;
    }
}
