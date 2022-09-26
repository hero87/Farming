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

    private Mission mission;

    public void Initiate(Mission mission)
    {
        this.mission = mission;
        this.mission.onAddProgress += UpdateText;
        image.sprite = Resources.Load<Sprite>($"Sprites/{Enum.GetName(typeof(TrackableType), mission.Key)}");
        number.text = $"X0";
    }

    private void UpdateText() => number.text = $"X{mission.CurrentValue}";

    private void OnDisable() => mission.onAddProgress -= UpdateText;
}
