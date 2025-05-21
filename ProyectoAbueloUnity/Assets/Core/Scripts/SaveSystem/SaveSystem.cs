using System;
using System.IO;
using Palmmedia.ReportGenerator.Core.Common;
using UnityEngine;

public static class SaveSystem
{
    public static int GetCurrentFaseSceneIndex()
    {
        SaveData loadedData = LoadGameData(false);
        return loadedData.faseSceneIndex;
    }

    #region Save / Load Game
    public static void SaveGameData()
    {
        SaveData saveData = new SaveData();
        SaveFile(SaveDataToJSON(saveData), GetSaveDataPath());
        DebugManager.Instance.DebugGlobalSystemMessage("Game saved at " + GetSaveDataPath());
    }

    public static SaveData LoadGameData(bool applyDataToCurrentScene)
    {
        string loadedString = LoadFile(GetSaveDataPath());
        SaveData loadedData = JSONToSaveData(loadedString);
        if(applyDataToCurrentScene)
            loadedData.Apply();
        return loadedData;
    }

    private static void SaveFile(string dataString, string dataPath)
    {
        StreamWriter streamWriter = new StreamWriter(dataPath);
        streamWriter.Write(dataString);
        streamWriter.Close();
    }

    private static string LoadFile(string dataPath)
    {
        if (File.Exists(dataPath))
        {
            StreamReader streamReader = new StreamReader(dataPath);
            return streamReader.ReadToEnd();
        }
        else
            throw new UnityException("Save file not found at path " + dataPath);
    }

    private static string SaveDataToJSON(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        return json;
    }

    private static SaveData JSONToSaveData(string json)
    {
        return JsonUtility.FromJson<SaveData>(json);
    }

    private static string GetSaveDataPath()
    {
        return Application.persistentDataPath + "/sd.json";
    }
    #endregion

    #region Save / Load Screenshot
    public static void SaveScreenshot(Texture2D texture2D, string screenshotName)
    {
        byte[] byteArray = texture2D.EncodeToPNG();
        File.WriteAllBytes(GetScreenshotDataPath(screenshotName), byteArray);
        DebugManager.Instance.DebugGlobalSystemMessage($"Screenshot saved at " + GetScreenshotDataPath(screenshotName));
    }

    public static Texture2D LoadScreenshot(string screenshotName)
    {
        if (File.Exists(GetScreenshotDataPath(screenshotName)))
        {
            byte[] byteArray = File.ReadAllBytes(GetScreenshotDataPath(screenshotName));
            Texture2D texture2D = new Texture2D(1920, 1080);
            bool hasLoaded = texture2D.LoadImage(byteArray);
            if(hasLoaded)
                return texture2D;
            else
                throw new Exception("Unable to load screenshot at " +  screenshotName);
        }
        else
            throw new UnityException($"Screenshot {screenshotName} not found at path " + GetScreenshotDataPath(screenshotName));
    }

    private static string GetScreenshotDataPath(string screenshotName)
    {
#if UNITY_EDITOR
        return Application.dataPath + $"/Pictures/cameracapture-{screenshotName}.png";
#else
        return Application.dataPath + $"/cameracapture-{screenshotName}.png";
#endif
    }
    #endregion

    [Serializable]
    public class SaveData
    {
        public int faseSceneIndex;

        public bool mammothGlobalPictureTaken;
        public bool mammothEatPictureTaken;
        public bool mammothSleepPictureTaken;
        public bool mammothHeadbuttPictureTaken;
        public bool mammothShakePictureTaken;

        public bool elkGlobalPictureTaken;
        public bool elkEatPictureTaken;
        public bool elkShakePictureTaken;
        public bool elkGrowlPictureTaken;
        public bool elkShowOffPictureTaken;

        public bool ornitoGlobalPictureTaken;
        public bool ornitoEatPictureTaken;
        public bool ornitoSwimPictureTaken;
        public bool ornitoSunbathingPictureTaken;
        public bool ornitoProtectPictureTaken;

        public bool chestnutTreePictureTaken;
        public bool birchTreePictureTaken;
        public bool heatherPictureTaken;
        public bool gorsePictureTaken;
        public bool hollyPictureTaken;

        public bool rushPictureTaken;
        public bool dandelionPictureTaken;
        public bool cloverPictureTaken;
        public bool sprucePictureTaken;
        public bool cypressPictureTaken;

        public bool fernPictureTaken;
        public bool redChestnutTreePictureTaken;
        public bool beechTreePictureTaken;
        public bool willowTreePictureTaken;

        public bool fairyRingPictureTaken;
        public bool amanitaPictureTaken;
        public bool parasolPictureTaken;
        public bool goldenChanterellePictureTaken;
        public bool boletusPictureTaken;

        public bool moonBeatlePictureTaken;
        public bool fireflyPictureTaken;
        public bool butterflyPictureTaken;
        public bool dragonflyPictureTaken;
        public bool beePictureTaken;

        public bool[] galleryPicturesTaken;

        public SaveData()
        {
            faseSceneIndex = ScenesController.Instance.FaseSceneIndex;

            mammothGlobalPictureTaken = GameManager.Instance.MammothGlobalPictureTaken;
            mammothEatPictureTaken = GameManager.Instance.MammothEatPictureTaken;
            mammothSleepPictureTaken = GameManager.Instance.MammothSleepPictureTaken;
            mammothHeadbuttPictureTaken = GameManager.Instance.MammothHeadbuttPictureTaken;
            mammothShakePictureTaken = GameManager.Instance.MammothShakePictureTaken;

            elkGlobalPictureTaken = GameManager.Instance.ElkGlobalPictureTaken;
            elkEatPictureTaken = GameManager.Instance.ElkEatPictureTaken;
            elkShakePictureTaken = GameManager.Instance.ElkShakePictureTaken;
            elkGrowlPictureTaken = GameManager.Instance.ElkGrowlPictureTaken;
            elkShowOffPictureTaken = GameManager.Instance.ElkShowOffPictureTaken;

            ornitoGlobalPictureTaken = GameManager.Instance.OrnitoGlobalPictureTaken;
            ornitoEatPictureTaken = GameManager.Instance.OrnitoEatPictureTaken;
            ornitoSwimPictureTaken = GameManager.Instance.OrnitoSwimPictureTaken;
            ornitoSunbathingPictureTaken = GameManager.Instance.OrnitoSunbathingPictureTaken;
            ornitoProtectPictureTaken = GameManager.Instance.OrnitoProtectPictureTaken;

            chestnutTreePictureTaken = GameManager.Instance.ChestnutTreePictureTaken;
            birchTreePictureTaken = GameManager.Instance.BirchTreePictureTaken;
            heatherPictureTaken = GameManager.Instance.HeatherPictureTaken;
            gorsePictureTaken = GameManager.Instance.GorsePictureTaken;
            hollyPictureTaken = GameManager.Instance.HollyPictureTaken;

            rushPictureTaken = GameManager.Instance.RushPictureTaken;
            dandelionPictureTaken = GameManager.Instance.DandelionPictureTaken;
            cloverPictureTaken = GameManager.Instance.CloverPictureTaken;
            sprucePictureTaken = GameManager.Instance.SprucePictureTaken;
            cypressPictureTaken = GameManager.Instance.CypressPictureTaken;

            fernPictureTaken = GameManager.Instance.FernPictureTaken;
            redChestnutTreePictureTaken = GameManager.Instance.RedChestnutTreePictureTaken;
            beechTreePictureTaken = GameManager.Instance.BeechTreePictureTaken;
            willowTreePictureTaken = GameManager.Instance.WillowTreePictureTaken;

            fairyRingPictureTaken = GameManager.Instance.FairyRingPictureTaken;
            amanitaPictureTaken = GameManager.Instance.AmanitaPictureTaken;
            parasolPictureTaken = GameManager.Instance.ParasolPictureTaken;
            goldenChanterellePictureTaken = GameManager.Instance.GoldenChanterellePictureTaken;
            boletusPictureTaken = GameManager.Instance.BoletusPictureTaken;

            moonBeatlePictureTaken = GameManager.Instance.MoonBeatlePictureTaken;
            fireflyPictureTaken = GameManager.Instance.FireflyPictureTaken;
            butterflyPictureTaken = GameManager.Instance.ButterflyPictureTaken;
            dragonflyPictureTaken = GameManager.Instance.DragonflyPictureTaken;
            beePictureTaken = GameManager.Instance.BeePictureTaken;

            var sourceArray = GameManager.Instance.GalleryPicturesTaken;
            galleryPicturesTaken = sourceArray != null ? (bool[])sourceArray.Clone() : new bool[0];
        }

        public void Apply()
        {
            if (mammothGlobalPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.MammothGlobal);
            if (mammothEatPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.MammothEat);
            if (mammothSleepPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.MammothSleep);
            if (mammothHeadbuttPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.MammothHeadbutt);
            if (mammothShakePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.MammothShake);

            if (elkGlobalPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.ElkGlobal);
            if (elkEatPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.ElkEat);
            if (elkShakePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.ElkShake);
            if (elkGrowlPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.ElkGrowl);
            if (elkShowOffPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.ElkShowOff);

            if (ornitoGlobalPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.OrnitoGlobal);
            if (ornitoEatPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.OrnitoEat);
            if (ornitoSwimPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.OrnitoSwim);
            if (ornitoSunbathingPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.OrnitoSunbathing);
            if (ornitoProtectPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.OrnitoProtect);

            if (chestnutTreePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.ChestnutTree);
            if (birchTreePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.BirchTree);
            if (heatherPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Heather);
            if (gorsePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Gorse);
            if (hollyPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Holly);

            if (rushPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Rush);
            if (dandelionPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Dandelion);
            if (cloverPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Clover);
            if (sprucePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Spruce);
            if (cypressPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Cypress);

            if (fernPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Fern);
            if (redChestnutTreePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.RedChestnutTree);
            if (beechTreePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.BeechTree);
            if (willowTreePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.WillowTree);

            if (fairyRingPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.FairyRing);
            if (amanitaPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Amanita);
            if (parasolPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Parasol);
            if (goldenChanterellePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.GoldenChanterelle);
            if (boletusPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Boletus);

            if (moonBeatlePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.MoonBeatle);
            if (fireflyPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Firefly);
            if (butterflyPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Butterfly);
            if (dragonflyPictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Dragonfly);
            if (beePictureTaken) 
                GameManager.Instance.SetPictureTaken(Target.Bee);

            if (galleryPicturesTaken != null)
            {
                for (int i = 0; i < galleryPicturesTaken.Length; i++)
                {
                    if (galleryPicturesTaken[i])
                        GameManager.Instance.SetPictureTaken(Target.None, i);
                }
            }
        }
    }
}
