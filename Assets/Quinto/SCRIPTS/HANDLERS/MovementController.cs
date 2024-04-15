using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private InputSystem inputSystem;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    Vector2 moveDirection = Vector2.zero;
    Vector2 rotateDirection = Vector2.zero;

    private Action Movement;
    private Action Rotation;

    private PlayerInput input;


//#if UNITY_EDITOR        //que esto así porque funciona en el editor nada más

//    private void OnValidate()
//    {
//        switch (inputSystem)
//        {
//            case InputSystem.OldInputSystem:
//                {
//                    Movement = OldInputSystemMovement;
//                    Rotation = OldInputSystemRotation;
//                    break;
//                }

//            case InputSystem.NewInputSystem:
//                {
//                    Movement = NewInputSystemMovement;
//                    Rotation = NewInputSystemRotation;
//                    break;
//                }
//        }
//    }

//#endif

    private void Start()            //y que esto lo pongamos en el Start porque si no no hace nada
    {
        Debug.Log("Hola start");
        input = GetComponent<PlayerInput>();

        switch (inputSystem)
        {
            case InputSystem.OldInputSystem:
                {
                    Movement = OldInputSystemMovement;
                    Rotation = OldInputSystemRotation;
                    break;
                }

            case InputSystem.NewInputSystem:
                {
                    Movement = NewInputSystemMovement;
                    Rotation = NewInputSystemRotation;
                    break;
                }
        }
    }

    private void FixedUpdate()   //que lo cambiemos a FixUpdate
    {
        Movement();
        Rotation();
    }

    #region New Input System

    private void NewInputSystemMovement()
    {
        transform.position += this.transform.rotation * new Vector3(0, 0, NewInputSystemMovementDirection()) * (movementSpeed * Time.deltaTime);
    }

    private void NewInputSystemRotation()
    {

        transform.Rotate(new Vector3(0, NewInputSystemRotationDirection(), 0) * (rotationSpeed * Time.deltaTime));

    }

    private float NewInputSystemMovementDirection()
    {

        return input.actions["Move"].ReadValue<Vector2>().y;

    }

    private float NewInputSystemRotationDirection()
    {

        return input.actions["Move"].ReadValue<Vector2>().x;

    }

    #endregion

    #region Old Input System

    private void OldInputSystemRotation()
    {
        transform.Rotate(new Vector3(0, OldSystemRotationDirection().x, 0) * (rotationSpeed * Time.deltaTime));
    }

    private void OldInputSystemMovement()
    {
        transform.position += this.transform.rotation * new Vector3(0, 0, OldSystemMovementDirection().y) * (movementSpeed * Time.deltaTime);
    }

    private Vector2 OldSystemMovementDirection()
    {
        moveDirection = Vector2.zero;

        if (InputHandler.MoveForwardInput())
        {

            moveDirection += Vector2.up;

        }

        if (InputHandler.MoveBackwardInput())
        {

            moveDirection += Vector2.down;

        }

        //if (!InputHandler.MoveForwardInput() && !InputHandler.MoveBackwardInput())    QUITA ESTE, que porque el signo ! significa que no estás tocando el input,
        //pero si no lo tocaste no deberías tener un if para no tocarlo.
        //{

        //    moveDirection = Vector2.zero;

        //}

        return moveDirection.normalized;

    }

    private Vector2 OldSystemRotationDirection()
    {
        rotateDirection = Vector2.zero;     //LE PONEMOS EL ZERO PARA QUE TENGA UNA RESPUESTA, PARA QUE NO SEA NULO

        if (InputHandler.RotateRightInput())
        {

            rotateDirection += Vector2.right;

        }
        if (InputHandler.RotateLeftInput())
        {

            rotateDirection += Vector2.left;

        }
        //if (!InputHandler.RotateRightInput() && !InputHandler.RotateLeftInput())    QUITAMOS ESTE IF 
        //{

        //    rotateDirection = Vector2.zero;

        //}

        return rotateDirection.normalized;

    }

    #endregion


    public void TriggerEnter()
    {
        Debug.Log(gameObject.name + " entro en un trigger");
    }

    public void TriggerExit()
    {
        Debug.Log(gameObject.name + " salio de un trigger");

    }

    public void TriggerStay()
    {
        Debug.Log(gameObject.name + " esta en un trigger");
    }


}

public enum InputSystem
{
    OldInputSystem, NewInputSystem
}