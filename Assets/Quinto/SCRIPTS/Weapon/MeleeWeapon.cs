using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public class MeleeWeapon : Weapon
    {
        protected float range;
        protected float weight;

        protected Sprite rangeGuide;

        internal override void Aim()
        {
            Debug.Log("Apuntando con " + name);
        }
    }
}

