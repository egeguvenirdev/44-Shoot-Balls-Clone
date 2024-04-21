using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateVelocity;
    [SerializeField] private Space _rotateSpace;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float additionalHeight = 0.5f;

    private Vector3 minHeight;
    private Vector3 maxHeight;

    public void Init()
    {
        minHeight = transform.position;
        maxHeight = minHeight + new Vector3(0, additionalHeight, 0);

        ActionManager.Updater += OnUpdate;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        transform.Rotate(_rotateVelocity * Time.deltaTime, _rotateSpace);

        float lerpValue = (Mathf.Sin(speed * Time.time) + 1f) / 2f; // <0, 1> 
        transform.position = Vector3.Lerp(minHeight, maxHeight, lerpValue * deltaTime);
    }
}

