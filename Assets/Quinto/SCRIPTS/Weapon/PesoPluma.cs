using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public class PesoPluma : MeleeWeapon
    {
        [SerializeField] private Animator animator;

        internal override void MeleeAttack()
        {
            animator.Play("Baseball Hit");
            base.MeleeAttack();
            Debug.Log("Ataque básico con " + name);
        }
    }
}

