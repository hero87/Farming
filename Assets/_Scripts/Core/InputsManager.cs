using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType { None, Click, Hold, }

public class InputsManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private float holdTime;

    private InputType inputType;
    private float firstClickTime;

    private void UpdateOnHold()
    {
        // when mouse is first down
        if (Input.GetMouseButtonDown(0))
        {
            inputType = InputType.Hold;
            firstClickTime = Time.time;
            //Debug.Log("Hold");
        }

        // when mouse is up
        if (Input.GetMouseButtonUp(0) && Time.time - firstClickTime < holdTime)
        {
            inputType = InputType.Click;
            //Debug.Log("Click");
        }

        //if (Input.GetMouseButtonUp(0) && Time.time - firstClickTime >= holdTime)
        //    inputType = InputType.Hold;
    }

    private void ResetOnHold()
    {
        if (Input.GetMouseButtonUp(0))
            inputType = InputType.None;
    }

    private void ManageCameraTouchController()
    {
        if (Extensions.IsInsideUI(canvas, Input.mousePosition))
            Camera.main.GetComponent<BitBenderGames.TouchInputController>().enabled = false;
        else
            Camera.main.GetComponent<BitBenderGames.TouchInputController>().enabled = true;
    }

    private void ManagePlayerInput()
    {
        if (inputType != InputType.Click) return;
        if (Extensions.IsInsideUI(canvas, Input.mousePosition)) return;
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
        UpdateOnHold();
        ManageCameraTouchController();
        ManagePlayerInput();
        ResetOnHold();
    }
}
