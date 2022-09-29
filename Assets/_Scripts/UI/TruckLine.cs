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


    public void ActivateAnimation() => StartCoroutine(ActivateAnimationCoroutine());

    private IEnumerator ActivateAnimationCoroutine()
    {
        var time = LevelManager.Instance.GetSetting(SettingsKey.TradeTime) / 1000.0f;

        truck.localScale = new Vector3(1, 1, 1);
        truck.DOAnchorPos(end.anchoredPosition, time / 2.0f);
        yield return new WaitForSeconds(2 * time / 3);

        truck.localScale = new Vector3(-1, 1, 1);
        truck.DOAnchorPos(start.anchoredPosition, time / 2.0f);
        yield return new WaitForSeconds(time / 3);
    }
}
