using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputsManager : MonoBehaviour
{
    [SerializeField] private Grass grassPrefab;
    [SerializeField] private Transform grassParent;

    [SerializeField] private int maxWellCapacity;
    [SerializeField] private Image waterBar;


    private int currentWater;
    private int LayersToHit;


    private void Awake()
    {
        currentWater = maxWellCapacity;

        var groundLayer = 1 << LayerMask.NameToLayer("Ground");
        var wellLayer = 1 << LayerMask.NameToLayer("Well");
        var eggsLayer = 1 << LayerMask.NameToLayer("Eggs");
        var meatsLayer = 1 << LayerMask.NameToLayer("Meats");
        var milksLayer = 1 << LayerMask.NameToLayer("Milks");

        LayersToHit = groundLayer | wellLayer | eggsLayer | meatsLayer | milksLayer;
    }


    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue, LayersToHit)) return;

        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && currentWater > 0)
        {
            Instantiate(grassPrefab, hit.point, Quaternion.identity, grassParent);
            currentWater -= 1;
            waterBar.fillAmount = currentWater / (float)maxWellCapacity;
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Well") && currentWater < maxWellCapacity)
        {
            currentWater = maxWellCapacity;
            waterBar.fillAmount = currentWater / (float)maxWellCapacity;
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Eggs"))
        {
            //
        }
    }
}
