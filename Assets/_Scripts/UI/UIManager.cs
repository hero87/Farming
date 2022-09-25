using DG.Tweening;
using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  
    [SerializeField] private ButtonItem buttonItemPrefab;
    [SerializeField] private MissionItem missionItemPrefab;
    [SerializeField] private TextMeshProUGUI coinsCountText;
    [SerializeField] private Button viewMissionListButton;
    [SerializeField] private RectTransform missionList;
    [SerializeField] private RectTransform buttonList;
    public RectTransform MissionListContent => missionList.GetComponentInChildren<ScrollRect>().content;


    private void Set()
    {


        foreach (var item in LevelManager.Instance.CurrentLevel.GetMissions)
        {
            var missionItem = Instantiate(missionItemPrefab, missionList);
            missionItem.Set(item);
        }


        foreach (var item in LevelManager.Instance.CurrentLevel.GetMissions)
        {
            var buttonItem = Instantiate(buttonItemPrefab, buttonList);
            buttonItem.Set(item);
        }


    }

    public void ViewMissionList()
    {
        missionList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, -75, 0), 1.0f);
        viewMissionListButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, 150, 0), 1.0f);
    }

    public void HideMissionList()
    {
        missionList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(600, -75, 0), 1.0f);
        viewMissionListButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-75, -75, 0), 1.0f);
    }
}
