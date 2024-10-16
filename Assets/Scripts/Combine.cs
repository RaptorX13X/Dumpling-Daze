using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine : MonoBehaviour
{
    [SerializeField] private GameObject objectToCombineWith;
    [SerializeField] private GameObject combinedObject;
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(objectToCombineWith.tag))
            {
                Instantiate(combinedObject, collision.gameObject.transform.position, combinedObject.transform.rotation);
                //combinedObject.transform.position = collision.gameObject.transform.position;
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
}
