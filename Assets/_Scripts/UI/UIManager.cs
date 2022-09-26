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
    [SerializeField] private StorageItem storageItemPrefab;
    [SerializeField] private TruckItem truckItemPrefab;

    [SerializeField] private TextMeshProUGUI coinsCountText;
    [SerializeField] private Button viewMissionListButton;

    [SerializeField] private RectTransform missionsList;
    [SerializeField] private RectTransform storage;
    [SerializeField] private RectTransform truck;
    [SerializeField] private RectTransform buttonsList;

    public RectTransform MissionListContent => missionsList.GetComponentInChildren<ScrollRect>().content;


    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("ERROR | There is already an UIManager object!");
    }

    public void Initiate()
    {
        var buttonItemsSet = new HashSet<ButtonItem.Key>();
        foreach (var mission in LevelManager.Instance.CurrentLevel.Missions)
        {
            var missionItem = Instantiate(missionItemPrefab, MissionListContent);
            missionItem.Initiate(mission);

            var storageItem = Instantiate(storageItemPrefab, storage);
            storageItem.Initiate(mission);

            if (buttonItemsSet.Contains(ButtonItem.GetFunction(mission.Objective))) continue;

            var buttonItem = Instantiate(buttonItemPrefab, buttonsList);
            buttonItem.Initiate(mission.Objective);
            buttonItemsSet.Add(ButtonItem.GetFunction(mission.Objective));
        }
    }

    public void ViewMissionList()
    {
        missionsList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, -75, 0), 1.0f);
        viewMissionListButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, 150, 0), 1.0f);
    }

    public void HideMissionList()
    {
        missionsList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(600, -75, 0), 1.0f);
        viewMissionListButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-75, -75, 0), 1.0f);
    }
}
