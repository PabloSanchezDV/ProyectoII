using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager Instance;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _pictureCullingMask;
    [SerializeField] private LayerMask _baseCullingMask;

    private Texture2D _lastPictureTexture2D;
    private Sprite _lastPictureSprite;
    private Target _screenshotTarget = Target.None;

    public Sprite LastPictureSprite { get { return _lastPictureSprite; } }
    public Target ScreenshotTarget { get { return _screenshotTarget; } set { _screenshotTarget = value; } }

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

        EventHolder.Instance.onPhotoObjectsDetected.AddListener(TakeScreenshot);
    }

    private void OnDisable()
    {
        EventHolder.Instance.onPhotoObjectsDetected.RemoveListener(TakeScreenshot);
    }

    private void TakeScreenshot()
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

        _lastPictureTexture2D = new Texture2D(Screen.width, Screen.height);
        _lastPictureTexture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _lastPictureTexture2D.Apply();
        RenderTexture.active = null;
        cam.targetTexture = null;

        _lastPictureSprite = Texture2DToSprite(_lastPictureTexture2D);
        
        if(_screenshotTarget != Target.None)
        {
            SaveScreenshot(_lastPictureTexture2D, _screenshotTarget.ToString());
            if (!GameManager.Instance.IsPictureTaken(_screenshotTarget))
            {
                UIManager.Instance.TriggerNotification();
                AudioManager.Instance.PlayNewNotebookEntrySound(cam.gameObject);
                GameManager.Instance.SetPictureTaken(_screenshotTarget);
            }
        }

        EventHolder.Instance.onScreenshotTaken?.Invoke();
    }

    private void SaveScreenshot(Texture2D texture2D, string screenshotName)
    {
        SaveSystem.SaveScreenshot(texture2D, screenshotName);
    }

    public Sprite LoadScreenshot(Target target)
    {
        return Texture2DToSprite(SaveSystem.LoadScreenshot(target.ToString()));
    }

    public Sprite LoadScreenshot(int galleryIndex)
    {
        return Texture2DToSprite(SaveSystem.LoadScreenshot("GalleryPicture" + galleryIndex.ToString()));
    }

    public void SaveGalleryScreenshot(int galleryIndex)
    {
        SaveScreenshot(_lastPictureTexture2D, "GalleryPicture" + galleryIndex.ToString());
    }

    private Sprite Texture2DToSprite(Texture2D texture2D)
    {
        Rect rect = new Rect(0, 0, texture2D.width, texture2D.height);
        return Sprite.Create(texture2D, rect, Vector2.one * 0.5f, 100);
    }
}
