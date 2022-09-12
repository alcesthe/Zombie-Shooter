using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour
{
    private GameObject player;
    private AICharacterControl aICharacterControl;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Find the player in Scene and assign player to AICharacterControl.target
        aICharacterControl = GetComponent<AICharacterControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Debug.Log(player.transform);
            aICharacterControl.target = player.transform;
        }
        StartCoroutine(ToggleNavMeshAgent());
    }

    IEnumerator ToggleNavMeshAgent()
    {
        // Toggle the Nav Mesh Agent for the AI start working after Instantiate
        navMeshAgent.enabled = false;
        yield return new WaitForSeconds(3.0f);
        navMeshAgent.enabled = true;
    }
}
