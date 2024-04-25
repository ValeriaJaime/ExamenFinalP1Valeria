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


        [Header("Fire Type")]
        [SerializeField] internal FireType fireType = FireType.Automatic; //que esto lo hacemos regresando
        [SerializeField] internal int bulletPerBurst;//no se va a cambiar el tipo de disparo entonces por ahora no 

        [Header("General")]
        [SerializeField] internal TrailRenderer gunLaser; //un laser que muestra a dónde apuntas
        [SerializeField] internal Transform laserOrigin;

        [SerializeField] internal Transform raycastOrigin;
        private RaycastHit hit;
        [SerializeField] internal GameObject bulletPrefabSprite;
        [SerializeField] internal LayerMask hitMask;


        [Header("Shoot parameters")]
        [SerializeField] internal float rayDistance = 100; //fire range, hasta dónde llega
        [SerializeField] internal float rayForce = 500;
        //[SerializeField] internal int damage = 1;
        //[SerializeField] internal int actualAmmo = 5; //la cantidad de balas que tiene actualmente
        //[SerializeField] internal float fireRate = 0.06f; //es la velocidad del disparo 
        //[SerializeField] internal int maxAmmo = 8; //la cantidad máxima de balas que puede tener el arma

        [Header("Reload parameters")]
        //[SerializeField] internal int magazineAmmo;
        //[SerializeField] internal float reloadTime = 1.5f; //los segundos que se tarda en recargar el arma

        private float lastTimeShoot = Mathf.NegativeInfinity;

        [SerializeField] internal Animator animator;


        private void Awake()
        {
            damage = 1;
            actualAmmo = 25;
            fireRate = 0.1f;
            maxAmmo = 30;
            magazineAmmo = 30;
            reloadTime = 1.5f;

            actualAmmo = maxAmmo;

            //gunLaser = GetComponent<TrailRenderer>();
            animator = GetComponentInParent<Animator>();
        }

        internal override void AutomaticShot()//disparo con raycast
        {
            if (lastTimeShoot + fireRate < Time.time) //este te dice si puedes disparar porque ya pasó el tiempo del last time shot
            {
                if (actualAmmo >= 1)                    //este te dice si tienes balas
                {
                    Debug.Log("Disparo básico con " + name);
                    Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, rayDistance, hitMask);

                    TrailRenderer trail = Instantiate(gunLaser, raycastOrigin.position, Quaternion.identity);

                    StartCoroutine(SpawnTrail(trail, hit)); 

                    actualAmmo--;

                    if (hit.transform != null)
                    {
                        GameObject bulletholeClone = Instantiate(bulletPrefabSprite, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                        Destroy(bulletholeClone, 4f);

                        if (hit.transform)
                        {
                            Debug.Log("Disparaste a " + hit.transform.name);
                        }

                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * rayForce);
                        }

                        if (hit.transform.CompareTag("Enemy"))
                        {
                            Debug.Log("Golpeaste a un enemigo");
                            hit.transform.GetComponent<EnemyLifeDef>().TakeDamage(damage); //aquí estamos mandando al TakeDamage el damage, que es lo que está dentro de los paréntesis
                        }

                        else
                        {
                            Debug.Log("No golpeaste enemigos");
                        }

                        lastTimeShoot = Time.time;
                    }
                }
            }
        }

        private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
        {
            float time = 0;
            Vector3 startPosition = trail.transform.position;

            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
                time += Time.deltaTime / trail.time;

                yield return null;
            }

            trail.transform.position = hit.point;

            Destroy(trail.gameObject, trail.time);
        }

        internal override void Reload()
        {
            //comenzar animación de recarga
            Debug.Log("Recargando " + name);
            StartCoroutine(WaintingReloading());
            Debug.Log("Recargando " + name + " " + actualAmmo + " " + magazineAmmo);
            actualAmmo = actualAmmo + magazineAmmo;
            Debug.Log(name + " " + actualAmmo);

            if (actualAmmo > 8)
            {
                actualAmmo = maxAmmo;
            }
            Debug.Log(actualAmmo);
            //terminar animación de recarga
        }

        IEnumerator WaintingReloading()
        {
            yield return new WaitForSeconds(reloadTime);
            Debug.Log("Recargada " + name);
        }

        internal override void Aim()
        {
            Debug.Log("Apuntando con " + name);
            animator.Play("Rifle Aiming Idle");
        }
    }

    [SerializeField] internal enum FireType
    {
        Automatic, Burst
    }

}
