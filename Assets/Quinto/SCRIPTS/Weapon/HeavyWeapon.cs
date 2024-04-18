using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public class HeavyWeapon : MeleeWeapon
    {
        [SerializeField] private Animator animator;

        internal override void MeleeAttack()
        {
            animator.Play("Baseball Hit");
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
