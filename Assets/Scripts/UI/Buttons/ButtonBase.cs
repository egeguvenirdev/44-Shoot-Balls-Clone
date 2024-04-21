using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonBase : MonoBehaviour
{
    protected VibrationManager vibration;

    public virtual void Init()
    {
        vibration = VibrationManager.Instance;
    }

    public abstract void DeInit();

    public virtual void OnButtonClick()
    {
        vibration.SelectionVibration();
    }
}
