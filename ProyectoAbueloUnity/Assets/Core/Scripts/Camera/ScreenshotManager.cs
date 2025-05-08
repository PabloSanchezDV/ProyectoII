using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager Instance;

    [SerializeField] private bool _savePictures;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _pictureCullingMask;

    private Sprite _lastPictureSprite;
    [SerializeField] private LayerMask _baseCullingMask;

    public Sprite LastPictureSprite { get { return _lastPictureSprite; } }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        _baseCullingMask = _mainCamera.cullingMask;

        EventHolder.Instance.onPictureTaken.AddListener(SaveScreenshot);
    }

    private void SaveScreenshot()
    {
        _mainCamera.cullingMask = _pictureCullingMask;
        TakeScreenshot(_mainCamera);
        _mainCamera.enabled = false;
        _mainCamera.cullingMask = _baseCullingMask;
        _mainCamera.enabled = true;
    }

    private void TakeScreenshot(Camera cam)
    {
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        cam.Render();

        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        renderedTexture.Apply();
        RenderTexture.active = null;
        cam.targetTexture = null;

        _lastPictureSprite = Texture2DToSprite(renderedTexture);
        
        if(_savePictures)
            SaveScreenshot(renderedTexture);
    }

    private void SaveScreenshot(Texture2D texture2D)
    {
        byte[] byteArray = texture2D.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + $"/Pictures/{GetScreenshotName()}", byteArray);
        Debug.Log($"Screenshot cameracapture-{GetScreenshotName()}.png saved at " + Application.dataPath + "\"/Pictures");
    }

    private Sprite Texture2DToSprite(Texture2D texture2D)
    {
        Rect rect = new Rect(0, 0, texture2D.width, texture2D.height);
        return Sprite.Create(texture2D, rect, Vector2.one * 0.5f, 100);
    }

    private string GetScreenshotName()
    {
        return $"cameracapture-{System.DateTime.Now.Year}-{System.DateTime.Now.Month}-{System.DateTime.Now.Day}-{System.DateTime.Now.Hour}-{System.DateTime.Now.Minute}-{System.DateTime.Now.Second}.png";
    }
}
