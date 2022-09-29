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

    private Objective trackableType;

    public void Initiate(Objective trackableType)
    {
        this.trackableType = trackableType;

        Storage.Instance.onValueChanged += UpdateStorageNumber;
        Truck.Instance.onValueChanged += UpdateTruckNumber;

        storageItem.sprite = Extensions.GetSprite(trackableType);
        truckItem.sprite = Extensions.GetSprite(trackableType);

        truckNumber.text = $"x0";
        storageNumber.text = $"x0";
    }

    private void UpdateTruckNumber(Objective trackableType, int value)
    {
        if (trackableType != this.trackableType) return;
        truckNumber.text = $"x{value}";
    }

    private void UpdateStorageNumber(Objective trackableType, int value)
    {
        if (trackableType != this.trackableType) return;
        storageNumber.text = $"x{value}";
    }

    private void OnDisable()
    {
        Storage.Instance.onValueChanged -= UpdateStorageNumber;
        Truck.Instance.onValueChanged -= UpdateTruckNumber;
    }

    public void MoveToStorage() => Truck.Instance.MoveToStorage(trackableType, 1);

    public void MoveToTruck() => Storage.Instance.MoveToTruck(trackableType, 1);
}
