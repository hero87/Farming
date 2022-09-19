using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class WaterController : MonoBehaviour
{

    [SerializeField] private Grass grassPrefab;
    [SerializeField] private Transform grassParent;
    [SerializeField] private int maxWater;
    [SerializeField] private Image waterBar;


    private int currentWater;
    private int LayersToHit;


    private void Awake()
    {
        currentWater = maxWater;

        var groundLayer = 1 << LayerMask.NameToLayer("Ground");
        var wellLayer = 1 << LayerMask.NameToLayer("Well");
        LayersToHit = groundLayer | wellLayer;
    }


    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var _hit, float.MaxValue, LayersToHit)) return;


        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && currentWater > 0)          // if it hits the ground
        {
            Debug.Log("X");
            Grass grass = Instantiate(grassPrefab, _hit.point, Quaternion.identity, grassParent);
            currentWater -= 1;
            waterBar.fillAmount = currentWater / (float)maxWater;
        }

        // TODO there is the error
        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Well"))                                // if it hits the well
        {
            currentWater = maxWater;
            waterBar.fillAmount = currentWater / (float)maxWater;
        }
    }

}
