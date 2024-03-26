using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NStaticMapManager : MonoBehaviour
{
    [Header("�� ���� �Է�")]
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

    private float latitude = default;
    private float longitude = default;

    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int)Math.Round(rect.height);

        Input.location.Start();
        StartCoroutine(UpdateLocation());
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ��ġ ���� ������Ʈ �ڷ�ƾ
    IEnumerator UpdateLocation()
    {
        while (true)
        {
            if (!Input.location.isEnabledByUser)
            {
                Managers.Android.ShowAndroidToastMessage("��ġ ���񽺰� Ȱ��ȭ�Ǿ� ���� �ʽ��ϴ�.");
                yield break;
            }

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait <= 0)
            {
                Managers.Android.ShowAndroidToastMessage("�ð� �ʰ�: ��ġ ���񽺸� ������ �� �����ϴ�.");
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Running)
            {
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;

                UpdateMap();
            }
            else
            {
                Managers.Android.ShowAndroidToastMessage("��ġ ���񽺸� ������ �� �����ϴ�.");
            }

            yield return new WaitForSeconds(1.5f);
        }
    }

    // �� ������Ʈ �Լ�
    void UpdateMap()
    {
        url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster?" + "w=" + mapWidth.ToString() + "&h=" + mapHeight.ToString() + $"&markers=type:d|size:mid|pos:{longitude} {latitude}" + "&center=" + $"{longitude} {latitude}" + "&level=" + level.ToString();
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        www.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        www.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        StartCoroutine(LoadMap(www));
    }

    // �� �ε� �Լ�
    IEnumerator LoadMap(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            GetComponent<RawImage>().texture = DownloadHandlerTexture.GetContent(www);
        }
        else
        {
            Managers.Android.ShowAndroidToastMessage($"error. �� �ҷ����� ����\n{www.result}");
        }
    }
}
