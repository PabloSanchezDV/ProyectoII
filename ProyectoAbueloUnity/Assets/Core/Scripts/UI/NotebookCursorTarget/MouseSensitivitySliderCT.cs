using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSensitivitySliderCT : SliderCT
{
    public override void SetLevel(int level)
    {
        base.SetLevel(level);

        SettingsManager.Instance.SetMouseSensitivity((float)level / (float)_levels);
    }
}
