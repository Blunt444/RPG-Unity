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
    public GameObject lootPrefab;
    public ItemSO itemSO;

    [SerializeField]
    private int minWoodDrop;
    [SerializeField]
    private int maxWoodDrop;

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

    public void DropWood(Transform tree)
    {

        Debug.Log("dropwoodf");

        Loot loot = Instantiate(lootPrefab, tree.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, UnityEngine.Random.Range(minWoodDrop, maxWoodDrop + 1));
        loot.DropWoodAnimation();
    }
}
