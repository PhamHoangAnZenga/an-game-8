using UnityEngine;
using SS.UI;
using TMPro;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.Networking;
using System.Linq;

public class DownloadScreenController : MonoBehaviour, IKeyBack
{
    public const string NAME = "DownloadScreen";
    public const string TEXT = "Downloading...";

    [SerializeField] TextMeshProUGUI _loadingText;
    [SerializeField] float _timerDelay = 1f;
    float _timer;
    int _index;

    public void OnKeyBack()
    {
        Core.Close();
    }

    void Update()
    {
        Debug.Log("on downloading...");

        if (_timer > _timerDelay)
        {
            _loadingText.text = TEXT.Substring(0, TEXT.Count() - 3 + _index);
            _index = (_index + 1) % 4;
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }

    
    public async Task<bool>  DownloadAudio(string url, ButtonElement buttonElement)
    {
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

        buttonElement.SetAudio( DownloadHandlerAudioClip.GetContent(request) );

        SaveFile(request.downloadHandler.data);

        Core.Close();
        return true;        
    }

    async void SaveFile(byte[] audioData)
    {
        await File.WriteAllBytesAsync(Application.persistentDataPath, audioData);        
    }
}