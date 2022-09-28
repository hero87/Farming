using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Products : MonoBehaviour
{
    public int ProductsLifeTime => LevelManager.Instance.GetSetting(Settings.Key.ProductsLifeTime);
    public int currentProductLife;

    private void Awake()
    {
        currentProductLife = ProductsLifeTime;
    }
    private void Update()
    {
        currentProductLife -= (int)(1000 * Time.deltaTime);
        if (currentProductLife <= 0) Destroy(gameObject);
    }


}
