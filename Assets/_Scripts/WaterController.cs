using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    [SerializeField] private Grass grassPrefab;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] Transform grassPrant;



    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }


        RaycastHit _hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit,float.MaxValue, layerMask))
        {

            Grass grass = Instantiate(grassPrefab, _hit.point, Quaternion.identity, grassPrant);
            


        }

    }


}
