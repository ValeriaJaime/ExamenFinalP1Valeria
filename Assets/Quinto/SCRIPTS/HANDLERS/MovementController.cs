using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private InputSystem inputSystem;

    [SerializeField] private float speed = 5;
    [SerializeField] private float rotateSpeed = 12;
    [SerializeField] private float walkingSpeed = 5;
    [SerializeField] private float runningSpeed = 10;
    [SerializeField] private float jumpForce = .1f;

    Vector2 moveDirection = Vector2.zero;


    private Action Movement;
    private Action Rotation;
    private Action Shoot;
    private Action Jump;
    private Action Run;
    private Action Reload;

    private PlayerInput input;

    private Rigidbody playerRb;

    public void TriggerEnter()
    {
        Debug.Log(gameObject.name + "  entró en un trigger");
    }

    public void TriggerExit()
    {
        Debug.Log(gameObject.name + " salió de un trigger");
    }

    public void TriggerStay() 
    {
        Debug.Log(gameObject.name + " se queda en el trigger");
    }

    private void OnValidate()
    {
        switch (inputSystem)
        {
            case InputSystem.OldInputSystem:
                {
                    Movement = OldInputMovement;
                    Rotation = OldInputRotation;
                    Jump = OldInputJump;
                    Shoot = OldShootInput;
                    Reload = OldReloadInput;
                    break;
                }

            case InputSystem.NewInputSystem: 
                {
                    Movement = NewInputMovement;
                    Rotation = NewInputRotation;
                    Jump = NewJumpInput;
                    Shoot = NewShootInput;
                    Reload = NewReloadInput;
                    break;
                }
        }
    }

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        Rotation();
        Jump();
        //Shoot();
        Reload();
    }   

    void NewInputMovement()
    {
        playerRb.position += this.transform.rotation * new Vector3(x: 0, y: 0, z: NewMoveDirection()) * (NewActualSpeed() * Time.deltaTime);
        //transform.position += this.transform.rotation * new Vector3(x: 0, y: 0, z: NewMoveDirection()) * (ActualSpeed() * Time.deltaTime);
    }

    void NewInputRotation()
    {
        transform.Rotate(new Vector3(x: 0, y: NewRotateDirection(), z: 0) * (rotateSpeed * Time.deltaTime));
    }

    float NewMoveDirection()
    {
        return input.actions["Move"].ReadValue<Vector2>().y;
    }

    float NewRotateDirection()
    {
        return input.actions["Move"].ReadValue<Vector2>().x;
    }

    void OldInputMovement()
    {
        playerRb.position += this.transform.rotation * new Vector3(x: 0, y: 0, z: OldMoveDirection().y) * (OldActualSpeed() * Time.deltaTime);
        //transform.position += this.transform.rotation * new Vector3(x: 0, y: 0, z: OldMoveDirection().y) * (ActualSpeed() * Time.deltaTime);
    }

    void OldInputRotation()
    {
        transform.Rotate(new Vector3(x: 0, y: OldRotateDirection().x, z: 0) * (rotateSpeed * Time.deltaTime));
    }

    Vector2 OldMoveDirection()
    {
        if (InputHandler.MoveForwardInput())
        {
            moveDirection += Vector2.up;
        }

        if (InputHandler.MoveBackwardInput())
        {
            moveDirection += Vector2.down;
        }

        return moveDirection != Vector2.zero ? moveDirection.normalized : Vector2.zero;
    }

    Vector2 OldRotateDirection()
    {
        if (InputHandler.RotateLeftInput())
        {
            moveDirection += Vector2.left;
        }

        if (InputHandler.RotateRightInput())
        {
            moveDirection += Vector2.right;
        }

        return moveDirection != Vector2.zero ? moveDirection.normalized : Vector2.zero;
    }

    private float NewActualSpeed()
    {
        return input.actions["Run"].WasPressedThisFrame() ? runningSpeed : walkingSpeed;
    }

    private float OldActualSpeed()
    {
        // Si estoy presionando el input de correr, va a regresar runningSpeed, si no va a regresar walkingSpeed
        return InputHandler.RunInput() ? runningSpeed : walkingSpeed;
    }

    void NewJumpInput()
    {
        //input.actions["Jump"].WasPressedThisFrame();
        if (input.actions["Jump"].WasPressedThisFrame())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Salta nuevo");
        }
    }

    void OldInputJump()
    {
        if (InputHandler.JumpInput())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Salta viejo");
        }
    }

    void NewShootInput()
    {
        if (input.actions["Shoot"].WasPressedThisFrame())
        {
            Debug.Log("Dispara nuevo");
        }
    }

    void OldShootInput()
    {
        if (InputHandler.ShootInput())
        {
            Debug.Log("Dispara viejo");
        }
    }

    void NewReloadInput()
    {
        if (input.actions["Reload"].WasPressedThisFrame())
        {
            Debug.Log("Recarga nuevo");
        }
    }

    void OldReloadInput()
    {
        if (InputHandler.ReloadInput())
        {
            Debug.Log("Recarga viejo");
        }
    }
}

public enum InputSystem
{
    OldInputSystem, NewInputSystem
}
