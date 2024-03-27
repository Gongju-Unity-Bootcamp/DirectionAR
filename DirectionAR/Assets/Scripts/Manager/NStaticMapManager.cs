using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class NStaticMapManager : MonoBehaviour
{
    [Header("�� ���� �Է�")]
    private string url = "";
    private int mapWidth = 640;
    private int mapHeight = 640;
    private Rect rect;
    private int frame = 0;
    public enum scale { low = 1, high = 2 };
    public scale mapResolution = scale.low;
    public enum type { basic, traffic, satellite, satellite_base, terrain };
    public type mapType = type.basic;

    public RawImage rawImage;

    private float latitude;
    private float longitude;
    private float panLatitude;
    private float panLongitude;

    [Range(1, 20)] public int level = 19;

    public float zoomSmoothTime = 0.2f;

    private Texture2D mapTexture;

    private bool isMapLoaded = false;
    private bool isPanMap = false;

    private string markers = default;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rect = rawImage.rectTransform.rect;
        mapWidth = (int)Mathf.Round(rect.width);
        mapHeight = (int)Mathf.Round(rect.height);

        System.Reflection.FieldInfo[] fields = typeof(Markers).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        foreach (var field in fields)
        {
            // Markers Ŭ������ �ʵ尪�� �����ͼ� markers ������ ����
            string fieldValue = (string)field.GetValue(null);
            markers += fieldValue;
        }

        Input.location.Start();
        StartCoroutine(UpdateLocation());
    }

    void Update()
    {
        if (frame >= 100)
        {
            UpdateMap();
            StartCoroutine(UpdateLocation());
            frame = 0;
        }
        frame++;
        Zoom();
        PanMap();
    }

    private void Zoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

            if (deltaMagnitudeDiff > 0)
            {
                if (level < 20)
                    level++;
            }
            else if (deltaMagnitudeDiff < 0)
            {
                if (level > 15)
                    level--;
            }

            UpdateMap();
            StartCoroutine(LoadMap());
        }
    }

    private void PanMap()
    {
        if (!isPanMap)
        {
            panLongitude = longitude;
            panLatitude = latitude;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && isMapLoaded)
            {
                isPanMap = true;

                Vector2 delta = touch.deltaPosition;
                float deltaX = delta.x * (360f / Screen.width) / (level * 5000);
                float deltaY = delta.y * (180f / Screen.height) / (level * 5000);
                panLongitude -= deltaX;
                panLatitude -= deltaY;

                UpdateMap();
                StartCoroutine(LoadMap());
            }
        }
    }


    // ��ġ ���� ������Ʈ �ڷ�ƾ
    IEnumerator UpdateLocation()
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

            if (!isMapLoaded) // ���� �ε���� �ʾ��� ������ �ε�
            {
                UpdateMap();
                StartCoroutine(LoadMap());
            }
        }
        else
        {
            Managers.Android.ShowAndroidToastMessage("��ġ ���񽺸� ������ �� �����ϴ�.");
        }
    }

    // �� ������Ʈ �Լ�
    void UpdateMap()
    {
        float tempLong;
        float tempLat;

        if (isPanMap)
        {
            tempLong = panLongitude;
            tempLat = panLatitude;
        }
        else
        {
            tempLong = longitude;
            tempLat = latitude;
        }

        url = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster?" + "w=" + mapWidth.ToString() + "&h=" + mapHeight.ToString() + $"&markers=type:d|size:mid|pos:{Input.location.lastData.longitude} {Input.location.lastData.latitude}" + markers + "&center=" + $"{tempLong} {tempLat}" + "&level=" + level.ToString();
    }

    // �� �ε� �Լ�
    IEnumerator LoadMap()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        www.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", Managers.Android.clientID);
        www.SetRequestHeader("X-NCP-APIGW-API-KEY", Managers.Android.secretKey);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            mapTexture = DownloadHandlerTexture.GetContent(www);
            rawImage.texture = mapTexture;
            isMapLoaded = true;
        }
        else
        {
            Managers.Android.ShowAndroidToastMessage($"error. �� �ҷ����� ����\n{www.result}");
        }
    }

    public void ResetPan()
    {
        isPanMap = false;
        level = 19;
        UpdateMap();
        StartCoroutine(LoadMap());
    }
}
