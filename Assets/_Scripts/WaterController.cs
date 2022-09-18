using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGrass : MonoBehaviour
{
    [SerializeField] private Vector3 place;
    [SerializeField] private GameObject grass;

    private RaycastHit _hit;
    public bool addNow;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& addNow== true)
        {
            if(Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition),out _hit))
            {
                if (_hit.transform.tag == "Ground")
                {
                    place = new Vector3 (_hit.point.x, _hit.point.y, _hit.point.z);
                    Instantiate(grass, place, Quaternion.identity);
                    addNow = false;
                }
            }
        }
    }

    public void PlaceGrass()
    {
        addNow = true;
    }

}
