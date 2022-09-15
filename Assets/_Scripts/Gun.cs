using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;

    private UnityEvent onInteract;
    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Light muzzleLight;
    [SerializeField] GameObject impactEffect;
    [SerializeField] AudioClip gunSFX;

    private AudioSource audioSource;
    private LevelLoader levelLoader;
    public LayerMask interactableLayermask = 6;
    private Player player;

    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelLoader = FindObjectOfType<LevelLoader>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        Interact();
    }

    private void Shoot()
    {
        StartCoroutine(PlaySFFAndVFX());

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Zombie zombie = hit.transform.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    private void Interact()
    {
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 2, interactableLayermask))
        {
            if(hit.collider.GetComponent<Helicopter>())
            {
                GameManager gameManager = FindObjectOfType<GameManager>();
                gameManager.SetMessage("Press E to escape");

                onInteract = hit.collider.GetComponent<Helicopter>().onInteract;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StopAllCoroutines();
                    gameManager.isEscaped = true;
                    onInteract.Invoke();
                }
            }
        }
    }

    IEnumerator PlaySFFAndVFX()
    {
        muzzleFlash.Play();
        muzzleLight.enabled = true;
        audioSource.PlayOneShot(gunSFX);
        yield return new WaitForSeconds(0.05f);
        muzzleLight.enabled = false;
    }
}
