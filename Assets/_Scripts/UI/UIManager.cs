using DG.Tweening;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Parameters")]
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

    [Header("Panels")]
    [SerializeField] private RectTransform winPanel;
    [SerializeField] private RectTransform losePanel;
    [SerializeField] private RectTransform missionsListPanel;
    [SerializeField] private RectTransform buttonsListPanel;
    [SerializeField] private RectTransform storagePanel;
    [SerializeField] private RectTransform truckPanel;
    [SerializeField] private RectTransform truckLinePanel;
    [SerializeField] private RectTransform timePanel;
    [SerializeField] private RectTransform coinsPanel;

    [Header("Containers")]
    [SerializeField] private RectTransform missionListContainer;
    [SerializeField] private RectTransform buttonsListContainer;
    [SerializeField] private RectTransform storageContainer;
    [SerializeField] private RectTransform truckContainer;

    [Header("Images")]
    [SerializeField] private Image storageSpaceFillImage;


    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("ERROR | There is already an UIManager object!");
    }


    public void Initiate()
    {
        var buttonItemsSet = new HashSet<ButtonItem.Key>();
        foreach (var objective in LevelManager.Instance.Objectives)
        {
            var missionItem = Instantiate(missionItemPrefab, missionListContainer);
            missionItem.Initiate(LevelManager.Instance.GetMission(objective));

            if (Extensions.IsCollectable(objective))
            {
                var storageItem = Instantiate(storageItemPrefab, storageContainer);
                storageItem.Initiate(objective);

                var truckItem = Instantiate(truckItemPrefab, truckContainer);
                truckItem.Initiate(objective);
            }

            if (ButtonItem.GetFunction(objective) == ButtonItem.Key.None) continue;
            if (buttonItemsSet.Contains(ButtonItem.GetFunction(objective))) continue;

            var buttonItem = Instantiate(buttonItemPrefab, buttonsListContainer);
            buttonItem.Initiate(objective);
            buttonItemsSet.Add(ButtonItem.GetFunction(objective));
        }
    }


    private void HideMainUI()
    {
        timePanel.DOAnchorPos3D(new Vector3(-40, 300, 0), animationTime);
        buttonsListPanel.DOAnchorPos3D(new Vector3(-1800, 0, 0), animationTime);
        storagePanel.DOAnchorPos3D(new Vector3(0, -400, 0), animationTime);
        truckLinePanel.DOAnchorPos3D(new Vector3(-30, -300, 0), animationTime);
        coinsPanel.DOAnchorPos3D(new Vector3(0, -300, 0), animationTime);
    }

    private void ViewMainUI()
    {
        timePanel.DOAnchorPos3D(new Vector3(-40, -20, 0), animationTime);
        buttonsListPanel.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime);
        storagePanel.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime);
        truckLinePanel.DOAnchorPos3D(new Vector3(-30, 30, 0), animationTime);
        coinsPanel.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime);
    }


    // EVENTS For LevelManager

    public void UpdateCoinsCount(int value) => coinsCountText.text = $"{value}";

    public void UpdateStorageSpace(float value) => storageSpaceFillImage.fillAmount = value;

    public void ViewWinPanel(int time, bool isGoldTime)
    {
        winPanel.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime);

        if (isGoldTime) winText.text = "وقت ذهبي!";
        else winText.text = "أحسنت!";

        HideMainUI();
    }

    public void ViewLosePanel()
    {
        losePanel.DOAnchorPos3D(new Vector3(0, 0, 0), animationTime);

        HideMainUI();
    }

    public void UpdateTime(int timeInt)
    {
        winTimeText.text = $"{timeInt}";
        timeText.text = $"{timeInt}";
    }



    // EVENTS For Buttons

    public void ReloadScene() => SceneManager.LoadScene("Game");

    public void ViewMissionList()
    {
        missionsListPanel.DOAnchorPos3D(new Vector3(-50, -75, 0), animationTime);
        HideMainUI();
    }

    public void HideMissionList()
    {
        missionsListPanel.DOAnchorPos3D(new Vector3(600, -75, 0), animationTime);
        ViewMainUI();
    }

    public void ViewTruck()
    {
        if (Truck.Instance.IsTradingActive) return;
        truckPanel.DOAnchorPos3D(new Vector3(0, -55, 0), animationTime);
        HideMainUI();
    }

    public void HideTruck()
    {
        Truck.Instance.MoveAllToStorage();
        truckPanel.DOAnchorPos3D(new Vector3(0, 855, 0), animationTime);
        ViewMainUI();
    }

    public void Trade()
    {
        if (Truck.Instance.TotalPrice <= 0) return;

        Truck.Instance.ConfirmTrade();
        TruckLine.Instance.ActivateAnimation();
        truckPanel.DOAnchorPos3D(new Vector3(0, 855, 0), animationTime);
        ViewMainUI();
    }
}
