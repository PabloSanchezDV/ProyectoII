using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Fields
    [SerializeField] private float _daytime;
    [SerializeField] private float _daytimeModifier;
    [SerializeField] private int _startHour;
    [SerializeField] private int _startMinute;
    [SerializeField] private int _endingHour;
    [SerializeField] private int _endingMinute;
    [SerializeField] private List<PinCT> _pinsList;

    private bool _dayEnded = false;

    #region Pictures Taken 

    #region Mammoth
    [SerializeField] private bool _mammothGlobalPictureTaken;
    [SerializeField] private bool _mammothEatPictureTaken;
    [SerializeField] private bool _mammothSleepPictureTaken;
    [SerializeField] private bool _mammothHeadbuttPictureTaken;
    [SerializeField] private bool _mammothShakePictureTaken;
    #endregion

    #region Elk
    [SerializeField] private bool _elkGlobalPictureTaken;
    [SerializeField] private bool _elkEatPictureTaken;
    [SerializeField] private bool _elkShakePictureTaken;
    [SerializeField] private bool _elkGrowlPictureTaken;
    [SerializeField] private bool _elkShowOffPictureTaken;
    #endregion

    #region Ornito
    [SerializeField] private bool _ornitoGlobalPictureTaken;
    [SerializeField] private bool _ornitoEatPictureTaken;
    [SerializeField] private bool _ornitoSwimPictureTaken;
    [SerializeField] private bool _ornitoSunbathingPictureTaken;
    [SerializeField] private bool _ornitoProtectPictureTaken;
    #endregion

    #region Plants
    [SerializeField] private bool _chestnutTreePictureTaken;
    [SerializeField] private bool _birchTreePictureTaken;
    [SerializeField] private bool _heatherPictureTaken;
    [SerializeField] private bool _gorsePictureTaken;
    [SerializeField] private bool _hollyPictureTaken;

    [SerializeField] private bool _rushPictureTaken;
    [SerializeField] private bool _dandelionPictureTaken;
    [SerializeField] private bool _cloverPictureTaken;
    [SerializeField] private bool _sprucePictureTaken;
    [SerializeField] private bool _cypressPictureTaken;

    [SerializeField] private bool _fernPictureTaken;
    [SerializeField] private bool _redChestnutTreePictureTaken;
    [SerializeField] private bool _beechTreePictureTaken;
    [SerializeField] private bool _willowTreePictureTaken;
    #endregion

    #region Fungus
    [SerializeField] private bool _fairyRingPictureTaken;
    [SerializeField] private bool _amanitaPictureTaken;
    [SerializeField] private bool _parasolPictureTaken;
    [SerializeField] private bool _goldenChanterellePictureTaken;
    [SerializeField] private bool _boletusPictureTaken;
    #endregion

    #region Bugs
    [SerializeField] private bool _moonBeatlePictureTaken;
    [SerializeField] private bool _fireflyPictureTaken;
    [SerializeField] private bool _butterflyPictureTaken;
    [SerializeField] private bool _dragonflyPictureTaken;
    [SerializeField] private bool _beePictureTaken;
    #endregion

    [SerializeField] private bool[] _galleryPicturesTaken;

    #endregion

    #endregion

    #region Properties
    public float Daytime { get { return _daytime; } set { _daytime = value; } }
    public float StartHour { get { return _startHour; } }
    public float StartMinute { get { return _startMinute; } }
    public bool HasLoadedData { get { return ScenesController.Instance.DoesLoad; } }

    public List<PinCT> PinsList { get { return _pinsList; } set { _pinsList = value; } }

    #region Pictures Taken

    #region Mammoth
    public bool MammothGlobalPictureTaken { get { return _mammothGlobalPictureTaken; } }
    public bool MammothEatPictureTaken { get { return _mammothEatPictureTaken; } }
    public bool MammothSleepPictureTaken { get { return _mammothSleepPictureTaken; } }
    public bool MammothHeadbuttPictureTaken { get { return _mammothHeadbuttPictureTaken; } }
    public bool MammothShakePictureTaken { get { return _mammothShakePictureTaken; } }
    #endregion

    #region Elk
    public bool ElkGlobalPictureTaken { get { return _elkGlobalPictureTaken; } }
    public bool ElkEatPictureTaken { get { return _elkEatPictureTaken; } }
    public bool ElkShakePictureTaken { get { return _elkShakePictureTaken; } }
    public bool ElkGrowlPictureTaken { get { return _elkGrowlPictureTaken; } }
    public bool ElkShowOffPictureTaken { get { return _elkShowOffPictureTaken; } }
    #endregion

    #region Ornito
    public bool OrnitoGlobalPictureTaken { get { return _ornitoGlobalPictureTaken; } }
    public bool OrnitoEatPictureTaken { get { return _ornitoEatPictureTaken; } }
    public bool OrnitoSwimPictureTaken { get { return _ornitoSwimPictureTaken; } }
    public bool OrnitoSunbathingPictureTaken { get { return _ornitoSunbathingPictureTaken; } }
    public bool OrnitoProtectPictureTaken { get { return _ornitoProtectPictureTaken; } }
    #endregion

    #region Plants
    public bool ChestnutTreePictureTaken { get { return _chestnutTreePictureTaken; } }
    public bool BirchTreePictureTaken { get { return _birchTreePictureTaken; } }
    public bool HeatherPictureTaken { get { return _heatherPictureTaken; } }
    public bool GorsePictureTaken { get { return _gorsePictureTaken; } }
    public bool HollyPictureTaken { get { return _hollyPictureTaken; } }

    public bool RushPictureTaken { get { return _rushPictureTaken; } }
    public bool DandelionPictureTaken { get { return _dandelionPictureTaken; } }
    public bool CloverPictureTaken { get { return _cloverPictureTaken; } }
    public bool SprucePictureTaken { get { return _sprucePictureTaken; } }
    public bool CypressPictureTaken { get { return _cypressPictureTaken; } }

    public bool FernPictureTaken { get { return _fernPictureTaken; } }
    public bool RedChestnutTreePictureTaken { get { return _redChestnutTreePictureTaken; } }
    public bool BeechTreePictureTaken { get { return _beechTreePictureTaken; } }
    public bool WillowTreePictureTaken { get { return _willowTreePictureTaken; } }
    #endregion

    #region Fungus
    public bool FairyRingPictureTaken { get { return _fairyRingPictureTaken; } }
    public bool AmanitaPictureTaken { get { return _amanitaPictureTaken; } }
    public bool ParasolPictureTaken { get { return _parasolPictureTaken; } }
    public bool GoldenChanterellePictureTaken { get { return _goldenChanterellePictureTaken; } }
    public bool BoletusPictureTaken { get { return _boletusPictureTaken; } }
    #endregion

    #region Bugs
    public bool MoonBeatlePictureTaken { get { return _moonBeatlePictureTaken; } }
    public bool FireflyPictureTaken { get { return _fireflyPictureTaken; } }
    public bool ButterflyPictureTaken { get { return _butterflyPictureTaken; } }
    public bool DragonflyPictureTaken { get { return _dragonflyPictureTaken; } }
    public bool BeePictureTaken { get { return _beePictureTaken; } }
    #endregion

    public bool[] GalleryPicturesTaken { get { return _galleryPicturesTaken; } }

    #endregion

    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        _daytime = _startHour * 60 + _startMinute;
        _galleryPicturesTaken = new bool[64];

        if (ScenesController.Instance.DoesLoad)
            SaveSystem.LoadGameData(true);
    }

    void Update()
    {
        _daytime += _daytimeModifier * Time.deltaTime;

        if (!_dayEnded)
        {
            if (_daytime >= _endingHour * 60 + _endingMinute)
            {
                UIManager.Instance.TriggerFadeOut();
                _dayEnded = true;
            }
        }
    }

    public void EndDay()
    {
        DebugManager.Instance.DebugMessage("Day ended at " + _daytime + "s");
        SaveSystem.SaveGameData();
        ScenesController.Instance.EndDay();
    }

    public float GetProgress()
    {
        int picturesTaken = 0;

        if (_mammothGlobalPictureTaken)
            picturesTaken++;
        if (_mammothEatPictureTaken)
            picturesTaken++;
        if (_mammothSleepPictureTaken)
            picturesTaken++;
        if (_mammothHeadbuttPictureTaken)
            picturesTaken++;
        if (_mammothShakePictureTaken)
            picturesTaken++;

        if (_elkGlobalPictureTaken)
            picturesTaken++;
        if (_elkEatPictureTaken)
            picturesTaken++;
        if (_elkShakePictureTaken)
            picturesTaken++;
        if (_elkGrowlPictureTaken)
            picturesTaken++;
        if (_elkShowOffPictureTaken)
            picturesTaken++;

        if (_ornitoGlobalPictureTaken)
            picturesTaken++;
        if (_ornitoEatPictureTaken)
            picturesTaken++;
        if (_ornitoSwimPictureTaken)
            picturesTaken++;
        if (_ornitoSunbathingPictureTaken)
            picturesTaken++;
        if (_ornitoProtectPictureTaken)
            picturesTaken++;

        if (_chestnutTreePictureTaken)
            picturesTaken++;
        if (_birchTreePictureTaken)
            picturesTaken++;
        if (_heatherPictureTaken)
            picturesTaken++;
        if (_gorsePictureTaken)
            picturesTaken++;
        if (_hollyPictureTaken)
            picturesTaken++;
        if (_rushPictureTaken)
            picturesTaken++;
        if (_dandelionPictureTaken)
            picturesTaken++;
        if (_cloverPictureTaken)
            picturesTaken++;
        if (_sprucePictureTaken)
            picturesTaken++;
        if (_cypressPictureTaken)
            picturesTaken++;
        if (_fernPictureTaken)
            picturesTaken++;
        if (_redChestnutTreePictureTaken)
            picturesTaken++;
        if (_beechTreePictureTaken)
            picturesTaken++;
        if (_willowTreePictureTaken)
            picturesTaken++;

        if (_fairyRingPictureTaken)
            picturesTaken++;
        if (_amanitaPictureTaken)
            picturesTaken++;
        if (_parasolPictureTaken)
            picturesTaken++;
        if (_goldenChanterellePictureTaken)
            picturesTaken++;
        if (_boletusPictureTaken)
            picturesTaken++;

        if (_moonBeatlePictureTaken)
            picturesTaken++;
        if (_fireflyPictureTaken)
            picturesTaken++;
        if (_butterflyPictureTaken)
            picturesTaken++;
        if (_dragonflyPictureTaken)
            picturesTaken++;
        if (_beePictureTaken)
            picturesTaken++;

        return (float)picturesTaken / 40 * 100;
    }

    public void SetPictureTaken(Target target, int galleryIndex = 0)
    {
        switch (target)
        {
            case Target.MammothGlobal:
                _mammothGlobalPictureTaken = true;
                break;
            case Target.MammothEat:
                _mammothEatPictureTaken = true;
                break;
            case Target.MammothSleep:
                _mammothSleepPictureTaken = true;
                break;
            case Target.MammothHeadbutt:
                _mammothHeadbuttPictureTaken = true;
                break;
            case Target.MammothShake:
                _mammothShakePictureTaken = true;
                break;

            case Target.ElkGlobal:
                _elkGlobalPictureTaken = true;
                break;
            case Target.ElkEat:
                _elkEatPictureTaken = true;
                break;
            case Target.ElkShake:
                _elkShakePictureTaken = true;
                break;
            case Target.ElkGrowl:
                _elkGrowlPictureTaken = true;
                break;
            case Target.ElkShowOff:
                _elkShowOffPictureTaken = true;
                break;

            case Target.OrnitoGlobal:
                _ornitoGlobalPictureTaken = true;
                break;
            case Target.OrnitoEat:
                _ornitoEatPictureTaken = true;
                break;
            case Target.OrnitoSwim:
                _ornitoSwimPictureTaken = true;
                break;
            case Target.OrnitoSunbathing:
                _ornitoSunbathingPictureTaken = true;
                break;
            case Target.OrnitoProtect:
                _ornitoProtectPictureTaken = true;
                break;

            case Target.ChestnutTree:
                _chestnutTreePictureTaken = true;
                break;
            case Target.BirchTree:
                _birchTreePictureTaken = true;
                break;
            case Target.Heather:
                _heatherPictureTaken = true;
                break;
            case Target.Gorse:
                _gorsePictureTaken = true;
                break;
            case Target.Holly:
                _hollyPictureTaken = true;
                break;

            case Target.Rush:
                _rushPictureTaken = true;
                break;
            case Target.Dandelion:
                _dandelionPictureTaken = true;
                break;
            case Target.Clover:
                _cloverPictureTaken = true;
                break;
            case Target.Spruce:
                _sprucePictureTaken = true;
                break;
            case Target.Cypress:
                _cypressPictureTaken = true;
                break;

            case Target.Fern:
                _fernPictureTaken = true;
                break;
            case Target.RedChestnutTree:
                _redChestnutTreePictureTaken = true;
                break;
            case Target.BeechTree:
                _beechTreePictureTaken = true;
                break;
            case Target.WillowTree:
                _willowTreePictureTaken = true;
                break;

            case Target.FairyRing:
                _fairyRingPictureTaken = true;
                break;
            case Target.Amanita:
                _amanitaPictureTaken = true;
                break;
            case Target.Parasol:
                _parasolPictureTaken = true;
                break;
            case Target.GoldenChanterelle:
                _goldenChanterellePictureTaken = true;
                break;
            case Target.Boletus:
                _boletusPictureTaken = true;
                break;

            case Target.MoonBeatle:
                _moonBeatlePictureTaken = true;
                break;
            case Target.Firefly:
                _fireflyPictureTaken = true;
                break;
            case Target.Butterfly:
                _butterflyPictureTaken = true;
                break;
            case Target.Dragonfly:
                _dragonflyPictureTaken = true;
                break;
            case Target.Bee:
                _beePictureTaken = true;
                break;
            case Target.None:
                _galleryPicturesTaken[galleryIndex] = true;
                break;
            default:
                throw new System.Exception($"Unable to set the value for {target.ToString()} in GameManager. The picture won't be seen in the notebook.");
        }
    }

    public bool IsPictureTaken(Target target)
    {
        switch (target)
        {
            case Target.MammothGlobal:
                return _mammothGlobalPictureTaken;
            case Target.MammothEat:
                return _mammothEatPictureTaken;
            case Target.MammothSleep:
                return _mammothSleepPictureTaken;
            case Target.MammothHeadbutt:
                return _mammothHeadbuttPictureTaken;
            case Target.MammothShake:
                return _mammothShakePictureTaken;

            case Target.ElkGlobal:
                return _elkGlobalPictureTaken;
            case Target.ElkEat:
                return _elkEatPictureTaken;
            case Target.ElkShake:
                return _elkShakePictureTaken;
            case Target.ElkGrowl:
                return _elkGrowlPictureTaken;
            case Target.ElkShowOff:
                return _elkShowOffPictureTaken;

            case Target.OrnitoGlobal:
                return _ornitoGlobalPictureTaken;
            case Target.OrnitoEat:
                return _ornitoEatPictureTaken;
            case Target.OrnitoSwim:
                return _ornitoSwimPictureTaken;
            case Target.OrnitoSunbathing:
                return _ornitoSunbathingPictureTaken;
            case Target.OrnitoProtect:
                return _ornitoProtectPictureTaken;

            case Target.ChestnutTree:
                return _chestnutTreePictureTaken;
            case Target.BirchTree:
                return _birchTreePictureTaken;
            case Target.Heather:
                return _heatherPictureTaken;
            case Target.Gorse:
                return _gorsePictureTaken;
            case Target.Holly:
                return _hollyPictureTaken;

            case Target.Rush:
                return _rushPictureTaken;
            case Target.Dandelion:
                return _dandelionPictureTaken;
            case Target.Clover:
                return _cloverPictureTaken;
            case Target.Spruce:
                return _sprucePictureTaken;
            case Target.Cypress:
                return _cypressPictureTaken;

            case Target.Fern:
                return _fernPictureTaken;
            case Target.RedChestnutTree:
                return _redChestnutTreePictureTaken;
            case Target.BeechTree:
                return _beechTreePictureTaken;
            case Target.WillowTree:
                return _willowTreePictureTaken;

            case Target.FairyRing:
                return _fairyRingPictureTaken;
            case Target.Amanita:
                return _amanitaPictureTaken;
            case Target.Parasol:
                return _parasolPictureTaken;
            case Target.GoldenChanterelle:
                return _goldenChanterellePictureTaken;
            case Target.Boletus:
                return _boletusPictureTaken;

            case Target.MoonBeatle:
                return _moonBeatlePictureTaken;
            case Target.Firefly:
                return _fireflyPictureTaken;
            case Target.Butterfly:
                return _butterflyPictureTaken;
            case Target.Dragonfly:
                return _dragonflyPictureTaken;
            case Target.Bee:
                return _beePictureTaken;

            default:
                throw new System.Exception($"Unable to get the value for {target.ToString()} in GameManager.");
        }
    }
}
