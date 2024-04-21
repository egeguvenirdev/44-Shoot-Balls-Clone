using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandRotator : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private float speed;

    private float offsetValueY;
    private bool canMove;

    public void Init()
    {
        ActionManager.Updater += OnUpdate;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        offsetValueY -= speed * deltaTime;
        mat.mainTextureOffset = new Vector2(0, offsetValueY);
    }
}