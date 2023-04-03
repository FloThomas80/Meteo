using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System.Threading.Tasks;

public class GetJSON : MonoBehaviour
{

    [ContextMenu("Test Get")]
    void Start()
    {
        //StartCoroutine(GetRequest("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void TestGet()
    {
        string url1 = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m";
        using var www = UnityWebRequest.Get(url1);

        www.SetRequestHeader("content-type","application/JSON");
        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (www.result == UnityWebRequest.Result.Success)
            Debug.Log("Succes : " + www.downloadHandler.text);
        else
            Debug.Log("Failure");
    }
    IEnumerator GetRequest (string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            Debug.Log(webRequest.downloadHandler.text);
            var N = JSON.Parse(webRequest.downloadHandler.text);
            string weather  = N["current_weather"][0]["temperature"].Value;
            print(weather);
        }
    }
}
