using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonElement : MonoBehaviour
{
    static readonly string URL = "https://600tuvungtoeic.com/audio/";
    
    [SerializeField] TextMeshProUGUI _word;
    [SerializeField] TextMeshProUGUI _meaning;
    [SerializeField] TextMeshProUGUI _pronunciation;
    [SerializeField] TextMeshProUGUI _explain;
    [SerializeField] TextMeshProUGUI _example;
    [SerializeField] TextMeshProUGUI _id;

    AudioSource _audioSource;
    AudioClip _audioClip;
    string _fileName;
    string _audio;

    public void SetData(int id, VocabularyItem data, AudioSource audioSource)
    {
        _id.text = id.ToString();
        _word.text = data.Word;
        _meaning.text = data.Meaning;
        _pronunciation.text = data.Pronunciation;

        if (data.Explain == null) _explain.text = "NULL";
        else _explain.text = data.Explain;

        if (data.Example == null) _example.text = "NULL";
        else _example.text = data.Example;

        _audio = data.Audio;
        _audioSource = audioSource;

        _fileName = $"{_audio}.mp3";
    }

    public void OnButtonClick()
    {
        Debug.Log(_audio);
        PlayAudio();
    }
    
    async void PlayAudio()
    {
        if (_audioClip == null)
        {
            string savePath = Path.Combine(Application.persistentDataPath, _fileName);

            if (!File.Exists(savePath))
            {
                if (await DownloadAudio(savePath) == false)
                {
                    return;
                }
            }
            else
            {
                _audioClip = DownloadHandlerAudioClip.GetContent(UnityWebRequestMultimedia.GetAudioClip(savePath, AudioType.MPEG));
            }
            
        }
        
        _audioSource.PlayOneShot(_audioClip);
    }

    async Task<bool> DownloadAudio(string savePath)
    {
        string url = Path.Combine(URL, _fileName);
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("cannot download");
            return false;
        }

        _audioClip = DownloadHandlerAudioClip.GetContent(request);

        SaveFile(request.downloadHandler.data);
        
        return true;
    }

    async void SaveFile(byte[] audioData)
    {
        await File.WriteAllBytesAsync(Application.persistentDataPath, audioData);        
    }
}
