using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    /// <summary>
    /// TAREA
    /// 
    /// Hacer esta arma
    /// </summary>
    public class ExplosiveGun : FireWeapon
    {
        protected Sprite explosionArea; //Proyectar una imagen el el hit.point que muestre el radio de explosi�n o d�nde va a caer, que d�nde va a caer m�s que nada 
        protected GameObject proyectile; //es un disparo f�sico, el raycast puede ser el que diga d�nde va a caer, pero el disparo va en un proyectil

        internal override void SingleShot()
        {
            base.SingleShot();
            Debug.Log("Disparo b�sico con " + name);
        }

        internal override void Reload()
        {
            base.Reload();
            Debug.Log("Recargando " + name);
        }

        internal override void Aim()
        {
            Debug.Log("Apuntando con " + name);
        }
    }
}

  