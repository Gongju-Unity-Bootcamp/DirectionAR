using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARMarkerController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabs;

    private ARTrackedImageManager _arTrackedImageManager;
    private Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();


    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        
        foreach(GameObject prefab in _prefabs)
        {
            GameObject clone = Instantiate(prefab);
            Prefabs.Add(prefab.name, clone);
            clone.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs _eventArgs)
    {
        foreach(var trackedImage in _eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach(var trackedImage in _eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach(var trackedImage in _eventArgs.removed)
        {
            Prefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage _trackedImage)
    {
        GameObject trackedPrefabs = Prefabs[_trackedImage.referenceImage.name];

        if(_trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedPrefabs.transform.position = _trackedImage.transform.position;

            switch (_trackedImage.referenceImage.name)
            {
                case "PhotoZone(1)":
                    trackedPrefabs.transform.rotation = _trackedImage.transform.rotation * Quaternion.Euler(45, 0, 0);
                    break;
                case "PhotoZone(2)":
                    trackedPrefabs.transform.rotation = _trackedImage.transform.rotation * Quaternion.Euler(45, 90, 90);
                    break;
                case "PhotoZone(5)":
                    trackedPrefabs.transform.rotation = _trackedImage.transform.rotation * Quaternion.Euler(-90, -90, -90);
                    break;
                default:
                    trackedPrefabs.transform.rotation = _trackedImage.transform.rotation;
                    break;
            }

            trackedPrefabs.SetActive(true);
        }
        else
        {
            trackedPrefabs.SetActive(false);
        }
    }
}
