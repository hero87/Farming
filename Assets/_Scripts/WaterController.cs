using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class WaterController : MonoBehaviour
{

    [SerializeField] private Grass grassPrefab;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform grassParent;
    [SerializeField] private int maxWater;
    [SerializeField] private Image waterBar;


    private int currentWater;


    private void Awake()
    {
        currentWater = maxWater;
    }


    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;


        RaycastHit _hit;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, float.MaxValue, layerMask)) return;

        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && currentWater > 0)          // if it hits the ground
        {
            Grass grass = Instantiate(grassPrefab, _hit.point, Quaternion.identity, grassParent);
            currentWater -= 1;
            waterBar.fillAmount = currentWater / (float)maxWater;
        }

        // TODO there is the error
        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Well"))                           // if it hits the well
        {
            currentWater = maxWater;
            waterBar.fillAmount = currentWater / (float)maxWater;
        }
    }

    public void FillWell(BaseEventData baseEventData)
    {
        currentWater = 5;
    }


}
