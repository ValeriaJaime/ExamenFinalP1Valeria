using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValeriaJaime
{
    // Todas las clases que hereden de esta interfaz, deberán de implementar
    // los metodos que contenga la misma
    public interface IInteractable
    {
        // Este metodo es el que uso cuando quiero que la interaccion requiera
        // de un objeto
        public virtual void Interact(Inventory inventory)
        {

        }

        // Este metodo es el que uso, cuano quiero hacer la interaccion, sin restricciones
        public virtual void Interact()
        {

        }

    }
}