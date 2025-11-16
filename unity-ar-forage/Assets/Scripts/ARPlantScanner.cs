using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlantScanner : MonoBehaviour {
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject plantPrefab;

    private void OnEnable() { trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; }
    private void OnDisable() { trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach (var newImage in eventArgs.added) {
            if (newImage.referenceImage.name.Contains("Dandelion")) {
                Instantiate(plantPrefab, newImage.transform.position, newImage.transform.rotation);
                FindObjectOfType<ForageGameManager>()?.OnPlantScanned(newImage);
            }
        }
    }
}