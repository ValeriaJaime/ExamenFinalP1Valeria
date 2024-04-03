using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public class HeavyWeapon : MeleeWeapon
    {
        internal override void MeleeAttack()
        {
            base.MeleeAttack();
            Debug.Log("Ataque básico con " + name);
        }

        internal override void ChargedMeleeAttack()
        {
            base.ChargedMeleeAttack();
            Debug.Log("Ataque cargado con " + name);
        }
    }

}
