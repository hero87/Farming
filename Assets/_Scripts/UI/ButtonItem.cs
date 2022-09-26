using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class ButtonItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private RTLTextMeshPro text;

    public enum Key
    {
        AddChickn,
        AddCow,
        AddSheep,
        None,
    }

    public Key Function { get; private set; }

    public void Initiate(TrackableType key)
    {
        Function = GetFunction(key);
        switch (Function)
        {
            case Key.AddChickn:
                text.text = @"أضف دجاجة";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewChicken);
                Function = Key.AddChickn;
                break;

            case Key.AddCow:
                text.text = @"أضف بقرة";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewCow);
                Function = Key.AddCow;
                break;

            case Key.AddSheep:
                text.text = @"أضف شاة";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewSheep);
                Function = Key.AddSheep;
                break;
        }
    }

    public static Key GetFunction(TrackableType key)
    {
        switch (key)
        {
            case TrackableType.EggsCount:
            case TrackableType.ChickensCount:
                return Key.AddChickn;

            case TrackableType.MilksCount:
            case TrackableType.CowsCount:
                return Key.AddCow;

            case TrackableType.MeatsCount:
            case TrackableType.SheepsCount:
                return Key.AddSheep;
        }

        //throw new System.Exception($"ERROR | No proper Function for key {key} exists");
        return Key.None;
    }
}
