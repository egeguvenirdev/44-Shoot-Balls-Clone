using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLevelButton : ButtonBase
{
    public override void Init()
    {
        base.Init();
    }

    public override void DeInit()
    {

    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();

        gameObject.SetActive(false);
    }
}
