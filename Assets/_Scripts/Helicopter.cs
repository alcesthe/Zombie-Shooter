using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] AudioClip callSound;
    [SerializeField] float helicopterSpeed = 50f;

    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private bool called = false;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Call()
    {
        if (!called)
        {
            audioSource.PlayOneShot(callSound);
            audioSource.spatialBlend = 0; // Turn 2D for global sound
            Invoke("ChangeSoundSpatialBlendTo3D", callSound.length); // Turn it back 3D to only hear when near
            rigidbody.velocity = new Vector3(0, 0, helicopterSpeed);
            called = true;
        } 
    }

    private void ChangeSoundSpatialBlendTo3D()
    {
        audioSource.spatialBlend = 1;
    }
}
