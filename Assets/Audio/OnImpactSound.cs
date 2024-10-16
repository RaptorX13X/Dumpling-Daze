using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class OnImpactSound : MonoBehaviour
{
    [SerializeField] string[] eventPaths;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            string eventPath = eventPaths[UnityEngine.Random.Range(0, eventPaths.Length)];
            RuntimeManager.PlayOneShot(eventPath, transform.position);
        }
    }
}

