using UnityEngine;
using SS.UI;
using TMPro;
using System;
using UnityEngine.UI;

public class LoadScreenController : MonoBehaviour, IKeyBack
{
    public const string NAME = "LoadScreen";
    [SerializeField] TextMeshProUGUI _loadingText;
    int _timer;
    
    public void OnKeyBack()
    {
        Core.Close();
    }

    void Update()
    {
        if
        _loadingText = Text[i];
    }
}