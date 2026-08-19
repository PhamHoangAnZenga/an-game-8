using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] int _batchSize = 11;

    [SerializeField] ButtonElement _buttonPrefab;
    [SerializeField] Transform _container;
    [SerializeField] AudioSource _audioSource;

    List<VocabularyItem> _datas = new List<VocabularyItem>();
    List<ButtonElement> _item = new List<ButtonElement>();

    int _startID;

    void Start()
    {
        string json = Resources.Load<TextAsset>("JsonData").text;
        var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<List<VocabularyItem>>>(json);
        _datas = temp[0];
        // Debug.Log(_item.Count);

        _startID = 0;
        for (int i = 0; i < _batchSize; ++i)
        {
            _item.Add( Instantiate(_buttonPrefab, _container) ); 
            _item[i].SetData(i+1, _datas[i], _audioSource);
        }
    }

    public void NextData()
    {
        _startID = Mathf.Min(_startID + _batchSize, _datas.Count - _batchSize);
        UpdateData();
    }

    public void PrevData()
    {
        _startID = Mathf.Max(0, _startID - _batchSize);
        UpdateData();
    }
    
    private void UpdateData(){
        for (int i = 0; i < _batchSize; ++i)
        {
            _item[i].SetData(i+1 + _startID, _datas[i + _startID], _audioSource);
        }        
    }
}
