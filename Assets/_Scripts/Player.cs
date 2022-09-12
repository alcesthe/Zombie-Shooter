using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Helicopter helicopter;
    [SerializeField] GameObject spawnPoints;
    [SerializeField] AudioClip startVoice;
    [SerializeField] GameObject landingArea;
    
    private Transform[] listOfSpawnPointsTransform;
    private CharacterController characterController;
    private AudioSource audioSource;
    private bool respawn = false;
    private bool lastToggle = false;
    
    // Start is called before the first frame update
    void Start()
    {
        listOfSpawnPointsTransform = spawnPoints.GetComponentsInChildren<Transform>();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        PlayPlayerBeginSound();
    }

    private void PlayPlayerBeginSound()
    {
        audioSource.PlayOneShot(startVoice);
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

    private void OnFindClearArea()
    {
        helicopter.Call();
        Instantiate(landingArea, transform.position, transform.rotation);
    }
}
