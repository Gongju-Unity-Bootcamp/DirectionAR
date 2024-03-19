using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARMenuManager : MonoBehaviour
{
    private static ARMenuManager _instance;
    public static ARMenuManager Instance => _instance ??= _instance = new ARMenuManager();
    GameObject _arMenuManager;

    public List<Item> items = new List<Item>();
    public Transform _content;
    public GameObject _item;
    public static int _currentId = default;

    public void Init()
    {
        _arMenuManager = GameObject.Find("@ARMenuManager");

        if (_arMenuManager == null)
        {
            _arMenuManager = new GameObject { name = "@ARMenuManager" };
            UnityEngine.Object.DontDestroyOnLoad(_arMenuManager);
        }

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

            itemId.id = item.id;
            itemTitle.text = item.title;
            itemInfo.text = item.info;
            itemCoverImage.sprite = item.coverImage;
        }
    }
}