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
        if (_timer > _timerDelay)
        {
            _loadingText.text = TEXT.Substring(0, TEXT.Count() - 3 + _index);
            _index = (_index + 1) % 4;
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
    
    public async Task<bool>  DownloadAudio(string url, string savePath)
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);

        Debug.Log("Start Download: " + url);

        _ = request.SendWebRequest();

        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("cannot download");
            Core.Close();
            return false;
        }

        Debug.Log("download success");
        await SaveFile(request.downloadHandler.data, savePath);
        Core.Close();
        return true;
    }

    async Task SaveFile(byte[] audioData, string savePath)
    {
        await File.WriteAllBytesAsync(savePath, audioData);
        Debug.Log("save file success");     
    }
}