using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class ButtonItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private RTLTextMeshPro text;

    public void Initiate(Mission.Key key)
    {
        switch (key)
        {
            case Mission.Key.EggsCount:
            case Mission.Key.ChickensCount:
                text.text = @"≈÷«›… œÃ«Ã…";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewChicken);
                break;

            case Mission.Key.MilksCount:
            case Mission.Key.CowsCount:
                text.text = @"≈÷«›… »ﬁ—…";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewCow);
                break;

            case Mission.Key.MeatsCount:
            case Mission.Key.SheepsCount:
                text.text = @"≈÷«›… ‘«…";
                button.onClick.AddListener(LevelManager.Instance.InstantiateNewSheep);
                break;
        }
    }
}
