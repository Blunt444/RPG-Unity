using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance;
    public int level;
    public int currentExp;
    public int expToLevel;
    public float expGrowthMultiplier;
    public Slider expSlider;
    public TMP_Text currentLevelTxt;

    private void Awake()
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

    public void SetValues(int level, int currentExp, int expToLevel, float expGrowthMultiplier)
    {
        this.level = level;
        this.currentExp = currentExp;
        this.expGrowthMultiplier = expGrowthMultiplier;
        this.expToLevel = expToLevel;

        UpdateUI();
    }

    public Dictionary<string, float> GetValues()
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        dict["level"] = level;
        dict["currentExp"] = currentExp;
        dict["expToLevel"] = expToLevel;
        dict["expGrowthMultiplier"] = expGrowthMultiplier;
        return dict;
    }

    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }

    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }
    private void OnDisable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        StanceManager.Instance.BlockSwitchingStance();
        if (currentExp >= expToLevel)
        {
            LevelUp();
        }
        StanceManager.Instance.UnblockSwitchingStance();
        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = CalculateExpForNextLevel();

        StanceManager.Instance.ChangePointToRespectiveStance(1);

        UpdateUI();

    }

    private int CalculateExpForNextLevel()
    {
        return Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelTxt.text = "Level: " + level;
    }
}
