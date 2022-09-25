using DG.Tweening;
using System.Collections.Generic;
using System;
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


    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("ERROR | There is already a UIManager object!");
    }

    public Action OnInstantiateNewChicken;
    public Action OnInstantiateNewCow;
    public Action OnInstantiateNewSheep;

    public void Initiate()
    {
        var buttonItemsSet = new HashSet<Mission.Key>();
        foreach (var mission in LevelManager.Instance.CurrentLevel.Missions)
        {
            var missionItem = Instantiate(missionItemPrefab, MissionListContent);
            missionItem.Initiate(mission);

            if (buttonItemsSet.Contains(mission.Objective)) continue;

            var buttonItem = Instantiate(buttonItemPrefab, buttonList);
            buttonItem.Initiate(mission.Objective);
            buttonItemsSet.Add(mission.Objective);
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
