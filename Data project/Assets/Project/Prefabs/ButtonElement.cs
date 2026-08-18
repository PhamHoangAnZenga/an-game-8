using TMPro;
using UnityEngine;

public class ButtonElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _word;
    [SerializeField] TextMeshProUGUI _meaning;
    [SerializeField] TextMeshProUGUI _pronunciation;
    [SerializeField] TextMeshProUGUI _explain;
    [SerializeField] TextMeshProUGUI _example;
    [SerializeField] TextMeshProUGUI _id;

    public void SetData(int id, VocabularyItem data)
    {
        _id.text = id.ToString();
        _word.text = data.Word;
        _meaning.text = data.Meaning;
        _pronunciation.text = data.Pronunciation;
        if (data.Explain == null) _explain.text = "NULL";
        else _explain.text = data.Explain;

        if (data.Example == null) _example.text = "NULL";
        else _example.text = data.Example;
    }
}
