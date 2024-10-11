using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public enum Cmd
{
    GET = 3001,
    SET = 3002,
    REMOVE = 3003,
    PUSH_EVENT = 3004
}



public class EventTracking : MonoBehaviour
{
    [System.Serializable]
    public class API
    {
        [System.Serializable]
        public class Data
        {
            public string path = "";
            public Dictionary<string, object> eventInfo;
        }

        public string token = "fhdajshutewy8gr89s7d89e7w";
        public Cmd cmd;
        public Data data;

    }


    static EventTracking _inst;
    static EventTracking Inst
    {
        get
        {
            if (!_inst)
            {
                GameObject newGameObj = new GameObject("Event Tracking");
                _inst =  newGameObj.AddComponent<EventTracking>();
                DontDestroyOnLoad(newGameObj);
            }
            return _inst;
        }
    }


    string domain = "https://sliboxdatabase.onrender.com";
    

    public static void PushEvent(string path, Dictionary<string , object> data)
    {
        API api = new API();
        api.data = new API.Data();

        api.cmd = Cmd.PUSH_EVENT;

        api.data.path = path;
        api.data.eventInfo = data;

        Inst.SendTrackingEvent(api);

    }

    public static void SetData(string path, Dictionary<string, object> data)
    {
        API api = new API();
        api.data = new API.Data();

        api.cmd = Cmd.SET;

        api.data.path = path;
        api.data.eventInfo = data;

        Inst.SendTrackingEvent(api);
    }

    public void SendTrackingEvent(API api, System.Action<string> callback = null)
    {
        StartCoroutine(SendMessageCoroutine(api, callback));
    }

    private IEnumerator SendMessageCoroutine(API api, System.Action<string> callback)
    {
        string url = domain + "/api";
        string mess = JsonConvert.SerializeObject(api);
        Debug.Log(mess);
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(mess);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            string responseText = webRequest.downloadHandler.text;
            Debug.Log("Received mess: " + responseText);
            callback?.Invoke(responseText);
        }
    }


}
