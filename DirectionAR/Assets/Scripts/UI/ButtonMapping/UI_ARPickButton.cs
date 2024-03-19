using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ARPickButton : MonoBehaviour
{
    public int id;

    public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickARZoneButton);
    }

    void OnClickARZoneButton()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("ARZoneInfo");
    }
}
