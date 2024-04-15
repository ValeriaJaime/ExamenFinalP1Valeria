using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace  WEAPON
{
    public class ShotGun : FireWeapon
    {
        [Header("General")]
        protected TrailRenderer gunLaser;
        [SerializeField] private Transform raycastOrigin;
        private RaycastHit hit;
        [SerializeField] private GameObject bulletPrefabSprite;
        [SerializeField] private LayerMask hitMask;

        [Header("Shoot parameters")]
        [SerializeField] internal float spread;    //en sí cuánto se desvía el disparo

        [SerializeField] internal Sprite spreadRange; //la imagen que te dice de dónde a dónde puedes disparar, supongo haha

        [SerializeField] internal Transform[] birdShotOrigin;  //los 9 puntos de los que salen las balas

        [SerializeField] private float rayDistance = 100; //fire range, hasta dónde llega
        [SerializeField] private float rayForce = 500;
        //[SerializeField] private int damage = 1;
        [SerializeField] private int birdshot = 20; //la cantidad de balas que tiene actualmente
        //[SerializeField] private float fireRate = 0.02f; //es la velocidad del disparo 
        //[SerializeField] private int maxAmmo = 20; //la cantidad máxima de balas que puede tener el arma

        [Header("Reload parameters")]
        //[SerializeField] private int magazineAmmo = 15;
        //[SerializeField] private float reloadTime = 1.5f; //los segundos que se tarda en recargar el arma

        private float lastTimeShoot = Mathf.NegativeInfinity;

        /// <summary>
        /// Aqui lo que tienen que hacer, es tener minimo 9
        /// raycast, estos saldran de el arreglo de BirdShotOrigin
        /// Al disparar deben de dispersarse una distancia aleatoria de 0 a spreadRange
        /// </summary>
        /// 

        private void Awake()
        {
            damage = 1;
            fireRate = 0.02f;
            maxAmmo = 20;

            magazineAmmo = 20;
            reloadTime = 1.5f;

            birdshot = maxAmmo;
        }

        internal override void SingleShot()
        {
            if (lastTimeShoot + fireRate < Time.time) //este te dice si puedes disparar porque ya pasó el tiempo del last time shot
            {
                if (birdshot >= 1)                    //este te dice si tienes balas
                {
                    Debug.Log("Disparo básico con " + name);

                    raycastOrigin = RandomShootingPoint();
                    Debug.Log("Punto de disparo es " + raycastOrigin.name);

                    //spread
                    float x = Random.Range(0, spread);
                    float y = Random.Range(0, spread);

                    //calculate direction with spread
                    Vector3 direction = raycastOrigin.transform.forward + new Vector3(x, y, 0);

                    Physics.Raycast(raycastOrigin.position, direction, out hit, rayDistance, hitMask);

                    birdshot--;

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

                        //if (hit.transform.CompareTag("Enemy"))
                        //{
                        //    Debug.Log("Golpeaste a un enemigo");
                        //    /*hit.transform.GetComponent<EnemyLife>().TakeDamage(rayDamage / (hit.distance));*/  //aquí estamos mandando al TakeDamage el damage, que es lo que está dentro de los paréntesis
                        //}

                        lastTimeShoot = Time.time;
                    }
                }
            }
        }

        private Transform RandomShootingPoint()
        {
            int randomPoint = Random.Range(0, birdShotOrigin.Length);
            Debug.Log("Se eligió el punto de disparo llamado " + birdShotOrigin[randomPoint].name);
            return birdShotOrigin[randomPoint];
        }

        internal override void Reload()
        {
            //comenzar animación de recarga
            Debug.Log("Recargando " + name);
            StartCoroutine(WaintingReloading());
            Debug.Log("Recargando " + name + " " + birdshot + " " + magazineAmmo);
            birdshot = birdshot + magazineAmmo;
            Debug.Log(name + " " + birdshot);

            if (birdshot > 8)
            {
                birdshot = maxAmmo;
            }
            Debug.Log(birdshot);
            //terminar animación de recarga
        }

        IEnumerator WaintingReloading()
        {
            yield return new WaitForSeconds(reloadTime);
            Debug.Log("Recargada " + name);
        }

        internal override void Aim()   //diferente imagen para todas las miras de todas las armas
        {
            Debug.Log("Apuntando con " + name);
        }
    }

}
