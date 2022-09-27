using DG.Tweening;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float animationTime;

    [SerializeField] private ButtonItem buttonItemPrefab;
    [SerializeField] private MissionItem missionItemPrefab;
    [SerializeField] private StorageItem storageItemPrefab;
    [SerializeField] private TruckItem truckItemPrefab;

    [SerializeField] private TextMeshProUGUI coinsCountText;
    [SerializeField] private Button viewMissionListButton;
    [SerializeField] private Button viewTrackButton;

    [SerializeField] private RectTransform missionsList;
    [SerializeField] private RectTransform buttonsList;
    [SerializeField] private RectTransform storage;
    [SerializeField] private RectTransform truck;

    public RectTransform MissionListContent => missionsList.GetComponentInChildren<ScrollRect>().content;
    public RectTransform TruckContent => truck.GetChild(0).GetComponent<RectTransform>();


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

            if (Extensions.IsCollectable(mission.Key))
            {
                var storageItem = Instantiate(storageItemPrefab, storage);
                storageItem.Initiate(mission);

                var truckItem = Instantiate(truckItemPrefab, TruckContent);
                truckItem.Initiate(mission);
            }

            if (ButtonItem.GetFunction(mission.Key) == ButtonItem.Key.None) continue;
            if (buttonItemsSet.Contains(ButtonItem.GetFunction(mission.Key))) continue;

            var buttonItem = Instantiate(buttonItemPrefab, buttonsList);
            buttonItem.Initiate(mission.Key);
            buttonItemsSet.Add(ButtonItem.GetFunction(mission.Key));
        }
    }

    public void UpdateCoinText(int value) => coinsCountText.text = $"{value}";

    private void HideMainUI()
    {
        viewMissionListButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, 300, 0), animationTime);
        viewTrackButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, 200, 0), animationTime);
        buttonsList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-1800, 0, 0), animationTime);
        storage.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0, -300, 0), animationTime);
    }

    private void ViewMainUI()
    {
        viewMissionListButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, -38, 0), animationTime);
        viewTrackButton.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, -138, 0), animationTime);
        buttonsList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0, 0, 0), animationTime);
        storage.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0, 15, 0), animationTime);
    }



    public void ViewMissionList()
    {
        missionsList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-50, -75, 0), 1.0f);
        HideMainUI();
    }

    public void HideMissionList()
    {
        missionsList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(600, -75, 0), 1.0f);
        ViewMainUI();
    }

    public void ViewTruck()
    {
        if (TruckLine.Instance.Active) return;
        truck.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0, -55, 0), 1.0f);
        HideMainUI();
    }

    public void HideTruck()
    {
        truck.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0, 855, 0), 1.0f);
        ViewMainUI();
    }

    public void Trade()
    {
        TruckLine.Instance.ActivateAnimation();
        HideTruck();
    }
}
