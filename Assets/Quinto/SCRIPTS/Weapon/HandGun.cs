using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace WEAPON
{
    /// <summary>
    /// TAREA
    /// 
    /// Que hagamos esta arma también
    /// </summary>
    public class HandGun : FireWeapon
    {
        //de este falta implementae el trailrenderer y la mira. Del automatic es practicamente lo mismo que este
        //falta la mira, y tenemos que hacer que los valores sean visibles en el editor. Y con eso queda el automatic.
        //Para el shotgun hay que seguir el tutorial de spread. Y para el explosive hay que buscar un tutorial 
        [Header("General")]
        [SerializeField] protected TrailRenderer gunLaser;
        [SerializeField] private Transform raycastOrigin;
        private RaycastHit hit;
        [SerializeField] private GameObject bulletPrefabSprite;
        [SerializeField] private LayerMask hitMask;

        [Header("Shoot parameters")]
        [SerializeField] private float rayDistance = 100; //fire range, hasta dónde llega
        [SerializeField] private float rayForce = 500;
        //[SerializeField] private int damage = 1;
        //[SerializeField] private int actualAmmo = 5; //la cantidad de balas que tiene actualmente
        //[SerializeField] private float fireRate = 0.06f; //es la velocidad del disparo 
        //[SerializeField] private int maxAmmo = 8; //la cantidad máxima de balas que puede tener el arma

        [Header("Reload parameters")]
        //[SerializeField] private int magazineAmmo;
        //[SerializeField] private float reloadTime = 1.5f; //los segundos que se tarda en recargar el arma

        private float lastTimeShoot = Mathf.NegativeInfinity;

        private void Start()
        {
            damage = 1;
            actualAmmo = 8;
            fireRate = 0.5f;
            maxAmmo = 8;
            magazineAmmo = 8;
            reloadTime = 1.5f;

            actualAmmo = maxAmmo;
        }

        internal override void SingleShot() //disparo con raycast
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

            if (actualAmmo > maxAmmo)
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

        internal override void Aim() //la mira es la parte visual del raycast
        {
            Debug.Log("Apuntando con " + name);
        }
    }

}
