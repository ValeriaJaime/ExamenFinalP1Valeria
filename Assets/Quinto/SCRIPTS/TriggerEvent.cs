using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    [SerializeField] UnityEvent onTriggerStay;

    [SerializeField] string[] tags = new string[5];
    [SerializeField] List<string> tagList;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tagList.Contains(other.tag))
        {
            onTriggerEnter.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider other) 
    {
        if (tagList.Contains(other.tag))
        {
            onTriggerExit.Invoke();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (tagList.Contains(other.tag))
        {
            onTriggerStay.Invoke();
        }

    }

}
