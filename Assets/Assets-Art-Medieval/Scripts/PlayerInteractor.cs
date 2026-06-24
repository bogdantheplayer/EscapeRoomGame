using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera;
    public float sphereRadius = 0.3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

            if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, interactDistance))
            {
                RoomQuizSimple quiz = hit.collider.GetComponent<RoomQuizSimple>();
                if (quiz != null)
                {
                    quiz.Interact();
                    return;
                }
                InteractableInfo info = hit.collider.GetComponent<InteractableInfo>();
                if (info != null)
                {
                    info.Interact();
                }
            }
        }
    }
}