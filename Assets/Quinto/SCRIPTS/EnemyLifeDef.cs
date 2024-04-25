using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEAPON
{
    public class EnemyLifeDef : MonoBehaviour
    {
        [SerializeField] private float vida;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject arma;
        [SerializeField] private float receivedDamage = 1;
        [SerializeField] private float damageFisico;
        [SerializeField] private GameObject spawner;
        [SerializeField] internal bool haMuerto = false;
        //private Renderer rend;
        //public Material miMaterial;
        //public Material danioMaterial;
        //public bool lastimao = false;


        private void Start()
        {
            spawner = GameObject.Find("Spawner");
        }

        private void OnTriggerEnter(Collider bala)
        {

            if (bala.CompareTag("Bala"))
            {
                Debug.Log("Lo golpearon");
                damageFisico = 1;
                Debug.Log("Received damage = " + damageFisico);

                Debug.Log("El danio es igual a " + damageFisico);
                TakeDamageFisico();
            }

            else
            {
                Debug.Log("Nada");
            }
        }
        internal void TakeDamage(int receivedDamage)
        {
            //AudioManager.Instance.PlayMusic("Hit");
            vida = vida - receivedDamage;
            Debug.Log(vida + " - " + receivedDamage);
            //rend.material = danioMaterial;
            //lastimao = true;
            //StartCoroutine(daniTime());

            if (vida <= 0)
            {
                //Destroy(this.gameObject);
                vida = 0;
                Debug.Log("Te va a decir que murió");
                haMuerto = true;
                //spawner.GetComponent<Spawner>().EnemigoMuerto(true);
                Debug.Log("Ya murió");
            }

            Debug.Log("No se destruyó haha");
        }

        private void TakeDamageFisico()
        {
            //AudioManager.Instance.PlayMusic("Hit");
            vida -= damageFisico;
            Debug.Log(vida + " - " + damageFisico);
            //rend.material = danioMaterial;
            //lastimao = true;
            //StartCoroutine(daniTime());

            if (vida <= 0)
            {
                //Destroy(this.gameObject);
                vida = 0;
                Debug.Log("Te va a decir que murió");
                haMuerto = true;
                //spawner.GetComponent<Spawner>().EnemigoMuerto(true);
                Debug.Log("Ya murió");
            }

            Debug.Log("No se destruyó haha");
        }

    }

}