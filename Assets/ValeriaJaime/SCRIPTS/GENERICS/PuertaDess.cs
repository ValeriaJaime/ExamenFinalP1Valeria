using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaDess : MonoBehaviour
{
    [SerializeField] private Item llave;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && llave)
        {
            Destroy(this.gameObject);
            Debug.Log("Waos");
        }


        //public void Interact(Inventory inventario)
        //{

        //    if (inventario.inventory.Contains(llave))
        //    {
        //        Debug.Log("Se abre la puerta");
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        Debug.Log("No tienes la llave");
        //    }
        //}

        //public void Interact()
        //{

        //}
    }
}

