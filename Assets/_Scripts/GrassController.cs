using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{

    [SerializeField] private List<Grass> grassList;
    private int grassIndex;
    [SerializeField] private float timeToCreate;
    private float timeLeft;


    private void Awake()
    {
        grassList.ForEach(m => m.gameObject.SetActive(false));
        timeLeft = timeToCreate;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            if (grassIndex == grassList.Count) { grassIndex = 0; return; }
            grassList[grassIndex].gameObject.SetActive(true);
            grassIndex++;
            timeLeft = timeToCreate;


        }
    }
}