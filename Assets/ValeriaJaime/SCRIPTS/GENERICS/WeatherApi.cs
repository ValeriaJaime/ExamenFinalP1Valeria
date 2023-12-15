using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.Rendering.PostProcessing;

public class WeatherApi : MonoBehaviour
{
    [SerializeField] private WeatherData data;
    [SerializeField] private static float latitude = -34.61315f /*20.11697f*/;
    [SerializeField] private static float longitud = -58.37723f /*-98.73329f*/;
    [SerializeField] private static string units = "metric";
    [SerializeField] private static readonly string apiKey = "8c654fbc09fac1f9208b0c739434de3a";
    [SerializeField] public float speed = 1;

    private string getWeatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitud}&appid={apiKey}&units={units}";

    public string json;

    [SerializeField] private Light directionalLight;
    [SerializeField] private Color colorToChange;
    [SerializeField] private float colorChangeSpeed = 1;
    [SerializeField] private float bloomIntensity;
    [SerializeField] private float chromIntensity;
    [SerializeField] private PostProcessVolume postProcessing;
    private Bloom bloom;
    private ChromaticAberration chrom;

    private void Start()
    {
        postProcessing.profile.TryGetSettings(out bloom);
        postProcessing.profile.TryGetSettings(out chrom);

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
                GetColorAndBloomAndChrom();
                StartCoroutine(ChangeLightColor());
                StartCoroutine(ChangeBloomIntensity());
                StartCoroutine(ChangeChromIntensity());
            }


            yield return new WaitForSecondsRealtime(600f);
        }
        
    }

    private IEnumerator ChangeLightColor()
    {
        //que lo metió en una corrutina en lugar de dejarlo en void, para que pueda usar el Time.deltaTime sin meterlo en el update

        yield return new WaitUntil(() => ActualLight() == colorToChange);    //Espera hasta que... el color sea igual al del colorToChange
    }

    private IEnumerator ChangeBloomIntensity()
    {
        yield return new WaitUntil(() => ActualBloom().intensity.value == bloomIntensity);
    }

    private IEnumerator ChangeChromIntensity()
    {
        yield return new WaitUntil(() => ActualChrom().intensity.value == chromIntensity);
    }

    private Color ActualLight()     //va cambiando del color en el que está hacia el color que está actualmente
    {
        directionalLight.color = Color.Lerp(directionalLight.color, colorToChange, colorChangeSpeed * Time.deltaTime);
        return directionalLight.color;
    }

    private Bloom ActualBloom()
    {
        bloom.intensity.value = bloomIntensity;
        return bloom;
    }

    private ChromaticAberration ActualChrom()
    {
        chrom.intensity.value = chromIntensity;
        return chrom;
    }

    private void GetColorAndBloomAndChrom()
    {
        switch(data.actualTemp)
        {
            case var temp when data.actualTemp <= 0:
                {
                    colorToChange = Color.blue;
                    bloomIntensity = 0f;
                    chromIntensity = 0f;
                    break;
                }

            case var temp when data.actualTemp > 0 && data.actualTemp <= 15:
                {
                    colorToChange = Color.cyan;
                    bloomIntensity = 5f;
                    chromIntensity = 0.33f;
                    break;
                }

            case var temp when data.actualTemp > 15 && data.actualTemp <= 30:
                {
                    colorToChange = Color.magenta;
                    bloomIntensity = 10f;
                    chromIntensity = 0.66f;
                    break;
                }

            case var temp when data.actualTemp > 30:
                {
                    colorToChange = Color.red;
                    bloomIntensity = 15f;
                    chromIntensity = 1f;
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
