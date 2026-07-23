using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   public Enemy_Type enemyType;
   public Enemy_Color enemyColor;

   public void Awake()
   {
      ChooseRandomType();
      ChooseRandomColor();
   }

   public void ChooseRandomType()
   {
      enemyType = Enemy_Color_Type_Map.Instance.RandomType();
   }
   public void ChooseRandomColor()
   {
      enemyColor = Enemy_Color_Type_Map.Instance.RandomColor();
   }
   
}
