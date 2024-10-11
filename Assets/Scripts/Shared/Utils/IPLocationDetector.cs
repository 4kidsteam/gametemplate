using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore.Text;
using Newtonsoft.Json;

namespace Shared.Utils
{
    public class IPLocationDetector : MonoBehaviour
    {
        [System.Serializable]
        public class Country
        {
            [JsonProperty("businessName")] private string businessName;
            [JsonProperty("businessWebsite")] private string businessWebsite;
            [JsonProperty("city")] private string city;
            [JsonProperty("continent")] private string continent;
            [JsonProperty("country")] private string country;
            [JsonProperty("countryCode")] private string countryCode;
            [JsonProperty("ipName")] private string ipName;
            [JsonProperty("ipType")] private string ipType;
            [JsonProperty("isp")] private string isp;
            [JsonProperty("lat")] private string lat;
            [JsonProperty("lon")] private string lon;
            [JsonProperty("org")] private string org;
            [JsonProperty("query")] private string query;
            [JsonProperty("region")] private string region;
            [JsonProperty("status")] private string status;

            public override string ToString() => JsonConvert.SerializeObject(this);
        }

        const string TAG = "IPLocationDetector";

        private static IPLocationDetector _instance;
        public static IPLocationDetector Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject(TAG);
                    _instance = go.AddComponent<IPLocationDetector>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private Country _country = null;
        private FrameCounter _frameCounter = new FrameCounter(space: 600);

        public void DetectCountry()
        {
#if LOG_INFO
            if (Application.platform == RuntimePlatform.Android)
            {
                Debug.LogFormat("{0}->DetectCountry: APS", TAG);
                StartCoroutine(_DetectCountry());
            }
#endif
        }

        private IEnumerator _DetectCountry()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://extreme-ip-lookup.com/json/?key=J1XZpbNPmfTaFJaDx2X9");
            yield return request.SendWebRequest();


            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.LogFormat("{0}->_DetectCountry: APS SUCCESS = {1}", TAG, request.downloadHandler.text);
                _country = JsonConvert.DeserializeObject<Country>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogErrorFormat("{0}->_DetectCountry: APS ERROR: {1}", TAG, request.result.ToString());
            }
        }

#if LOG_INFO
        private void Update()
        {
            _frameCounter.IncreaseByOne();
            if (_frameCounter.Allow())
            {
                if (_country == null)
                {
                    Debug.LogFormat("{0}-> APS Detect Country = NULL", TAG);
                }
                else
                {
                    Debug.LogFormat("{0}-> APS Detect Country = {1}", TAG, _country.ToString());
                }
            }
        }
#endif
    }
}