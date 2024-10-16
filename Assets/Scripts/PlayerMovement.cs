using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Vector3 velocity;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform feet; // empty game object in players feet
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    public static string surfaceType;

    // Dodaj referencje do skrypt�w zarz�dzaj�cych d�wi�kami krok�w
    //[SerializeField] private WoodenFootstepManager woodenFootstepManager;
    [SerializeField] private TileFootstepManager tileFootstepManager;

    private bool grounded;
    private Vector3 lastFootstepPosition;

    [SerializeField] private float minFootstepDistance = 0.1f;

    private void Start()
    {
        throwModifier = 0f;
        lastFootstepPosition = transform.position;
    }

    void Update()
    {
        grounded = Physics.CheckSphere(feet.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;

            // Sprawd� rodzaj powierzchni pod stopami gracza
            string surfaceType = DetectSurfaceType();

            // Wywo�aj odpowiedni skrypt zarz�dzaj�cy d�wi�kami krok�w na odpowiedniej powierzchni
            if (surfaceType == "Wooden")
            {
                //woodenFootstepManager.UpdateFootstepEvent();
            }
            else if (surfaceType == "Tile")
            {
                tileFootstepManager.UpdateFootstepEvent();
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move.normalized * (speed * Time.deltaTime));
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Oblicz odleg�o�� mi�dzy aktualn� pozycj� a ostatni� pozycj�
        float distanceSinceLastFootstep = Vector3.Distance(transform.position, lastFootstepPosition);

        // Wywo�aj metod� aktualizacji d�wi�ku krok�w tylko gdy odleg�o�� jest wi�ksza ni� minimalna
        if (distanceSinceLastFootstep > minFootstepDistance && grounded)
        {
            if (surfaceType == "Wooden")
            {
                //woodenFootstepManager.UpdateFootstepEvent();
            }
            else if (surfaceType == "Tile")
            {
                tileFootstepManager.UpdateFootstepEvent();
            }
            lastFootstepPosition = transform.position;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (interactable == null)
        //    {
        //        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward,
        //                out RaycastHit raycastHit, pickUpRange, interactableLayer))
        //        {
        //            if (raycastHit.transform.TryGetComponent(out interactable))
        //            {
        //                interactable.PickUp(pickUpPoint);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        interactable.Drop();
        //        interactable = null;
        //    }
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (interactable != null)
        //        StartCoroutine(ForceCalc());
        //}
        //if (Input.GetMouseButtonUp(1))
        //{
        //    if (interactable != null)
        //    {
        //        StopAllCoroutines();
        //        interactable.Throw(transform.forward, throwModifier, transform.up);
        //        interactable = null;
        //        throwModifier = 0;
        //        //reset Carry Point position to the original position
        //    }
        //}
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

    string DetectSurfaceType()
    {
        RaycastHit hit;
        if (Physics.Raycast(feet.position, Vector3.down, out hit, groundDistance + 0.1f, groundMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("WoodenSurface"))
            {
                return "Wooden";
            }
            else if (hitObject.CompareTag("TileSurface"))
            {
                return "Tile";
            }
        }
        return null;
    }
}
