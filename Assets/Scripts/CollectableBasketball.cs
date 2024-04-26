using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VigilanteGamesUtilies;
using DG.Tweening;
using TMPro;

public class CollectableBasketball : ShootableObjectBase
{
    private Tween tween;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player") && canCollectable && value > 0) JumpOnToBand();
    }

    protected override void GetBallValue(float ballValue)
    {
        tween.Kill();
        tween = hitEffectPart.DOPunchScale(Vector3.one * 0.2f, 0.5f, 6).SetUpdate(true);

        if (value >= 0) value += ballValue;
        if (value < 0) value += 1;

        text.text = "" + value;
        if (value > 0) text.color = Color.white;

        if (ballValue < 0)
        {
            Destroy(gameObject);
        }
    }
}

