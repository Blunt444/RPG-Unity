using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
   public Enemy_Type enemyType;
   public Enemy_Color enemyColor;

   public float speed;
   public float attackCooldown;
   public float playerDetectionRange;
   public float attackRange;
   public int currentHealth;
   public int maxHealth;
   public int expReward;
   public int damage;
   public float weaponRange;
   public float knockbackForce;
   public float knockBackTime;
   public int guardDamage;

   public Transform detectionPoint;
   public LayerMask playerLayer;
   public Transform attackPoint;

   private Animator anim;


   public void Awake()
   {
      anim = GetComponent<Animator>();
      ChooseRandomType();
      ChooseRandomColor();
      SetStat();
      SetTransform();
      SetOverrideAnimator();

      detectionPoint = transform.Find("DetectionPoint");
      attackPoint = transform.Find("AttackPoint");

      if (detectionPoint == null || attackPoint == null)
      {
         Destroy(gameObject);
         return;
      }
   }

   public void ChooseRandomType()
   {
      enemyType = Enemy_Color_Type_Map.Instance.RandomType();
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
      EnemyStatStruct stats = Enemy_Stat_Map.Instance.GetEnemyStat(enemyType);
      speed = stats.speed;
      attackCooldown = stats.attackCooldown;
      playerDetectionRange = stats.playerDetectionRange;
      attackRange = stats.attackRange;
      maxHealth = stats.maxHealth;
      currentHealth = stats.maxHealth;
      expReward = stats.expReward;
      damage = stats.damage;
      weaponRange = stats.weaponRange;
      knockbackForce = stats.knockbackForce;
      knockBackTime = stats.knockBackTime;
      guardDamage = stats.guardDamage;
   }
   public void SetTransform()
   {
      EnemyTransform transforms = Enemy_Transform_Map.Instance.GetTransform(enemyType);
      playerLayer = transforms.playerLayer;
   }

}
