using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    /// <summary>
    /// TAREA
    /// 
    /// Hacer esta arma
    /// </summary>
    public class ExplosiveGun : FireWeapon
    {
        [Header("General")]
        [SerializeField] protected Sprite explosionArea; //Proyectar una imagen el el hit.point que muestre el radio de explosión o dónde va a caer, que dónde va a caer más que nada 
        [SerializeField] protected GameObject proyectile; //es un disparo físico, el raycast puede ser el que diga dónde va a caer, pero el disparo va en un proyectil
        [SerializeField] private Transform raycastOrigin;
        private RaycastHit hit;
        [SerializeField] private LayerMask hitMask;
        [SerializeField] private float fuerzaBala = 300 ;

        [Header("Shoot parameters")]
        [SerializeField] private float rayDistance = 100; //fire range, hasta dónde llega
        [SerializeField] private float rayForce = 500;

        private float lastTimeShoot = Mathf.NegativeInfinity;

        [SerializeField] private Animator animator;

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

                    Sprite explosionAreaPrefab = Instantiate(explosionArea, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                    Destroy(explosionAreaPrefab, 4f);

                    GameObject proyectileClone = Instantiate(proyectile, raycastOrigin.position, proyectile.transform.rotation);
                    proyectile.GetComponent<Rigidbody>().AddForce(transform.forward * fuerzaBala);
                    Destroy(proyectileClone, .75f);

                    actualAmmo--;

                    if (hit.transform != null)
                    {

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
/*                            hit.transform.GetComponent<EnemyLifeDef>().TakeDamage(damage)*/; //aquí estamos mandando al TakeDamage el damage, que es lo que está dentro de los paréntesis
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

        internal override void Aim()
        {
            Debug.Log("Apuntando con " + name);
            animator.Play("Rifle Aiming Idle");
        }
    }
}

  