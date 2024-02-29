using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float movX;
    private float movY;

    private Transform personaje;

    public float mouseSenseX;
    public float mouseSenseY;

    private float rotY = 0f;

    public bool invertYAxis;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        personaje = transform.parent; //aqui lo que hace es guardar en personaje el componente de transform del padre 
    }

    void Update()
    {
        movX = Input.GetAxis("Mouse X") * mouseSenseX * Time.deltaTime;
        movY = Input.GetAxis("Mouse Y") * mouseSenseY * Time.deltaTime;//para el movimiento del y primero agarramos la info del mouse en y

        if (invertYAxis == true)
        {
            rotY += movY;
        }

        else
        {
            rotY -= movY;
            //este esta asi porque agarra los datos del movY, pero en negativo, se lo resta al rotY el movY, porque venia en negativo
            //sin esta linea no se mueve porque no se resta ni suma
            //        rotY += movY;  a fuerzas debe ir la linea para agarrar la info de movY, pero si lo sumamos se pone invertido
        }

        rotY = Mathf.Clamp(rotY, -90f, 90f);
        //despues pasamos un mathf.clamp, lo que hace este es dar un valor maximo y minimo de coeficiente, entonces el -90 y el 90 son el limite

        personaje.Rotate(0, movX, 0);
        transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        //aqui no llamamos al padre porque se mueve la camara. El quaternion se pone porque los angulos en unity son quaterniones. Local rotation recibe quaternion
        //por eso se manda el angulo en euler como los angulos que nosotros conocemos, y lo manda a unity en quaterniones 
        //le tenemos que regresar un quaternion. Y le decimos que le vamos a mandar eulers y que los transforme a quaternion
    }

}
