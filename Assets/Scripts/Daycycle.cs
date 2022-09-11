using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daycycle : MonoBehaviour
{   
    [Tooltip ("Number of minutes per second that pass")]
    [SerializeField] float minutesPerSecond = 60f;

    private Quaternion startRotation;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float angleThisFrame = Time.deltaTime / 360 * minutesPerSecond;
        transform.RotateAround(transform.position, Vector3.forward, angleThisFrame);
    }
}
