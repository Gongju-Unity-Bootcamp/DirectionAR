using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class DirectionManager : MonoBehaviour
{
    private const string apiUrl = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";

    private List<Vector3> pathPoints = new List<Vector3>();

    public void Init()
    {
    }

    public IEnumerator GetDirections(string startCoord, string goalCoord)
    {
        string requestUrl = $"{apiUrl}?start={startCoord}&goal={goalCoord}";

        UnityWebRequest request = UnityWebRequest.Get(requestUrl);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", Managers.Android.clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", Managers.Android.secretKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Managers.Android.ShowAndroidToastMessage("네이버 지도 API 요청 실패: " + request.error);
        }
        else
        {
            // 응답 받은 JSON 데이터 처리
            string jsonResponse = request.downloadHandler.text;

            ParseResponse(jsonResponse);
        }
    }

    void ParseResponse(string jsonResponse)
    {
        // 정규식을 사용하여 JSON 데이터에서 경로 좌표 추출
        MatchCollection pathMatches = Regex.Matches(jsonResponse, "\"path\":\\[([\\d\\.,\\[\\]]+)\\]");
        foreach (Match match in pathMatches)
        {
            string pathData = match.Groups[1].Value;
            string[] pathArray = pathData.Split(new char[] { '[', ']', ',' }, StringSplitOptions.RemoveEmptyEntries);

            // 경로 좌표를 Vector3 형태로 변환하여 리스트에 추가
            for (int i = 0; i < pathArray.Length; i += 2)
            {
                float x = float.Parse(pathArray[i]);
                float y = float.Parse(pathArray[i + 1]);
                float z = 0;
                Vector3 point = new Vector3(x, y, z);
                pathPoints.Add(point);
            }
        }
    }
}
