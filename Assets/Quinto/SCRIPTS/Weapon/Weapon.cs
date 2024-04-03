using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public abstract class Weapon : MonoBehaviour
    {
        protected int damage;

        internal abstract void Aim();
        internal virtual void SingleShot()
        {
            Debug.Log("Single Shot");
        }

        internal virtual void AutomaticShot()
        {
            Debug.Log("Automatic Shot");
        }

        internal virtual void MeleeAttack()
        {
            Debug.Log("MeleeAttack");
        }

        internal virtual void ChargedMeleeAttack()
        {
            Debug.Log("Charged Melee Attack");
        }
    }
}

