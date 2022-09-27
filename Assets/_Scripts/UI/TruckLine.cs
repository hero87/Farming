using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TruckLine : MonoBehaviour
{
    [SerializeField] private Animator truckAnimator;

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

    public void ActivateAnimation() => StartCoroutine(ActivateAnimationCoroutine());

    private IEnumerator ActivateAnimationCoroutine()
    {
        var time = LevelManager.Instance.CurrentLevel.GetSetting(Settings.Key.TradeTime) / 1000.0f;

        Active = true;
        truck.localScale = new Vector3(1, 1, 1);
        truck.DOAnchorPos(end.anchoredPosition, time / 2.0f);
        truckAnimator.Play("Go");
        yield return new WaitForSeconds(2 * time / 3);

        truck.localScale = new Vector3(-1, 1, 1);
        truck.DOAnchorPos(start.anchoredPosition, time / 2.0f);
        truckAnimator.Play("Back");
        yield return new WaitForSeconds(time / 3);
        Active = false;

        Truck.Instance.ConfirmTrade();
    }
}
