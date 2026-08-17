using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   public Enemy_Type enemyType;
   public Enemy_Color enemyColor;
   public Enemy_Difficulty enemyDiffculty;

   public bool isManuallySpawned = false;

   public float speed;
   public float attackCooldownBuffer;
   public float playerDetectionRange;
   public float attackRange;
   public int currentHealth;
   public int maxHealth;
   public int expReward;
   public int damage;
   public float weaponRange;
   public float knockbackForce;
   public float knockBackTime;
   public float knockBackTimeResistance;
   public float stuntResistance;
   public int guardDamage;
   public int minDrop;
   public int maxDrop;
   public int minQuantity;
   public int maxQuantity;
   public List<ItemSO> loots;
   public GameObject LootPrefab;

   public Spawner_Spawn spawnerHut;

   public Transform detectionPoint;
   public LayerMask playerLayer;
   public Transform attackPoint;

   public Death deathPrefab;

   private Animator anim;


   public void Awake()
   {
      anim = GetComponent<Animator>();
      if (!isManuallySpawned)
      {
         ChooseRandomType();
         ChooseRandomColor();
         SetOverrideAnimator();
      }

      SetStat();
      SetTransform();
      SetCombatTransform();

      deathPrefab = Enemy_Color_Type_Map.Instance.GetDeathPrefab();

      if (detectionPoint == null || attackPoint == null)
      {
         Destroy(gameObject);
         return;
      }
   }

   private void SetCombatTransform()
   {
      if (enemyType == Enemy_Type.Torch)
      {
         detectionPoint = transform.Find("DetectionPoint");
         attackPoint = transform.Find("AttackPoint");
      }
      else if (enemyType == Enemy_Type.Tnt)
      {
         detectionPoint = transform.Find("DetectionPoint");
         attackPoint = transform.Find("AttackPoint");
      }
   }

   public void ChooseRandomType()
   {
      enemyType = Enemy_Color_Type_Map.Instance.RandomType();
      enemyDiffculty = Enemy_Color_Type_Map.Instance.RandomDifficulty();
   }
   public void ChooseRandomColor()
   {
      enemyColor = Enemy_Color_Type_Map.Instance.RandomColor();
   }
   public void SetOverrideAnimator()
   {
      anim.runtimeAnimatorController = Enemy_Color_Type_Map.Instance.GetOverrideController(enemyType, enemyColor);
   }
   public void SetStat()
   {
      EnemyStatStruct stats = Enemy_Stat_Map.Instance.GetEnemyStat(enemyType, enemyDiffculty);
      speed = stats.speed;
      attackCooldownBuffer = stats.attackCooldownBuffer;
      playerDetectionRange = stats.playerDetectionRange;
      attackRange = stats.attackRange;
      maxHealth = stats.maxHealth;
      currentHealth = stats.maxHealth;
      expReward = stats.expReward;
      damage = stats.damage;
      weaponRange = stats.weaponRange;
      knockbackForce = stats.knockbackForce;
      knockBackTime = stats.knockBackTime;
      knockBackTimeResistance = stats.knockBackTimeResistance;
      stuntResistance = stats.stuntResistance;
      guardDamage = stats.guardDamage;
      minDrop = stats.minDrop;
      maxDrop = stats.maxDrop;
      minQuantity = stats.minQuantity;
      maxQuantity = stats.maxQuantity;
      loots = new List<ItemSO>(stats.loots);
   }
   public void SetTransform()
   {
      EnemyTransform transforms = Enemy_Transform_Map.Instance.GetTransform(enemyType);
      playerLayer = transforms.playerLayer;
      LootPrefab = transforms.LootPrefab;
   }

}
