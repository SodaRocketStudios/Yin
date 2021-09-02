using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetLayerRecursively(this GameObject root, int layer)
    {
        root.layer = layer;

        for(int i = 0; i < root.transform.childCount; i++)
        {
            Transform child = root.transform.GetChild(i);
            child.gameObject.SetLayerRecursively(layer);
        }
    }
}
