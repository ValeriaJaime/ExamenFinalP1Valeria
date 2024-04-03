using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    /// <summary>
    /// TAREA
    /// 
    /// Que hagamos esta arma también
    /// </summary>
    public class HandGun : FireWeapon
    {
        protected TrailRenderer gunLaser;

        internal override void SingleShot() //disparo con raycast
        {
            base.SingleShot();
            Debug.Log("Disparo básico con " + name);
        }

        internal override void Reload()
        {
            base.Reload();
            Debug.Log("Recargando " + name);
        }

        internal override void Aim() //la mira es la parte visual del raycast
        {
            Debug.Log("Apuntando con " + name);
        }
    }

}
