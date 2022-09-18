using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    [SerializeField] private Grass grassPrefab;
    [SerializeField] private LayerMask layerMask;




    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }


        RaycastHit _hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, layerMask))
        {

            var place = new Vector3(_hit.point.x, 0.5f, _hit.point.z);
            Grass grass = Instantiate(grassPrefab, place, Quaternion.identity);


        }

    }


}
