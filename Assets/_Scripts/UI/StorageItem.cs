using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using System;

public class StorageItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private RTLTextMeshPro number;

    private Objective trackableType;

    public void Initiate(Objective trackableType)
    {
        this.trackableType = trackableType;
        Storage.Instance.onValueChanged += UpdateNumber;
        image.sprite = Extensions.GetSprite(trackableType);
        number.text = $"X0";
    }

    private void UpdateNumber(Objective trackableType, int value)
    {
        if (trackableType != this.trackableType) return;
        number.text = $"X{value}";
    }

    private void OnDisable() => Storage.Instance.onValueChanged -= UpdateNumber;
}
