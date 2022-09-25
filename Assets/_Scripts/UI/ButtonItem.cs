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
    }

    public Key Function { get; private set; }

    public void Initiate(Mission.Key key)
    {
        Function = GetFunction(key);
        switch (Function)
        {
            case Key.AddChickn:
                text.text = @"≈÷«›… œÃ«Ã…";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewChicken);
                Function = Key.AddChickn;
                break;

            case Key.AddCow:
                text.text = @"≈÷«›… »ﬁ—…";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewCow);
                Function = Key.AddCow;
                break;

            case Key.AddSheep:
                text.text = @"≈÷«›… ‘«…";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewSheep);
                Function = Key.AddSheep;
                break;
        }
    }

    public static Key GetFunction(Mission.Key key)
    {
        switch (key)
        {
            case Mission.Key.EggsCount:
            case Mission.Key.ChickensCount:
                return Key.AddChickn;

            case Mission.Key.MilksCount:
            case Mission.Key.CowsCount:
                return Key.AddCow;

            case Mission.Key.MeatsCount:
            case Mission.Key.SheepsCount:
                return Key.AddSheep;
        }

        throw new System.Exception($"ERROR | No proper Function for key {key} exists");
    }
}
