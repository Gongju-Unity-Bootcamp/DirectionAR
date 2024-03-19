using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ARPickButton : MonoBehaviour
{
    public Item item;

    public void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickARZoneButton);
    }

    void OnClickARZoneButton()
    {
        Managers.ARMenu._currentItem = item;

        Managers.UI.ShowPopupUI<UI_Popup>("ARZoneInfo");
    }
}
