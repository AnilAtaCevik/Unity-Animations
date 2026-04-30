using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Transform cameraMain;
    
    [SerializeField] InputAction movementInput;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 15f;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraMain = Camera.main.transform;
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
        
        Vector2 animVector = inputVector;
        if (animVector.magnitude > 0)
        {
            float maxVal = Mathf.Max(Mathf.Abs(animVector.x), Mathf.Abs(animVector.y));
            animVector /= maxVal; 
        }

        animator.SetFloat("MoveX", animVector.x, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveY", animVector.y, 0.1f, Time.deltaTime);

        Vector3 cameraForward = cameraMain.forward;
        cameraForward.y = 0; 
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (inputVector != Vector2.zero)
        {
            Vector3 moveDirection = (transform.forward * inputVector.y + transform.right * inputVector.x).normalized;
            transform.position += moveDirection * movementSpeed * Time.deltaTime;
        }
    }
}