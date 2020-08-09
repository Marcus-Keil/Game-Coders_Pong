using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isBump1;
    [SerializeField] float moveSpeed = 25.0f;
    [SerializeField] float Z_Boundary = 3.6f;

    private void LateUpdate()
    {
        if (isBump1)
        {
            float moveDirection = Input.GetAxis("Vertical");
            if (moveDirection > 0 && transform.position.z < Z_Boundary)
                transform.Translate(0, 0, moveDirection * moveSpeed * Time.deltaTime);
            if (moveDirection < 0 && transform.position.z > -Z_Boundary)
                transform.position = transform.position + new Vector3(0, 0, moveDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            float moveDirection = Input.GetAxis("Vertical2");
            if (moveDirection > 0 && transform.position.z < Z_Boundary)
                transform.Translate(0, 0, moveDirection * moveSpeed * Time.deltaTime);
            if (moveDirection < 0 && transform.position.z > -Z_Boundary)
                transform.position = transform.position + new Vector3(0, 0, moveDirection * moveSpeed * Time.deltaTime);
        }

    }
}