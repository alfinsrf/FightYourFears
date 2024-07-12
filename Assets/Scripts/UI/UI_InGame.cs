using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private SkillManager skills;
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Slider slider;       

    [Header("Money Info")]
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private float moneyAmount;
    [SerializeField] private float increaseRate = 100;


    // Start is called before the first frame update
    void Start()
    {
        if (playerStats != null)
        {
            playerStats.onHealthChanged += UpdateHealthUI;
        }

        skills = SkillManager.instance;        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoneyUI();                                                                                        
    }

    private void UpdateMoneyUI()
    {
        if (moneyAmount < PlayerManager.instance.GetCurrency())
        {
            moneyAmount += Time.deltaTime * increaseRate;
        }
        else
        {
            moneyAmount = PlayerManager.instance.GetCurrency();
        }

        currentMoney.text = ((int)moneyAmount).ToString() + (" Valor");
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }

    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }
}
