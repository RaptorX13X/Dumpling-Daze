using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private float pickUpRange;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform pickUpPoint;
    [SerializeField] private float throwModifier;
    [SerializeField] private Interactable interactable;

    private void Start()
    {
        throwModifier = 0f;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move.normalized * (speed * Time.deltaTime));

        if (Input.GetMouseButtonDown(0))
        {
            if (interactable == null)
            {
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward,
                        out RaycastHit raycastHit, pickUpRange, interactableLayer))
                {
                    if (raycastHit.transform.TryGetComponent(out interactable))
                    {
                        interactable.PickUp(pickUpPoint);
                    }
                }
            }
            else
            {
                interactable.Drop();
                interactable = null;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(interactable != null)
                StartCoroutine(ForceCalc());
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (interactable != null)
            {
                StopAllCoroutines();
                interactable.Throw(transform.forward, throwModifier);
                interactable = null;
                throwModifier = 0;
                //reset Carry Point position to the original position
            }
        }

        IEnumerator ForceCalc()
        {
            do
            {
                yield return new WaitForSeconds(0.2f);
                throwModifier += 1;
                //move Carry Point position toward the player closer by 0.1 (starts at z:-2, finishes do-while at z:-1)
            } while (throwModifier < 10);
        }
    }
}
