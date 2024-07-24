using OneSignalSDK;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class AppInitializator : MonoBehaviour
{
    [SerializeField] string oneSignalAppID;

    private const string WEBSITE_IP = "https://extreme-ip-lookup.com/json/?key=CZrhdcy6MTRTmo94YslD";
    private const string WEBSITE_WIKIPEDIA = "https://www.wikipedia.org/";
    private const string REQUIRED_COUNTRY = "Ukraine";

    private string _userCountry;

    private void Start()
    {
        OneSignal.Initialize(oneSignalAppID);
        Debug.Log("OneSignalSDK init");

        StartCoroutine(DetectCountry());
    }

    private IEnumerator DetectCountry()
    {
        UnityWebRequest request = UnityWebRequest.Get(WEBSITE_IP);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Detection of country is failed. Opening website...");
            Application.OpenURL(WEBSITE_WIKIPEDIA);
            Application.Quit();
        }
        else
        {
            if (request.isDone) CheckOnGameRunning(request);
        }
    }

    private void CheckOnGameRunning(UnityWebRequest request)
    {
        GeoData res = JsonUtility.FromJson<GeoData>(request.downloadHandler.text);
        _userCountry = res.country;
        Debug.Log("Country: " + _userCountry);

        if (res.country == REQUIRED_COUNTRY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Opening website...");
            Application.OpenURL(WEBSITE_WIKIPEDIA);
            Application.Quit();
        }
    }
}
