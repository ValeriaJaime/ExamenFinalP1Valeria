using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WEAPON
{
    public class AutomaticRfile : FireWeapon
    {
        /// <summary>
        /// TAREA
        /// 
        /// Crear un script para que en el
        /// inspector dependiendo de si está puesto
        /// el modo Burst, se vea o no la variable bulletsPerBurst
        /// </summary>

        /// <summary>
        /// TAREA 2
        /// 
        /// Hacer esta arma en el automatic shot con raycast
        /// </summary>


        internal TrailRenderer gunLaser; //un laser que muestra a dónde apuntas
        [SerializeField] internal Transform laserOrigin;

        [SerializeField] internal float rayDistance;
        [SerializeField] internal RaycastHit hit;
        [SerializeField] internal LayerMask hitMask;

        [SerializeField] internal GameObject proyectile;

        [SerializeField] internal FireType fireType = FireType.Automatic; //que esto lo hacemos regresando

        [SerializeField] internal int bulletPerBurst;//no se va a cambiar el tipo de disparo entonces por ahora no 

        private void Start()
        {
            gunLaser = GetComponent<TrailRenderer>();
        }

        internal override void AutomaticShot()//disparo con raycast
        {
            base.AutomaticShot();
            Debug.Log("Disparo automático con " + name);
            Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, rayDistance, hitMask);
        }

        internal override void Reload()
        {
            base.Reload();
            Debug.Log("Recargando " + name);
        }

        internal override void Aim()
        {
            gunLaser.SetPosition(0, laserOrigin.forward); 
            Debug.Log("Apuntando con " + name);
        }
    }

    [SerializeField] internal enum FireType
    {
        Automatic, Burst
    }

}
