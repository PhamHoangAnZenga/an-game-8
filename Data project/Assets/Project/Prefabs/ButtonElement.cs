using System.IO;
using System.Threading.Tasks;
using SS.UI;
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

        // _fileName = $"{_audio}.mp3";
        _fileName = "agreement.mp3";
    }

    public void SetAudio(AudioClip audioClip)
    {
        _audioClip = audioClip;
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

            if(await LoadLocalAudio(savePath) == false)
            {
                return;
            }
        }
        
        _audioSource.PlayOneShot(_audioClip);
    }

    async Task<bool> DownloadAudio(string savePath)
    {
        string url = Path.Combine(URL, _fileName);

        DownloadScreenController downloadScreen = null;

        Core.Add<DownloadScreenController>(
            screenName: "DownloadScreen",
            onScreenLoad: screen => downloadScreen = screen);

        bool result = await downloadScreen.DownloadAudio(url, savePath);

        return result;
    }

    async Task<bool> LoadLocalAudio(string savePath)
    {
        string url = "file://" + savePath;

        using UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

        _ = request.SendWebRequest();

        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("cannot load file");
            return false;
        }

        _audioClip = DownloadHandlerAudioClip.GetContent(request);
        return true;
    }
}
