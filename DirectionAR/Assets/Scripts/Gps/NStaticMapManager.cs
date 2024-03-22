using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NStaticMapManager : MonoBehaviour
{
    [Header("맵 정보 입력")]
    public string clientID = "";
    public string secretKey = "";
    private string url = "";
    private int mapWidth = 640;
    private int mapHeight = 640;
    public int level = 20;
    private Rect rect;
    public enum scale { low = 1, high = 2 };
    public scale mapResolution = scale.low;
    public enum type { basic, traffic, satellite, satellite_base, terrain };
    public type mapType = type.basic;

    public TextMeshProUGUI mapLog = new TextMeshProUGUI(); 

    private int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetStaticMap());
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int)Math.Round(rect.height);

    }

    // Update is called once per frame
    void Update()
    {
        if (frame >= 100)
        {
            StartCoroutine(GetStaticMap());
            frame = 0;
        }
        frame++;
    }


    IEnumerator GetStaticMap()
    {
        url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster?" + "w=" + mapWidth + "&h="+ mapHeight + "&level="+ level +"&format=png";

        var query = "";
        query += "&center=" + UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", Input.location.lastData.latitude, Input.location.lastData.longitude));

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url + query);
        www.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        www.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Destroy(GetComponent<RawImage>().texture);
            GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            mapLog.text = "택스쳐 불러오기 성공";
        }
        else
        {
            mapLog.text = "error. 맵 불러오기 실패";
        }
    }
}
