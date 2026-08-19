using UnityEngine;
using SS.UI;
using TMPro;

public class LoadScreenController : MonoBehaviour, IKeyBack
{
    public const string NAME = "LoadScreen";
    [SerializeField] TextMeshProUGUI _loadingText;
    int _timer;

    public void OnKeyBack()
    {
        Core.Close();
    }

    void Start()
    {
        _loadingText.DOText();
    }
}