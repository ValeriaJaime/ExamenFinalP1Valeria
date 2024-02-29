using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Eb este script, usaremos las configuraciones de inputs 
///para realizar acciones
/// </summary>
public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputConfig actualConfig;
    private static InputConfig _actualConfig;

    private void OnValidate()
    {
        _actualConfig = actualConfig;
    }

    public static bool MoveForwardInput()
    {
        return Input.GetKey(_actualConfig.walkForward);
    }

    public static bool MoveBackwardInput()
    {
        return Input.GetKey(_actualConfig.walkBackward);
    }

    public static bool RotateLeftInput()
    {
        return Input.GetKey(_actualConfig.rotateLeft);
    }

    public static bool RotateRightInput()
    {
        return Input.GetKey(_actualConfig.rotateRight);
    }

    public static bool JumpInput()
    {
        return Input.GetKeyDown(_actualConfig.jumpKey);
    }
    public static bool RunInput()
    {
        return Input.GetKey(_actualConfig.runKey);
    }

    public static bool ShootInput()
    {
        return Input.GetKeyDown(_actualConfig.shoot);
    }

    public static bool ReloadInput()
    {
        return Input.GetKeyDown(_actualConfig.reloadKey);
    }
}
