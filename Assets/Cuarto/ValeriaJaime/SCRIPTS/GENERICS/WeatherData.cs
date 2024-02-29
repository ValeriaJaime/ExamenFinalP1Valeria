using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct WeatherData 
{
    [SerializeField] public string country;
    [SerializeField] public string city;
    [SerializeField] public float actualTemp;
    [SerializeField] public string description;
    [SerializeField] public float windSpeed;
}
