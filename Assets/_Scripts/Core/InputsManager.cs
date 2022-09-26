using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputsManager : MonoBehaviour
{
    private int LayersToHit;


    private void Awake()
    {
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

        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Ground.Instance.PlantGrass(hit.point);
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Well"))
        {
            LevelManager.Instance.FillWell();
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Eggs"))
        {
            LevelManager.Instance.CurrentLevel.AddProgressTo(TrackableType.EggsCount);
            Destroy(hit.collider.gameObject);
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Milks"))
        {
            LevelManager.Instance.CurrentLevel.AddProgressTo(TrackableType.MilksCount);
            Destroy(hit.collider.gameObject);
        }
    }
}
