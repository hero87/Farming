using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Products : MonoBehaviour
{
    private int ProductsLifeTime => LevelManager.Instance.GetSetting(Settings.Key.ProductsLifeTime);
    private int currentProductLife;
    private Animator animator;

    private void Awake()
    {
        currentProductLife = ProductsLifeTime;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        currentProductLife -= (int)(1000 * Time.deltaTime);
        if (currentProductLife <= 5000) animator.SetBool("Fade",true);
        if (currentProductLife <= 0) Destroy(gameObject);
    }


}
