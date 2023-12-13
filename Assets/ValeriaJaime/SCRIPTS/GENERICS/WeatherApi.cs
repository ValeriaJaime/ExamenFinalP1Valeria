using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class WeatherApi : MonoBehaviour
{
    [SerializeField] private WeatherData data;
    [SerializeField] private static float latitude = 20.11697f;
    [SerializeField] private static float longitud = -98.73329f;
    [SerializeField] private static string units = "metric";
    [SerializeField] private static readonly string apiKey = "8c654fbc09fac1f9208b0c739434de3a";
    [SerializeField] public float speed = 1;

    private string getWeatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitud}&appid={apiKey}&units={units}";

    public string json;

    [SerializeField] private Light directionalLight;
    [SerializeField] private Color colorToChange;
    [SerializeField] private float colorChangeSpeed = 1;

    private void Start()
    {
        StartCoroutine(WeatherUpdate());
    }

    public bool scaled = true;


    private void Update()
    {

    }

    IEnumerator WeatherUpdate()
    {
        while (true)
        {
            UnityWebRequest request = new UnityWebRequest(getWeatherUrl);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }

            else
            {
                Debug.Log(request.downloadHandler.text);
                json = request.downloadHandler.text;
                DecodeJson();
                GetColor();
                StartCoroutine(ChangeLightColor());
            }


            yield return new WaitForSecondsRealtime(600f);
        }
        
    }

    private IEnumerator ChangeLightColor()
    {
        //que lo metió en una corrutina en lugar de dejarlo en void, para que pueda usar el Time.deltaTime sin meterlo en el update

        yield return new WaitUntil(() => ActualLight() == colorToChange);    //Espera hasta que... el color sea igual al del colorToChange
    }

    private Color ActualLight()     //va cambiando del color en el que está hacia el color que está actualmente
    {
        directionalLight.color = Color.Lerp(directionalLight.color, colorToChange, colorChangeSpeed * Time.deltaTime);
        return directionalLight.color;
    }

    private void GetColor()
    {
        switch(data.actualTemp)
        {
            case var temp when data.actualTemp <= 0:
                {
                    colorToChange = Color.blue;
                    break;
                }

            case var temp when data.actualTemp > 0 && data.actualTemp <= 15:
                {
                    colorToChange = Color.cyan;
                    break;
                }

            case var temp when data.actualTemp > 15 && data.actualTemp <= 30:
                {
                    colorToChange = Color.magenta; 
                    break;
                }

            case var temp when data.actualTemp > 30:
                {
                    colorToChange = Color.red;
                    break;
                }
        }
    }



    private void DecodeJson()
    {
        var weatherJson = JSON.Parse(json); //var implica que puede ser cualquier tipo de variable

        data.country = weatherJson["sys"]["country"].Value; 
        data.city = weatherJson["name"].Value;
        data.actualTemp = float.Parse(weatherJson["main"]["temp"].Value);
        data.description = weatherJson["weather"][0]["description"].Value;
        data.windSpeed = float.Parse(weatherJson["wind"]["speed"].Value);

    }
}
