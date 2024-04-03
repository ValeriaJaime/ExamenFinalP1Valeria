using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  WEAPON
{
    public class ShotGun : FireWeapon
    {
        protected float spread;
        protected int birdShot;

        protected Sprite spreadRange;

        protected Transform[] birdShotOrigin;

        /// <summary>
        /// Aqui lo que tienen que hacer, es tener minimo 9
        /// raycast, estos saldran de el arreglo de BirdShotOrigin
        /// Al disparar deben de dispersarse una distancia aleatoria de 0 a spreadRange
        /// </summary>
        /// 
        internal override void SingleShot()
        {
            base.SingleShot();
            Debug.Log("Disparo básico con " + name);
        }

        internal override void Reload()
        {
            base.Reload();
            Debug.Log("Recargando " + name);
        }

        internal override void Aim()   //diferente imagen para todas las miras de todas las armas
        {
            Debug.Log("Apuntando con " + name);
        }
    }

}
