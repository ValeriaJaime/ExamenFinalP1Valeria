using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FueraDeRango : MonoBehaviour
{
    [SerializeField] internal bool fueraDeRango = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            fueraDeRango = true;
        }
    }

}
