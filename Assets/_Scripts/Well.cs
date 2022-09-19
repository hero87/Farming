using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Well : MonoBehaviour
{
    [SerializeField] private int wellCapacity;
    [SerializeField] private Image waterBar;
    [SerializeField] private float fillingSpeed;
    [SerializeField] private Animator animator;

    private int currentWater;

    public static Well Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else throw new System.Exception("There is already a Well object!");

        currentWater = wellCapacity;
    }


    public void FillWater() => StartCoroutine(FillWaterCoroutine());

    private IEnumerator FillWaterCoroutine()
    {
        if (currentWater >= wellCapacity) yield break;
        animator.SetBool("ShouldFill", true);
        float value = currentWater;
        while (currentWater <= wellCapacity)
        {
            value += fillingSpeed * Time.deltaTime;
            waterBar.fillAmount = value / wellCapacity;
            currentWater = (int)value;
            yield return null;
            
        }
        animator.SetBool("ShouldFill", false);

    }

    public bool UseWater()
    {
        if (currentWater <= 0) return false;

        currentWater -= 1;
        waterBar.fillAmount = currentWater / (float)wellCapacity;
        return true;
    }

}
