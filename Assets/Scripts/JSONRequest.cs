using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Globalization;
using TMPro;
using System;

public class JSONRequest : MonoBehaviour
{
    //public int Temperature;
    //[ContextMenu("Test Get")]

    [SerializeField] private TextMeshProUGUI _weatherInfoText;
    [SerializeField] private TextMeshProUGUI _weatherCodeTxt;
    [SerializeField] private GameObject Picto_WheatherCode;
    [SerializeField] private Sprite[] Spr_WheatherCode;

    [Serializable]
    public class Rootobject
    {
        public float latitude;
        public float longitude;
        public float generationtime_ms;
        public int utc_offset_seconds;
        public string timezone;
        public string timezone_abbreviation;
        public float elevation;
        public Current_Weather current_weather;
        public Hourly_Units hourly_units;
        public Hourly hourly;
    }
    [Serializable]
    public class Current_Weather
    {
        public float temperature;
        public float windspeed;
        public float winddirection;
        public int weathercode;
        public string time;
    }
    [Serializable]
    public class Hourly_Units
    {
        public string time;
        public string temperature_2m;
        public string weathercode;
    }
    [Serializable]
    public class Hourly
    {
        public string[] time;
        public float[] temperature_2m;
        public int[] weathercode;
    }

    private void Start()
    {
        //Spr_WheatherCode = Resources.LoadAll<Sprite>(string.Format("Assets/Textures/pictos_meteo.jpg"));
    }


    public void GetWeatherData(float latitude, float longitude)
    {
        string latString = latitude.ToString(CultureInfo.InvariantCulture); // on recupere les lat long on transforme en string et on concatene en une adresse
        string lonString = longitude.ToString(CultureInfo.InvariantCulture);//longitude.ToString(CultureInfo.InvariantCulture) = "transforme la virgule en point"
        string requestUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latString}&longitude={lonString}&hourly=temperature_2m,weathercode&current_weather=true";
        //Debug.Log(requestUrl);

        StartCoroutine(FetchWeatherData(requestUrl)); //on demare la coroutine pour avoir les données meteo
        //Debug.Log("I get some Weather data !!");
    }


    private IEnumerator FetchWeatherData(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) //recupere l'url  pourquoi le USING
        {
            yield return webRequest.SendWebRequest();// je suppose qu'on envoie une requete a l'url  recu

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) // si erreur alors
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {

                //Debug.Log("Weather data: " + webRequest.downloadHandler.text);

                string weatherData = webRequest.downloadHandler.text;

                Rootobject parsedData = JsonUtility.FromJson<Rootobject>(weatherData);

                _weatherInfoText.text = "Temperature : " + parsedData.current_weather.temperature +"°C";
                int localWeatherCode = (parsedData.current_weather.weathercode);
                Debug.Log(localWeatherCode);
                switch (localWeatherCode)
                {
                    case 0 :
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[0];
                        _weatherCodeTxt.text = "Clear \nsky";
                        break;
                    case 1 or 2 or 3:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[1];
                        _weatherCodeTxt.text = "Mainly \nclear";
                        break;
                    case 45 or 48:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[2];
                        _weatherCodeTxt.text = "Fog";
                        break;
                    case 51 or 53 or 55:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[3];
                        _weatherCodeTxt.text = "Drizzle";
                        break;
                        case 56 or 57:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[4];
                        _weatherCodeTxt.text = "Freezing \nDrizzle";
                        break;
                    case 61 or 63 or 65:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[5];
                        _weatherCodeTxt.text = "Rain";
                        break;
                    case 66 or 67:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[6];
                        _weatherCodeTxt.text = "Freezing \nRain";
                        break;
                    case 71 or 73 or 75:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[7];
                        _weatherCodeTxt.text = "Snow ";
                        break;
                    case 77:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[8];
                        _weatherCodeTxt.text = "Snow ";
                        break;
                    case 80 or 81 or 82:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[9];
                        _weatherCodeTxt.text = "Rain \nshowers";
                        break;
                    case 85 or 86:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[10];
                        _weatherCodeTxt.text = "Snow \nshowers";
                        break;
                    default:
                        Picto_WheatherCode.GetComponent<SpriteRenderer>().sprite = Spr_WheatherCode[11];
                        _weatherCodeTxt.text = "Storm";
                        break;
                }
            }
        }
    }


}









