using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VigilanteGamesUtilies;
using DG.Tweening;
using TMPro;

public class BasketballHoop : ShootableObjectBase
{
    [SerializeField] private SkinnedMeshRenderer mesh;
    private Tweener smoothTween;
    private Tween tween;
    private float smoothMoneyNumbers = 0f;

    protected override void GetBallValue(float ballValue)
    {
        tween.Kill();
        tween = transform.DOPunchScale(Vector3.one * 0.025f, 0.5f, 6).SetUpdate(true);
        NetFlopEffect();

        if (value >= 0) value -= ballValue;
        if (value < 0) value += 1;

        text.text = "" + value;
        if (value > 0) text.color = Color.white;

        if (ballValue < 0)
        {
            Destroy(gameObject);
        }
    }

    private void NetFlopEffect()
    {
        smoothTween.Kill();
        smoothMoneyNumbers = 0;
        mesh.SetBlendShapeWeight(0, 0f);

        smoothTween = DOTween.To(() => smoothMoneyNumbers, x => smoothMoneyNumbers = x, 100f, 0.2f).SetSpeedBased(false)
            .OnUpdate(() => 
            { 
                mesh.SetBlendShapeWeight(0, smoothMoneyNumbers); 
            })

            .OnComplete(() => 
            {
                smoothTween = DOTween.To(() => smoothMoneyNumbers, x => smoothMoneyNumbers = x, 0, 0.2f).SetSpeedBased(false)
                    .OnUpdate(() =>
                    {
                        mesh.SetBlendShapeWeight(0, smoothMoneyNumbers);
                    });
            });
        
    }
}
