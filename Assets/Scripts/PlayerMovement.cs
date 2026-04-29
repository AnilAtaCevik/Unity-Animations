using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] InputAction movementInput;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 720f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        movementInput.Enable();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 inputVector = movementInput.ReadValue<Vector2>();

        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
            
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}