using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private bool isTakenByAnimal;

    private void Awake() { transform.rotation = Extensions.GetRandomRotation(); SearchForAnimal(); }


    private async void SearchForAnimal()
    {
        while (!isTakenByAnimal)
        {
            Collider[] colliders = new Collider[25];
            int count = Physics.OverlapSphereNonAlloc(transform.position, LevelManager.Instance.GetSetting(Settings.Key.GrassRadius), colliders);

            for (int i = 0; i < count; i++)
            {
                var animal = colliders[i].GetComponent<Animal>();
                if (animal != null) isTakenByAnimal = animal.StartChasing(this);
                if (isTakenByAnimal) return;
            }

            await Task.Delay(LevelManager.Instance.GetSetting(Settings.Key.GrassRefreshRate));
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere(transform.position, LevelManager.Instance.GetSetting(Settings.Key.GrassRadius));
    //}
}
