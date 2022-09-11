using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    #region Singleton
    public static GrassManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject grass;
}
