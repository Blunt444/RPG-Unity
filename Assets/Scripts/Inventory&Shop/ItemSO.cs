using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
   public string itemName;
   [TextArea]public string itemDescription;
   public Sprite icon;
   public int stackSize;
   public Vector2 uiOffset;
   public Vector2 shopUIOffset;
   public Vector2 shopUIItemSize = new Vector2(120,120);

   public bool isGold;
   public bool isUsable = true;

   [Header("Stats")]
   public int currentHealth;
   public int maxHealth;
   public int speed;
   public int damage;

   [Header("For Temporary Items")]
   public float duration;
   

}
