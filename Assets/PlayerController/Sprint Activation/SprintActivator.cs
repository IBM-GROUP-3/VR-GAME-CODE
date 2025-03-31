using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintActivator : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var sprinting = other.GetComponent<PlayerSprint>();
            if (sprinting != null)
            {
                sprinting.enabled = true;
                Destroy(gameObject);
            }
        }
    }
}
