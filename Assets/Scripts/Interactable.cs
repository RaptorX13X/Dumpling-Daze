using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform objectPickUpTransform;
    [SerializeField] private float strength;
    [SerializeField] private float objectLerpSpeed = 10f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform objectPickUpTransform)
    {
        this.objectPickUpTransform = objectPickUpTransform;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        this.objectPickUpTransform = null;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
    }
    public void Throw(Vector3 transform, float modifier)
    {
        this.objectPickUpTransform = null;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform * (strength * modifier), ForceMode.Impulse);
        _rigidbody.useGravity = true;
    }
    
    private void Update()
    {
        if (objectPickUpTransform != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectPickUpTransform.position, Time.deltaTime * objectLerpSpeed);
            _rigidbody.transform.position = newPosition;
        }
    }
}
