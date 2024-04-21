using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerClass
{
    public static void SetLayerRecursively(this GameObject obj, string layer)
    {
        int layerIndex = LayerMask.NameToLayer(layer);
        obj.layer = layerIndex;

        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetLayerRecursively(layer);
        }
    }
}
