using System;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using Unity.VisualScripting;

public class MissionItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private RTLTextMeshPro text;

    private Mission mission;

    public void Initiate(Mission mission)
    {
        this.mission = mission;
        this.mission.onAddProgress += UpdateText;
        image.sprite = Resources.Load<Sprite>($"Sprites/{Enum.GetName(typeof(TrackableType), mission.Key)}");
        text.text = $"تم تجميع {mission.CurrentValue} من {mission.TargetValue}";
    }

    private void UpdateText() => text.text = $"تم تجميع {mission.CurrentValue} من {mission.TargetValue}";

    private void OnDisable() => mission.onAddProgress -= UpdateText;
}
