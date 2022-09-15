using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Helicopter : MonoBehaviour
{
    [SerializeField] AudioClip callSound;
    [SerializeField] float helicopterSpeed = 50f;

    public UnityEvent onInteract;
    private AudioSource audioSource;
    private LandingArea landingArea;
    private bool called = false;
    private Transform[] listOfWaypoints;
    private int waypointIndex = 1;


    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void FixedUpdate()
    {
        if (called)
        {
            Move();
        }
    }

    public void Call()
    {
        if (!called)
        {
            landingArea = FindObjectOfType<LandingArea>();
            listOfWaypoints = landingArea.GetListOfWaypoints();

            PlayCallSound();

            called = true;
        }
    }

    private void PlayCallSound()
    {
        audioSource.PlayOneShot(callSound);
        audioSource.spatialBlend = 0; // Turn 2D for global sound
        Invoke("ChangeSoundSpatialBlendTo3D", callSound.length); // Turn it back 3D to only hear when near
    }

    private void Move()
    {
        if (waypointIndex < listOfWaypoints.Length)
        {
            var targetPosition = listOfWaypoints[waypointIndex].transform.position;
            var moveSpeed = helicopterSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
    }

    private void ChangeSoundSpatialBlendTo3D()
    {
        audioSource.spatialBlend = 1;
    }
}
