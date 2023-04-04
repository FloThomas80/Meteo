using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using SimpleJSON;
using System.Threading.Tasks;

public class GetJSON : MonoBehaviour
{

    [ContextMenu("Test Get")]
    void Start()
    {
        //StartCoroutine(GetRequest("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m")); // methode 1

        StartCoroutine("SendRequest");
    }

//    IEnumerator GetRequest (string url) // methode 1
//    {
//        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
//        {
//            yield return webRequest.SendWebRequest();

//            Debug.Log(webRequest.downloadHandler.text);
//            var N = JSON.Parse(webRequest.downloadHandler.text);
//            string weather  = N["current_weather"][0]["temperature"].Value;
//        }
//    }
//}

    IEnumerator SendRequest ()
    {
        string url1 = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m";
        Debug.Log("etape 1 ok");
        using var www = UnityWebRequest.Get(url1);

        www.SetRequestHeader("content-type", "application/JSON");
        var operation = www.SendWebRequest();
     
        while (!operation.isDone)
            yield return Task.Yield();
    


        if (www.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("until here i'm ok");
            var N = JSON.Parse(www.downloadHandler.text);
            string weather  = N["current_weather"][0]["temperature"].Value;

            Debug.Log("Temp is : " + weather);
            Debug.Log("Succes : " + www.downloadHandler.text);
        }
        else
            Debug.Log("Failure");
    }

//public class Button : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        EditorGUILayout.LabelField("Send request");
//    }

}