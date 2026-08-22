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
    public List<TreeOverrides> list = new List<TreeOverrides>();

    public float maxShakeTime = 0f;
    public float shakeMagnitude = 0.3f;
    public GameObject lootPrefab;
    public ItemSO itemSO;
    public List<TreeScript> trees = new List<TreeScript>();

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

    public void SetSaveTreeData(TreeData treeData)
    {
        foreach (TreeScript tree in trees)
        {
            if (tree.id == treeData.id)
            {
                tree.isDead = treeData.isDead;
                tree.SetTree(treeData);
                if (treeData.isDead)
                    tree.Die();
                // Debug.Log(tree.id);
                return;
            }
        }
    }

    public TreeOverrides RandomTreeVariant()
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public void DropWood(Transform tree)
    {

        // Debug.Log("dropwoodf");

        Loot loot = Instantiate(lootPrefab, tree.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, UnityEngine.Random.Range(minWoodDrop, maxWoodDrop + 1));
        loot.DropWoodAnimation();
    }
}
