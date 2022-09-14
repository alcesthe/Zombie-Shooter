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
    private AudioSource audioSource;
    private GameManager gameManager;

    public bool canDealDamage = true;

    [SerializeField] float amountOfTimeToResetAttack = 2f;
    [SerializeField] float health = 50f;
    [SerializeField] float damage = 25f;
    [SerializeField] int point = 10;
    [SerializeField] AudioClip deadSound;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aICharacterControl = GetComponent<AICharacterControl>(); // Find the player in Scene and assign player to AICharacterControl.target
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        
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
        audioSource.PlayOneShot(deadSound);
        Destroy(gameObject);
        gameManager.AddPoint(point);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") && canDealDamage)
        {
            StartCoroutine(DealDamage(collision.transform.GetComponent<Player>()));
        }
    }

    IEnumerator DealDamage(Player player)
    {
        canDealDamage = false;
        player.TakeDamage(damage);
        aICharacterControl.enabled = false; // Stop moving

        yield return new WaitForSeconds(amountOfTimeToResetAttack);

        aICharacterControl.enabled = true; // Start chasing the character again
        canDealDamage = true;
    }
}
