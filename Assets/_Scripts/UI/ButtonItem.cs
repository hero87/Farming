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

    public void Initiate(Objective key)
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

    public static Key GetFunction(Objective key)
    {
        switch (key)
        {
            case Objective.EggsCount:
            case Objective.ChickensCount:
                return Key.AddChickn;

            case Objective.MilksCount:
            case Objective.CowsCount:
                return Key.AddCow;

            case Objective.MeatsCount:
            case Objective.SheepsCount:
                return Key.AddSheep;
        }

        //throw new System.Exception($"ERROR | No proper Function for key {key} exists");
        return Key.None;
    }
}
