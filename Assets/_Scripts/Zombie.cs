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
    [SerializeField] float health = 50f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aICharacterControl = GetComponent<AICharacterControl>(); // Find the player in Scene and assign player to AICharacterControl.target
        player = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(ToggleNavMeshAgentAndAIScript());
    }

    IEnumerator ToggleNavMeshAgentAndAIScript()
    {
        // Toggle the Nav Mesh Agent for the AI start working after Instantiate
        navMeshAgent.enabled = false;
        aICharacterControl.enabled = false;

        yield return new WaitForSeconds(3.0f); // Wait for the zombie hit the ground (or nav mesh)

        navMeshAgent.enabled = true;
        aICharacterControl.enabled = true;
        aICharacterControl.target = player.transform;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
