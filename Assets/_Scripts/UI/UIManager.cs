using DG.Tweening;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float animationTime;

    [Header("Prefabs")]
    [SerializeField] private ButtonItem buttonItemPrefab;
    [SerializeField] private MissionItem missionItemPrefab;
    [SerializeField] private StorageItem storageItemPrefab;
    [SerializeField] private TruckItem truckItemPrefab;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI coinsCountText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI winTimeText;

    [Header("Panel")]
    [SerializeField] private RectTransform winPanel;
    [SerializeField] private RectTransform losePanel;
    [SerializeField] private RectTransform missionsList;
    [SerializeField] private RectTransform buttonsList;
    [SerializeField] private RectTransform storage;
    [SerializeField] private RectTransform truck;
    [SerializeField] private RectTransform truckLine;
    [SerializeField] private RectTransform viewsPanel;


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
                storageItem.Initiate(mission.Key);

                var truckItem = Instantiate(truckItemPrefab, TruckContent);
                truckItem.Initiate(mission.Key);
            }

            if (ButtonItem.GetFunction(mission.Key) == ButtonItem.Key.None) continue;
            if (buttonItemsSet.Contains(ButtonItem.GetFunction(mission.Key))) continue;

            var buttonItem = Instantiate(buttonItemPrefab, buttonsList);
            buttonItem.Initiate(mission.Key);
            buttonItemsSet.Add(ButtonItem.GetFunction(mission.Key));
        }
    }


    // EVENTS


    public void UpdateCoinText(int value) => coinsCountText.text = $"{value}";

    private void HideMainUI()
    {
        viewsPanel.DOAnchorPos3D(new Vector3(-40, 300, 0), animationTime);
        buttonsList.DOAnchorPos3D(new Vector3(-1800, 0, 0), animationTime);
        storage.DOAnchorPos3D(new Vector3(0, -300, 0), animationTime);
        truckLine.DOAnchorPos3D(new Vector3(-30, -300, 0), animationTime);
    }

    private void ViewMainUI()
    {
        viewsPanel.DOAnchorPos3D(new Vector3(-40, -20, 0), animationTime);
        buttonsList.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime);
        storage.DOAnchorPos3D(new Vector3(0, 15, 0), animationTime);
        truckLine.DOAnchorPos3D(new Vector3(-30, 30, 0), animationTime);
    }

    public void ViewMissionList()
    {
        missionsList.DOAnchorPos3D(new Vector3(-50, -75, 0), animationTime);
        HideMainUI();
    }

    public void HideMissionList()
    {
        missionsList.DOAnchorPos3D(new Vector3(600, -75, 0), animationTime);
        ViewMainUI();
    }

    public void ViewTruck()
    {
        if (TruckLine.Instance.Active) return;
        truck.DOAnchorPos3D(new Vector3(0, -55, 0), animationTime);
        HideMainUI();
    }

    public void HideTruck()
    {
        Truck.Instance.MoveAllToStorage();
        truck.DOAnchorPos3D(new Vector3(0, 855, 0), animationTime);
        ViewMainUI();
    }

    public void Trade()
    {
        TruckLine.Instance.ActivateAnimation();
        truck.DOAnchorPos3D(new Vector3(0, 855, 0), animationTime);
        ViewMainUI();
    }

    public void ViewWinPanel(string info, string time)
    {
        winPanel.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime).OnComplete( () => Time.timeScale = 0);
        winText.text = info;
        winTimeText.text = time;
        HideMainUI();
    }

    public void ViewLosePanel()
    {
        losePanel.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime).OnComplete(() => Time.timeScale = 0);
        HideMainUI();
    }

    public void SetTime(string time) => timeText.text = time;
}
