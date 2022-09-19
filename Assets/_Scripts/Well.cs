using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Well : MonoBehaviour
{
    [SerializeField] private int wellCapacity;
    [SerializeField] private Image waterBar;

    private int currentWater;

    public static Well Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new System.Exception("There is already a Well object!");

        currentWater = wellCapacity;
    }

    public void FillWater()
    {
        if (currentWater >= wellCapacity) return;

        currentWater = wellCapacity;
        waterBar.fillAmount = currentWater / (float)wellCapacity;
    }

    public bool UseWater()
    {
        if (currentWater <= 0) return false;

        currentWater -= 1;
        waterBar.fillAmount = currentWater / (float)wellCapacity;
        return true;
    }

}
