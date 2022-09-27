using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TruckLine : MonoBehaviour
{
    [SerializeField] private RectTransform truck;
    [SerializeField] private RectTransform start;
    [SerializeField] private RectTransform end;


    public static TruckLine Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new Exception("There is already a TruckLine object!");
    }

    public bool Active { get; private set; }

    public async void ActivateAnimation()
    {
        var time = LevelManager.Instance.CurrentLevel.GetSetting(Settings.Key.TradeSpeed);

        Active = true;
        truck.localScale = new Vector3(1, 1, 1);
        truck.DOAnchorPos(end.anchoredPosition, time / 2.0f);
        await Task.Delay(time / 2);

        truck.localScale = new Vector3(-1, 1, 1);
        truck.DOAnchorPos(start.anchoredPosition, time / 2.0f);
        await Task.Delay(time / 2);
        Active = false;

        Truck.Instance.ConfirmTrade();
    }
}
