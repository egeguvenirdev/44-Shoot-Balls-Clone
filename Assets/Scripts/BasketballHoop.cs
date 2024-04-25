using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VigilanteGamesUtilies;
using DG.Tweening;
using TMPro;

public class BasketballHoop : ShootableObjectBase
{
    private Tween tween;

    protected override void GetBallValue(float ballValue)
    {
        tween.Kill();
        tween = transform.DOPunchScale(Vector3.one * 0.05f, 0.5f, 6).SetUpdate(true);

        value -= ballValue;
        text.text = "" + value;

        if (ballValue < 0)
        {
            Destroy(gameObject);
        }
    }
}
