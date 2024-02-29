using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [Header("Variables")]

    [Tooltip("Aquí se van a poner los emptys con de spawnpoint")]
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject enemy;

    [SerializeField, Tooltip("Cantidad de enemigos en la ronda")] 
    private int amountofEnemies;

    [SerializeField, Tooltip("Cantidad de enemigos que tendremos a disposición")] 
    private int totalAmountofEnemies;

    [SerializeField] private float spawnRate;

    [SerializeField] private Queue<GameObject> enemyPool;

    [SerializeField] Transform poolParent;

    [SerializeField] bool haMuerto = true;


    private void Start()
    {
        //StartCoroutine(SpawnEnemiesInstantiate());
        PoolStart();
    }

    private void Update()
    {
    }

    private void PoolStart() //primero instasncia
    {
        enemyPool = new Queue<GameObject>();        //este lo pone así para asegurar que sí sea queue

        for (int i = 0; i < totalAmountofEnemies; i++)
        {
            GameObject enemy = Instantiate(this.enemy); //aquí lo instancea
            enemy.name = "Enemy" + i;
            enemy.transform.parent = poolParent;       
            enemyPool.Enqueue(enemy);                    //aquí lo mete en la lista
            enemy.SetActive(false); //los desactiva porque no los necesita ya mismo
        }

        StartCoroutine(SpawnEnemiesQueue());
    }

    private IEnumerator SpawnEnemiesQueue()
    {
        for (int i = 0; i < amountofEnemies; i++) 
        {
            StartCoroutine(CallEnemy(CalledEnemy())); //llama al enemigo y pasa lo que tenga que hacer el enemigo
            yield return new WaitForSeconds(spawnRate);//y eso cada vez que se cumple el spawnRate
        }
    }

    private IEnumerator CallEnemy(GameObject enemy) //justo aquí se llama un enemigo
    {
        yield return new WaitUntil(() => enemy.GetComponent<EnemyLife>().haMuerto || enemy.GetComponent<EnemyAI>().playerSalioDeRango);
        //yield return new WaitUntil(() => EnemigoMuerto(haMuerto) == true);
        /*        yield return new WaitForSeconds(5);*/         //aquí se espera 5 segundos
        Debug.Log("Va a murió el mono " + enemy.name);
        enemyPool.Enqueue(enemy) ;                  // y lo regresa a la fila y lo desactiva
        enemy.GetComponent<EnemyLife>().haMuerto = false;
        enemy.GetComponent<EnemyAI>().playerSalioDeRango = false;
        enemy.SetActive(false);
        Debug.Log("Esto nos dice si sí se pudo apagar el mono");
        yield return new WaitForSeconds(1);

        //haMuerto = false;
    }

    private GameObject CalledEnemy()
    {
        GameObject enemyToSpawn = enemyPool.Dequeue(); //se agarra el enemigo del enemy pool y se saca de la fila
        enemyToSpawn.SetActive(true);                   //Lo activa para usarlo
        enemyToSpawn.transform.position = RandomSpawn().position;     //agarra la información de un spawnPoint
        return enemyToSpawn;                            //lo regresa
    }

    //internal bool EnemigoMuerto(bool muerto)
    //{   
    //    haMuerto = muerto;
    //    return haMuerto;
    //}

    //private IEnumerator SpawnEnemiesInstantiate()
    //{
    //    for (int i = 0; i < amountofEnemies; i++)
    //    {
    //        Transform spawnPos = RandomSpawn();
    //        Instantiate(enemy, spawnPos.position, spawnPos.rotation);

    //        yield return new WaitForSeconds(spawnRate);
    //    }
    //}

    private Transform RandomSpawn()
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomSpawn];
    }

}
