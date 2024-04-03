using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public class FireWeapon : Weapon
    {
        protected int actualAmmo;
        protected int maxAmmo;
        protected int magazineAmmo;

        protected float fireRate;

        protected float reloadTime;

        internal virtual void Reload()
        {

        }

        internal override void Aim()
        {

        }
    }
}

