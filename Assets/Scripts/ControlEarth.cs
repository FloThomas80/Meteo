using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;


public class ControlEarth : MonoBehaviour
{

    private float _rotSpeed = 40f;
    [SerializeField]
    private GameObject _rotUsedtarget;
    [SerializeField]
    private Camera MainCamera;
    [SerializeField]
    private GameObject Clouds;



    private void OnEnable()
    {
        GetLatLong.OnClickRealise += FadeIn;
    }
    private void OnDisable()
    {
        GetLatLong.OnClickRealise -= FadeIn;
    }

    private void Update()
    {

        Clouds.transform.Rotate(0, 1 * Time.deltaTime, 0 ); // make clouds rotate slightly
        //var mouse = Mouse.current;
        if (Input.GetMouseButton(2))
        {
            //Debug.Log("Clicked middle");
            //float rotz = AngleRot * _rotSpeed * Mathf.Deg2Rad;
            transform.RotateAround(_rotUsedtarget.transform.position, Vector3.forward, _rotSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButton(1))
        {
            //Debug.Log("Clicked rigth");
            //float rotz = AngleRot * _rotSpeed * Mathf.Deg2Rad;
            transform.RotateAround(_rotUsedtarget.transform.position, -Vector3.forward, _rotSpeed * Time.deltaTime);
        }
        if (IsinBrakets())
        { 
                if (Input.GetAxis("Mouse ScrollWheel") > 0 )
            {
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z-0.3f);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z + 0.3f);
            }

        }
    }


    private bool IsinBrakets()
    {
        if (-50 > MainCamera.transform.position.z)
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, -50);
        else if (MainCamera.transform.position.z > -25)
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, -25);
        return true;
    }


    void OnMouseDrag()
    {
        float rotx = Input.GetAxis("Mouse X") * _rotSpeed * Mathf.Deg2Rad;
        float roty = Input.GetAxis("Mouse Y") * _rotSpeed * Mathf.Deg2Rad;
        

        transform.RotateAround(_rotUsedtarget.transform.position, Vector3.up, -rotx);
        transform.RotateAround(_rotUsedtarget.transform.position,Vector3.right, roty);

        StartCoroutine (FadeOut3D(Clouds, 0.1f, 10));

    }


    public IEnumerator FadeOut3D(GameObject t, float targetAlpha, float duration)
    {
        float diffAlpha;
        Renderer sr = t.GetComponent<Renderer>();

        if (targetAlpha < sr.material.color.a)
            diffAlpha = (targetAlpha - sr.material.color.a);
        else
        { 
            diffAlpha = (sr.material.color.a - targetAlpha);
        Debug.Log(" duration " + duration + " diff alpha " + diffAlpha);
        }
        float counter = 0;

            while (counter <= duration)
            {
                float alphaAmount = sr.material.color.a + (Time.deltaTime * diffAlpha) / duration;

                sr.material.color = new Color(sr.material.color.r, sr.material.color.g, sr.material.color.b, alphaAmount);

                counter++;
                yield return null;
            }
        //sr.material.color = new Color(sr.material.color.r, sr.material.color.g, sr.material.color.b, targetAlpha);
    }

    private void FadeIn()
    {
        Debug.Log("recu l'event");
        StartCoroutine(FadeOut3D(Clouds, 1f, 10));
    }
}
