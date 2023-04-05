using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLatLong : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private LayerMask _earthLayer;
    [SerializeField]
    private GameObject _flag;
    [SerializeField]
    private GameObject _ballTest;

    private float downClickTime;
    private float ClickDeltaTime = 0.2F;

    private Vector3 normalizedHitPoint;
    private Vector3 hitPoint;
    private Vector3 localHitPoint;
    private Vector2 LatLong;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, _earthLayer))
        {
            hitPoint = hit.point;
            localHitPoint = transform.InverseTransformPoint(hitPoint);

            normalizedHitPoint = localHitPoint.normalized;
        }
    }

    private Vector2 GetLatLongPosition()
    {
            float latitude = Mathf.Asin(normalizedHitPoint.y) * Mathf.Rad2Deg;
            float longitude = Mathf.Atan2(normalizedHitPoint.z, normalizedHitPoint.x) * Mathf.Rad2Deg;

            return new Vector2(latitude, longitude);
    }
    private void OnMouseDown()
    {
        downClickTime = Time.time; //petit calcul pour savoir si c'est un drag ou juste un click
    }
    private void OnMouseUp()
    {
        if (Time.time - downClickTime <= ClickDeltaTime) //si le delta entre le click and release est court alors c'est un click
        {
            if (_earthLayer == LayerMask.GetMask("Earth"))
            {
                Debug.Log("hitpoint = " + hitPoint + "local = " + localHitPoint + "normalize = " + normalizedHitPoint);


                Vector3 targetDirection = hitPoint - _flag.transform.position;//
                _flag.transform.up = targetDirection;// On deplace le pin sur la carte
                _ballTest.transform.position = hitPoint;//

                LatLong = GetLatLongPosition();

            }
        }
    }
}
