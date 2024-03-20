using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Camera renderCamera; // 렌더링할 카메라
    public RawImage uiImage; // 렌더링 결과를 표시할 UI Raw Image

    // Update is called once per frame
    void Update()
    {
        uiImage.texture = RenderCameraToTexture();
    }

    Texture RenderCameraToTexture()
    {
        // 렌더링할 텍스처 생성
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // 렌더링할 카메라 설정
        renderCamera.targetTexture = renderTexture;
        renderCamera.Render();
        // 렌더링된 텍스처 반환
        return renderTexture;
    }
}
