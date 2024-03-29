using UnityEngine;
using UnityEngine.UI;
using Microsoft.Maps;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;

public class MapPinManager : MonoBehaviour
{
    public static MapPinManager _instance { get; private set; }

    public GameObject mapPinPrefab;
    public Sprite newPinSprite; // ���ο� ��������Ʈ �̹���

    private MapRenderer mapRenderer;
    private MapPin mapPin;
    public MapPinLayer mapPinLayer;

    private MapPin[] pins;

    public void Init()
    {
        pins = new MapPin[8];

        int i = 0;
        foreach (var property in typeof(Places).GetFields())
        {
            if (property.FieldType == typeof(Vector2))
            {
                Vector2 location = (Vector2)property.GetValue(null);

                // MapPin�� Instantiate�Ͽ� �����մϴ�.
                GameObject newPinObject = Managers.Resource.Instantiate("MapPin");
                MapPin newPin = newPinObject.GetComponent<MapPin>();
                newPin.IsLayerSynchronized = false;

                // MapPin�� ��ġ�� �����մϴ�.
                newPin.Location = new LatLon(location.x, location.y);

                // MapPin�� �ڽ��� Image ������Ʈ�� �̹����� �����մϴ�.
                Image image = newPinObject.GetComponentInChildren<Image>();
                if (image != null)
                {
                    image.sprite = Resources.Load<Sprite>("Prefabs/Icon_PublicPlace");
                }

                // MapPin�� MapPinLayer�� �߰��մϴ�.
                mapPinLayer.MapPins.Add(newPin);

                // �迭�� MapPin�� �����մϴ�.
                pins[i] = newPin;
                i++;
            }
        }
    }

    void Start()
    {
        Input.location.Start();
        mapRenderer = GetComponent<MapRenderer>();

        // Init() �޼��带 ȣ���Ͽ� MapPins�� �ʱ�ȭ�մϴ�.
        Init();

        // ���� ��ġ�� ���� MapPin�� �����մϴ�.
        mapPin = Managers.Resource.Instantiate("MapPin").GetComponent<MapPin>();
        mapPin.name = "ME";
        mapPin.IsLayerSynchronized = false;
        mapPinLayer.MapPins.Add(mapPin);
        mapPin.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // ���� ��ġ�� MapPin�� �ݿ��մϴ�.
            mapPin.Location = new LatLon(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
    }
}
