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


    private void Update()
    {
        //var mouse = Mouse.current;
        if (Input.GetMouseButton(2))
        {
            Debug.Log("Clicked middle");
            //float rotz = AngleRot * _rotSpeed * Mathf.Deg2Rad;
            transform.RotateAround(_rotUsedtarget.transform.position, Vector3.forward, _rotSpeed * Time.deltaTime);
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
    }
}
