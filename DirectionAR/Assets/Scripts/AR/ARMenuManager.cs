using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARMenuManager : MonoBehaviour
{
    public static ARMenuManager _instance { get; private set; }

    public List<Item> items = new List<Item>();
    public Transform _content;
    public GameObject _item;

    public Item _currentItem = null;

    public void Init()
    {
        if (_item == null)
        {
            _item = Resources.Load<GameObject>("Prefabs/ARZoneButton");
        }
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in _content)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            GameObject obj = Managers.Resource.Instantiate(_item, _content);
            var itemId = obj.transform.GetComponent<UI_ARPickButton>();
            var itemTitle = obj.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            var itemInfo = obj.transform.Find("Info").GetComponent<TextMeshProUGUI>();
            var itemCoverImage = obj.transform.Find("CoverImage").GetComponent<Image>();

            itemId.item = item;
            itemTitle.text = item.title;
            itemInfo.text = item.info;
            itemCoverImage.sprite = item.coverImage;
        }
    }
}