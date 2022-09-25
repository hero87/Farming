using System;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class MissionItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private RTLTextMeshPro text;

    private Mission mission;

    public void Initiate(Mission mission)
    {
        this.mission = mission;
        this.mission.OnAddProgress += UpdateText;
        image.sprite = Resources.Load<Sprite>($"Sprites/{nameof(mission.Objective)}");
        text.text = $" „  Ã„Ì⁄ {mission.CurrentValue} „‰ {mission.TargetValue}";
    }

    private void UpdateText() => text.text = $" „  Ã„Ì⁄ {mission.CurrentValue} „‰ {mission.TargetValue}";
}
