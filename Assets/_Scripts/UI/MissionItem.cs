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
        this.mission.OnAddProgress += UpdateText;
        image.sprite = Resources.Load<Sprite>($"Sprites/{Enum.GetName(typeof(Mission.Key), mission.Objective)}");
        text.text = $"Êã ÊÌãíÚ {mission.CurrentValue} ãä {mission.TargetValue}";
    }

    private void UpdateText() => text.text = $"Êã ÊÌãíÚ {mission.CurrentValue} ãä {mission.TargetValue}";

    private void OnDisable() => mission.OnAddProgress -= UpdateText;
}
