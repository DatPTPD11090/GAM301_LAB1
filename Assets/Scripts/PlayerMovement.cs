using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A (-1) / D (1)
        float vertical = Input.GetAxis("Vertical");     // W (1) / S (-1)

        // Tính hướng di chuyển theo input
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;

        // Xoay nhân vật theo hướng di chuyển
        if (move.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Áp dụng trọng lực để nhân vật không lơ lửng
        moveDirection.y -= gravity * Time.deltaTime;

        // Di chuyển nhân vật
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
