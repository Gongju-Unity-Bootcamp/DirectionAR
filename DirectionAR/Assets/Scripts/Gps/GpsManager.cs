using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;


public class GpsManager : MonoBehaviour
{

    public TextMeshProUGUI[] data = new TextMeshProUGUI[4];
    public float delay;
    public float maxtime = 5f;

    private int frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Gps_manager());
    }
    IEnumerator Gps_manager()
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation); 
            while(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        if(Input.location.isEnabledByUser)
        {
            data[3].text = "Gps 장치가 꺼져있음.";
        }

        Input.location.Start();

        while(Input.location.status == LocationServiceStatus.Initializing && delay < maxtime) 
        {
            yield return new WaitForSeconds(1.0f);
            delay++;
        }

        if(Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
        {
            data[3].text = "위치정보를 가져오는데 실패함.";
        }

        if ( delay >= maxtime ) 
        {
            data[3].text = "지연시간 초과.";
        }

        data[0].text = "위도 :" + Input.location.lastData.latitude.ToString();
        data[1].text = "경도 :" + Input.location.lastData.longitude.ToString();
        data[2].text = "고도 :" + Input.location.lastData.altitude.ToString();
        data[3].text = "위치 정보 수신완료";
        yield return new WaitForSeconds(5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (frame >= 100)
        {
            StartCoroutine(Gps_manager());
            frame = 0;
        }
        frame++;
    }
}
