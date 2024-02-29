using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform jugador;
    private NavMeshAgent agent;

    [SerializeField] private bool detect;
    [SerializeField] private float areaDetection;
    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private Vector3 origen;

    [SerializeField] internal bool playerSalioDeRango = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origen = this.transform.position;   //toma la posición con la que el enemigo comienza en el juego
    }

    // Update is called once per frame
    void Update()
    {
        detect = Physics.CheckSphere(this.transform.position, areaDetection, whatIsPlayer); //este crea el area de detección

        if(GameObject.FindGameObjectWithTag("Player") != null && detect) //tiene seguir al player una vez que se cumpla lo de detect
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
            agent.SetDestination(jugador.position);
        }
        else
        {
            agent.SetDestination(origen);   //se regresa a su origen
            //new WaitUntil(() => this.transform.position == origen);
            //haRegresado = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            playerSalioDeRango = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, areaDetection);
    }
}
