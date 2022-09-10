using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject spawnPoints;
    [SerializeField] bool respawn = false;

    private Transform[] listOfSpawnPointsTransform;
    private bool lastToggle = false;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        listOfSpawnPointsTransform = spawnPoints.GetComponentsInChildren<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastToggle != respawn)
        {
            Respawn();
            respawn = false;
        } else
        {
            lastToggle = respawn;
        }
    }

    public void Respawn()
    {
        characterController.enabled = false; // Disable to teleport the player

        int index = Random.Range(1, listOfSpawnPointsTransform.Length);
        transform.position = listOfSpawnPointsTransform[index].transform.position;

        characterController.enabled = true; // Turn it back on when done teleport the player
    }
}
