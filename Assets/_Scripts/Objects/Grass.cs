using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private bool isTakenByAnimal;

    private void Awake() { transform.rotation = Extensions.GetRandomRotation(); StartCoroutine(SearchForAnimal()); }


    private IEnumerator SearchForAnimal()
    {
        while (!isTakenByAnimal)
        {
            Collider[] colliders = new Collider[25];
            int count = Physics.OverlapSphereNonAlloc(transform.position, LevelManager.Instance.GetSetting(SettingsKey.GrassRadius), colliders);

            for (int i = 0; i < count; i++)
            {
                var animal = colliders[i].GetComponent<Animal>();
                if (animal != null) isTakenByAnimal = animal.StartChasing(this);
                if (isTakenByAnimal) yield break;
            }

            yield return new WaitForSeconds(LevelManager.Instance.GetSetting(SettingsKey.GrassRefreshRate) / 1000.0f);
        }
    }
}
