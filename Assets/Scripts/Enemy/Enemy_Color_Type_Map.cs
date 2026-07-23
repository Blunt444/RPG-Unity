using System;
using System.Collections.Generic;
using UnityEngine;

public struct TypeStruct
{
    public int key;
    public Enemy_Type type;
}

public struct ColorStruct
{
    public int key;
    public Enemy_Color color;
}

public class Enemy_Color_Type_Map : MonoBehaviour
{
    public static Enemy_Color_Type_Map Instance;

    [SerializeField]
    private List<TypeStruct> typeList = new List<TypeStruct>();
    [SerializeField]
    private List<ColorStruct> colorList = new List<ColorStruct>();
    private Dictionary<int, Enemy_Type> typeMap = new Dictionary<int, Enemy_Type>();
    private Dictionary<int, Enemy_Color> colorMap = new Dictionary<int, Enemy_Color>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            foreach (TypeStruct typeStruct in typeList)
            {
                typeMap[typeStruct.key] = typeStruct.type;
            }

            foreach (ColorStruct colorStruct in colorList)
            {
                colorMap[colorStruct.key] = colorStruct.color;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Enemy_Type RandomType()
    {
        return typeMap[UnityEngine.Random.Range(1, typeList.Count + 1)];
    }

    public Enemy_Color RandomColor()
    {
        return colorMap[UnityEngine.Random.Range(1, colorList.Count + 1)];
    }
}
