using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Mission 01", menuName = "Create New Mission")]
public class Mission : ScriptableObject
{
    public enum Key
    {
        EggsCount,
        MilksCount,
        MeatsCount,
        BurgersCount,
        BreadsCount,
        CakesCount,
        ChickensCount,
        CowsCount,
        SheepsCount,
    }


    [SerializeField] private Key key;
    [SerializeField] private int targetValue;
    [SerializeField] private Sprite sprite;


    public Key GetKey => key;
    public int GetTargetValue => targetValue;
    public int GetCurrentValue => currentValue;
    public bool Completed => currentValue >= targetValue;


    private int currentValue;
    private GameObject missionCard;
    private RTLTextMeshPro info;
    private Image image;


    public void AddProgress()
    {
        currentValue++;
        info.text = $" „  Ã„Ì⁄ {currentValue} „‰ {targetValue}";
    }

    public void Initiate()
    {
        currentValue = 0;
        InitiateMissionCard();
        InitiateMissionButton();
    }

    private void InitiateMissionCard()
    {
        missionCard = Instantiate(LevelManager.Instance.MissionCardPrefab, LevelManager.Instance.MissionListContent);
        info = missionCard.GetComponentInChildren<RTLTextMeshPro>();
        image = missionCard.GetComponentInChildren<Image>();
        info.text = $" „  Ã„Ì⁄ {currentValue} „‰ {targetValue}";
        image.sprite = sprite;
    }

    private void InitiateMissionButton()
    {
        var button = Instantiate(LevelManager.Instance.InstantiateButtonPrefab, LevelManager.Instance.InstantiateListContent);

        switch (key)
        {
            case Key.EggsCount:
            case Key.ChickensCount:
                button.GetComponent<Button>().onClick.AddListener(LevelManager.Instance.InstantiateNewChicken);
                button.GetComponentInChildren<RTLTextMeshPro>().text = "√÷› œÃ«Ã…";
                break;

            case Key.MilksCount:
            case Key.CowsCount:
                button.GetComponent<Button>().onClick.AddListener(LevelManager.Instance.InstantiateNewCow);
                button.GetComponentInChildren<RTLTextMeshPro>().text = "√÷› »ﬁ—…";
                break;

            case Key.MeatsCount:
            case Key.SheepsCount:;
                button.GetComponent<Button>().onClick.AddListener(LevelManager.Instance.InstantiateNewSheep);
                button.GetComponentInChildren<RTLTextMeshPro>().text = "√÷› €‰„…";
                break;
        }
    }
}


