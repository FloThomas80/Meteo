using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;


public class ControlEarth : MonoBehaviour
{

    private float _rotSpeed = 20f;
    [SerializeField]
    private GameObject _rotUsedtarget;


    private void Update()
    {
        //var mouse = Mouse.current;
        if (Input.GetMouseButtonDown(2))
        {
           //float rotz = Input.GetAxis("Mouse Y") * _rotSpeed * Mathf.Deg2Rad;
            transform.RotateAround(_rotUsedtarget.transform.position, Vector3.forward, 100*Time.deltaTime);
        }
    }
    void OnMouseDrag()
    {
        float rotx = Input.GetAxis("Mouse X") * _rotSpeed * Mathf.Deg2Rad;
        float roty = Input.GetAxis("Mouse Y") * _rotSpeed * Mathf.Deg2Rad;
        

        transform.RotateAround(_rotUsedtarget.transform.position, Vector3.up, -rotx);
        transform.RotateAround(_rotUsedtarget.transform.position,Vector3.right, roty);
    }
}
