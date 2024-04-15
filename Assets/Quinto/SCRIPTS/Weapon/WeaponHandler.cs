using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WEAPON
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons = new Weapon[2];  //la cantidad de armas que tienes
        [SerializeField] private Weapon currentWeapon;              //el arma que tienes en ese momento

        [SerializeField] private int currentWeaponIndex;

        private Action WeaponInputs;

        private Action? UseWeapon;
        private Action? UseAndHoldHeapon;
        private Action? WeaponAim;
        private Action? Reload;

        #region Core


        private void Start()
        {
            currentWeapon = weapons[0];
            currentWeaponIndex = 0;
            SwitchFunction();
        }



        private void Update()
        {
            WeaponInputs();
            SwitchWeapon();
        }
        #endregion

        private void SwitchWeapon()
        {
            if (InputHandler.Scroll() > 0)//si el scroll es mayor a cero, es 1, significa que fue hacia arriba
            {
                currentWeaponIndex++; //se suma uno al weapon index

                currentWeaponIndex = currentWeaponIndex >= weapons.Length ? 0 : currentWeaponIndex;

                currentWeapon = weapons[currentWeaponIndex]; //cambias al arma con ese index

                SwitchFunction(); //y pasa este, que cambia las acciones en sí
            }

            else if (InputHandler.Scroll() < 0) //fuiste hacia abajo
            {  
                currentWeaponIndex--;

                currentWeaponIndex = currentWeaponIndex < 0 ? weapons.Length-1 : currentWeaponIndex;

                currentWeapon = weapons[currentWeaponIndex];

                SwitchFunction();
            }

        }

        private void SwitchFunction()
        {

            switch (currentWeapon)                      //EN EL START SE PONE EL SWITCH PARA QUE SE ACTIVE LA PRIMERA ARMA
            {
                case HeavyWeapon:
                    {
                        UseWeapon = currentWeapon.MeleeAttack;
                        UseAndHoldHeapon = currentWeapon.ChargedMeleeAttack;
                        WeaponAim = currentWeapon.Aim;
                        Reload = null;
                        WeaponInputs = HeavyMeleeWeapon;  //aquí cambia los inputs en las acciones
                        break;
                    }

                case PesoPluma:
                    {
                        UseWeapon = currentWeapon.MeleeAttack;
                        UseAndHoldHeapon = currentWeapon.ChargedMeleeAttack;
                        WeaponAim = null;
                        Reload = null;
                        WeaponInputs = LightMeleeWeapon;
                        break;
                    }

                case HandGun handGun:
                    {
                        UseWeapon = handGun.SingleShot;
                        UseAndHoldHeapon = null;
                        WeaponAim = handGun.Aim;
                        Reload = handGun.Reload;
                        WeaponInputs = SemiAutomatic;
                        break;
                    }

                case ShotGun shotGun:
                    {
                        UseWeapon = shotGun.SingleShot;
                        UseAndHoldHeapon = null;
                        WeaponAim = shotGun.Aim;
                        Reload = shotGun.Reload;
                        WeaponInputs = SemiAutomatic;
                        break;
                    }

                case ExplosiveGun explosiveGun:
                    {
                        UseWeapon = explosiveGun.SingleShot;
                        UseAndHoldHeapon = null;
                        WeaponAim = explosiveGun.Aim;
                        Reload = explosiveGun.Reload;
                        WeaponInputs = SemiAutomatic;
                        break;
                    }


                case AutomaticRfile automaticRfile:  //dice que en este caso, no sólo vamos a agarrar el current weapon que es el que entra en el switch,
                                                     //más bien es que podemos en el mismo switch declarar una variable tipo AutomaticRfile para poder acceder a
                                                     //las variables propias del AutomaticRfile, ya que en los otros, estaba accediendo a las variables de Weapon porque currentWeapon es tipo Weapon
                    {
                        UseWeapon = null;
                        UseAndHoldHeapon = automaticRfile.AutomaticShot;
                        Reload = automaticRfile.Reload;
                        WeaponAim = automaticRfile.Aim;
                        WeaponInputs = AutomaticWeapon;
                        break;
                    }
            }
        }



        private void AutomaticWeapon()
        {
            if (InputHandler.AutomaticShotKey())
            {
                UseAndHoldHeapon();
            }

            if (InputHandler.AimInput())
            {
                WeaponAim();
            }

            if (InputHandler.Reload())
            {
                Reload();
            }
        }

        private void SemiAutomatic()
        {
            if (InputHandler.ShootKey()) //si no es nulo y tiene el input, sucede el UseWeapon()
            {
                UseWeapon();
            }

            if (InputHandler.AimInput())
            {
                WeaponAim();
            }

            if (InputHandler.Reload())
            {
                Reload();
            }
        }

        private void LightMeleeWeapon()
        {
            if (InputHandler.ShootKey()) //si no es nulo y tiene el input, sucede el UseWeapon()
            {
                UseWeapon();
            }

            if (InputHandler.AimInput())
            {
                WeaponAim();
            }
        }

        private void HeavyMeleeWeapon()
        {
            if (InputHandler.ShootKey()) //si no es nulo y tiene el input, sucede el UseWeapon()
            {
                UseWeapon();
            }

            if (InputHandler.ShootKey())
            {
                UseAndHoldHeapon();
            }

            if (InputHandler.AimInput())
            {
                WeaponAim();
            }
        }

    }
}
