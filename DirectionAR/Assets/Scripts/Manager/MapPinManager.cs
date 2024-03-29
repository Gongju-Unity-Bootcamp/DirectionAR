using UnityEngine;
using UnityEngine.UI;
using Microsoft.Maps;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;

public class MapPinManager : MonoBehaviour
{
    public static MapPinManager _instance { get; private set; }

    public GameObject mapPinPrefab;
    public Sprite newPinSprite; // 새로운 스프라이트 이미지

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

                // MapPin을 Instantiate하여 생성합니다.
                GameObject newPinObject = Managers.Resource.Instantiate("MapPin");
                MapPin newPin = newPinObject.GetComponent<MapPin>();
                newPin.IsLayerSynchronized = false;

                // MapPin의 위치를 설정합니다.
                newPin.Location = new LatLon(location.x, location.y);

                // MapPin의 자식인 Image 컴포넌트의 이미지를 변경합니다.
                Image image = newPinObject.GetComponentInChildren<Image>();
                if (image != null)
                {
                    image.sprite = Resources.Load<Sprite>("Prefabs/Icon_PublicPlace");
                }

                // MapPin을 MapPinLayer에 추가합니다.
                mapPinLayer.MapPins.Add(newPin);

                // 배열에 MapPin을 저장합니다.
                pins[i] = newPin;
                i++;
            }
        }
    }

    void Start()
    {
        Input.location.Start();
        mapRenderer = GetComponent<MapRenderer>();

        // Init() 메서드를 호출하여 MapPins를 초기화합니다.
        Init();

        // 현재 위치에 대한 MapPin을 생성합니다.
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
            // 현재 위치를 MapPin에 반영합니다.
            mapPin.Location = new LatLon(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
    }
}
