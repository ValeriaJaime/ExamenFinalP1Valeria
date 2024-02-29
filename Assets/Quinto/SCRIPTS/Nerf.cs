using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Nerf : MonoBehaviour
{
    private PlayerInput input;

    private Action Shoot;

    //[SerializeField, Tooltip("Aquí se van a poner los emptys de las Balas")]
    //private GameObject bulletPrefab;

    [SerializeField, Tooltip("Aquí va a aparecer la bala")]
    private Transform gunPosition;

    [SerializeField] private GameObject bullet;

    [SerializeField, Tooltip("Te dice si puede disparar")]
    bool puedeDisparar = true;

    [SerializeField, Tooltip("Te dice si quiere disparar")]
    bool wantToShoot = true;

    [SerializeField] private float fuerzaBala; //la fuerza con la que el addforce va a lanzar 

    [SerializeField] private Transform particles;

    [SerializeField, Tooltip("Cantidad de carga máxima del arma")]
    private int amountofBullets;

    [SerializeField, Tooltip("Cantidad de enemigos que balas totales en la pool")]
    private int totalAmountofBullets;

    [SerializeField, Tooltip("Tiempo entre cada disparo")]
    private float cadenciaDeTiro;

    [SerializeField, Tooltip("")]
    private Queue<GameObject> bulletPool;

    [SerializeField, Tooltip("El empty en el que se va a guardar la queue")]
    Transform poolBulletParent;

    [SerializeField, Tooltip("Daño del jugador")]
    internal float weaponDamage = 1;

    //public Transform cameraOrigin; //en caso de que las balas salgan de la cámara, pero como este es tanque, que salga de la pistola 
    //public float rayDistance;
    //private RaycastHit hit;   //guarda la información de donde pegó el rayito
    //public LayerMask hitMask;
    //public float rayForce;
    //public int rayDamage;

    private void OnValidate()
    {
        Shoot = Shooting;
    }

    void Start()
    {
        PoolBulletStart();
        input = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (input.actions["Shoot"].WasPressedThisFrame() /*&& puedeDisparar == false && CanShoot() */)
        {
            Debug.Log("Disparando");
            wantToShoot = true;
            StartCoroutine(SpawnBulletsQueue());
            Debug.Log("Ya no dispara");
        }
    }

    #region "BulletPool"
    private void PoolBulletStart() //primero instasncia y se crea el queue
    {
        bulletPool = new Queue<GameObject>();        //este lo pone así para asegurar que sí sea queue

        for (int i = 0; i < totalAmountofBullets; i++) //este lo que hace es crear la lista con objetos desactivados
        {
            GameObject bullet = Instantiate(this.bullet); //aquí lo instancea
            bullet.name = "Bullet" + i;
            bullet.transform.parent = poolBulletParent;
            bulletPool.Enqueue(bullet);                    //aquí lo mete en la lista
            bullet.SetActive(false); //los desactiva porque no los necesita ya mismo
        }
    } 

    //yo creo que este no es necesario en sí, no de esta manera, no necesitamos que vaya apareciendo uno por uno, más bien vamos a necesitar
    //que este  se llame cada vez que se dispare una bala
    private IEnumerator SpawnBulletsQueue() //este lo que hace es que permite usar los objetos uno por uno de los que se van a necesitar
    {
        for (int i = 0; i < amountofBullets; i++)
        {
            EsperandoADisparar();
            yield return new WaitUntil(() => wantToShoot == true/* && CanShoot() == true*/);
            yield return new WaitUntil(() => puedeDisparar == true); 
            StartCoroutine(EnableBullet(EnabledBullet())); //entra la corrutina que desactiva el objeto, siendo aparecido el objeto en primer lugar
            //while(!wantToShoot && input.actions["Shoot"].WasPressedThisFrame() == false) 
            //{
            //    Debug.Log("Aún no puedes disparar");
            //}
            wantToShoot = false;
            puedeDisparar = false;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator EnableBullet(GameObject bulletToUse) //justo aquí se llama un enemigo o bala, aquí tendrías que meter el método de lo que haga
    {
        yield return new WaitForSeconds(3);
        bulletPool.Enqueue(bulletToUse);                  // y lo regresa a la fila y lo desactiva
        bulletToUse.SetActive(false);
        puedeDisparar = false;
        wantToShoot = false;
        Debug.Log("Se ha apagado");
    }

    private GameObject EnabledBullet()                  //este sólo te regreaa el game object sacado de la fila listo para usarse
    {
        GameObject bulletToShoot = bulletPool.Dequeue(); //se agarra el enemigo del enemy pool y se saca de la fila
        bulletToShoot.SetActive(true);                   //Lo activa para usarlo
        bulletToShoot.transform.position = gunPosition.position; //le da la posición sacadad del arma
        ShootBullet(bulletToShoot);
        return bulletToShoot;
    }

    #endregion

    private void ShootBullet(GameObject actualBullet)
    {
        actualBullet.GetComponent<Rigidbody>().AddForce(transform.forward * fuerzaBala);
    } 

    //private bool CanShoot()
    //{
    //    Debug.Log("Puede disparar es false");
    //    EsperandoADisparar();
    //    Debug.Log("Puede disparar es true");
    //    return puedeDisparar = true;
    //}

    private IEnumerator EsperandoADisparar()
    {
        yield return new WaitForSeconds(cadenciaDeTiro);
        puedeDisparar = true;
    }

    //private void OnTriggerEnter(Collider enemy)
    //{
    //    if (enemy.CompareTag("Enemy"))
    //    {
    //        enemy.GetComponent<EnemyLife>().TakeDamage(playerDamage);  //aqu? estamos mandando al TakeDamage el damage, que es lo que est? dentro de los par?ntesis
    //    }
    //}
}
