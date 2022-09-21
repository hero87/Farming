using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Well : MonoBehaviour
{
    [SerializeField] private Image waterBar;
    [SerializeField] private float fillingSpeed;
    [SerializeField] private Animator animator;

    private int capacity;
    private int currentWater;

    public static Well Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new System.Exception("There is already a Well object!");
    }


    public bool Fill()
    {
        if (currentWater >= capacity) return false;
        StartCoroutine(FillWaterCoroutine());
        return true;
    }

    private IEnumerator FillWaterCoroutine()
    {
        float value = currentWater;
        animator.SetBool("ShouldFill", true);
        while (currentWater <= capacity)
        {
            value += fillingSpeed * Time.deltaTime;
            waterBar.fillAmount = value / capacity;
            currentWater = (int)value;
            yield return null;

        }
        animator.SetBool("ShouldFill", false);
    }

    public bool UseWater()
    {
        if (currentWater <= 0) return false;

        currentWater -= 1;
        waterBar.fillAmount = currentWater / (float)capacity;
        return true;
    }

    public int Capacity
    {
        set
        {
            capacity = value;
            currentWater = capacity;
        }
        get => currentWater;
    }
}
