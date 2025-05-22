using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSliderCT : SliderCT
{
    public override void SetLevel(int level)
    {
        base.SetLevel(level);

        SettingsManager.Instance.SetSFXVolume(level / _levels);
    }
}
