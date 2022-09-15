using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Helicopter helicopter;
    [SerializeField] GameObject spawnPoints;
    [SerializeField] AudioClip startVoice;
    [SerializeField] GameObject landingArea;
    [SerializeField] float health = 100f;
    [SerializeField] AudioClip deadSound;
    
    private Transform[] listOfSpawnPointsTransform;
    private CharacterController characterController;
    private AudioSource audioSource;
    private LevelLoader levelLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        listOfSpawnPointsTransform = spawnPoints.GetComponentsInChildren<Transform>();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        levelLoader = FindObjectOfType<LevelLoader>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Spawn();
        PlayPlayerBeginSound();
    }

    private void PlayPlayerBeginSound()
    {
        audioSource.PlayOneShot(startVoice);
    }

    public void Spawn()
    {
        health = 100;
        Time.timeScale = 1; 

        characterController.enabled = false; // Disable to teleport the player

        int index = Random.Range(1, listOfSpawnPointsTransform.Length);
        transform.position = listOfSpawnPointsTransform[index].transform.position;

        characterController.enabled = true; // Turn it back on when done teleport the player
    }

    // Reference in ClearArea.cs SendMessage method
    private void OnFindClearArea()
    {
        Instantiate(landingArea, transform.position, transform.rotation);
        helicopter.Call();
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
        Time.timeScale = 0;
        levelLoader.LoadEndScene();
    }


    public float GetHealth()
    {
        return health;
    }
}
