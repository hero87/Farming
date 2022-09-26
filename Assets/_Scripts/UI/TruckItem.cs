using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using System;
using Unity.VisualScripting;

public class TruckItem : MonoBehaviour
{
    [SerializeField] private Image storageItem;
    [SerializeField] private Image truckItem;

    [SerializeField] private RTLTextMeshPro storageNumber;
    [SerializeField] private RTLTextMeshPro truckNumber;

    private Mission mission;

    public void Initiate(Mission mission)
    {
        this.mission = mission;

        Storage.Instance.onValueChanged += UpdateStorageNumber;
        Truck.Instance.onValueChanged += UpdateTruckNumber;

        storageItem.sprite = Resources.Load<Sprite>($"Sprites/{Enum.GetName(typeof(TrackableType), mission.Key)}");
        truckItem.sprite = Resources.Load<Sprite>($"Sprites/{Enum.GetName(typeof(TrackableType), mission.Key)}");

        truckNumber.text = $"X0";
        storageNumber.text = $"X0";
    }

    private void UpdateTruckNumber(TrackableType key, int value)
    {
        if (key != mission.Key) return;
        truckNumber.text = $"X{value}";
    }

    private void UpdateStorageNumber(TrackableType key, int value)
    {
        if (key != mission.Key) return;
        storageNumber.text = $"X{value}";
    }

    private void OnDisable()
    {
        Storage.Instance.onValueChanged -= UpdateStorageNumber;
        Truck.Instance.onValueChanged -= UpdateTruckNumber;
    }


    public void MoveToStorage()
    {
        Storage.Instance.IncreaseValueOf(mission.Key);
        Truck.Instance.DecreaseValueOf(mission.Key);
    }

    public void MoveToTruck()
    {
        Truck.Instance.IncreaseValueOf(mission.Key);
        Storage.Instance.DecreaseValueOf(mission.Key);
    }
}
