using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public class PesoPluma : MeleeWeapon
    {
        internal override void MeleeAttack()
        {
            base.MeleeAttack();
            Debug.Log("Ataque básico con " + name);
        }
    }
}

