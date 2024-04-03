using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script servirá para manipular la configuración de los inputs
/// 
/// Por ejemplo:
/// 
/// Disparar: Click derecho 
/// Saltar: Espacio 
/// 
/// </summary>
[CreateAssetMenu(fileName = "New Input Config", menuName = "Input", order = 0)]
public class InputConfig : ScriptableObject
{
    //KeyCode sirve para escoger teclas, de que E, etc
    public KeyCode walkForward = KeyCode.W;
    public KeyCode walkBackward = KeyCode.S;

    public KeyCode rotateLeft = KeyCode.A;
    public KeyCode rotateRight = KeyCode.D;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;

    public KeyCode shoot = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

    public KeyCode AimKey = KeyCode.Mouse1;
}

