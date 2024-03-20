using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Camera renderCamera; // �������� ī�޶�
    public RawImage uiImage; // ������ ����� ǥ���� UI Raw Image

    // Update is called once per frame
    void Update()
    {
        uiImage.texture = RenderCameraToTexture();
    }

    Texture RenderCameraToTexture()
    {
        // �������� �ؽ�ó ����
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // �������� ī�޶� ����
        renderCamera.targetTexture = renderTexture;
        renderCamera.Render();
        // �������� �ؽ�ó ��ȯ
        return renderTexture;
    }
}
