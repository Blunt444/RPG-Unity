using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct TreeOverrides
{
    public string key;
    public AnimatorOverrideController overrider;
    public Sprite[] choppedSprite;
}

public class TreeManager : MonoBehaviour
{
    public static TreeManager Instance;

    [SerializeField]
    List<TreeOverrides> list = new List<TreeOverrides>();

    public float maxShakeTime = 0f;
    public float shakeMagnitude = 0.3f;


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TreeOverrides RandomTreeVariant()
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
