using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour, ISaveManager
{
    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;    
    [SerializeField] private GameObject restartButton;
    [Space]
    
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject shopUgradeUI;
    public GameObject endGameUI;

    [SerializeField] private UI_VolumeSlider[] volumeSettings;

    private void Awake()
    {
        SwitchTo(shopUgradeUI);
        fadeScreen.gameObject.SetActive(true);        
    }

    // Start is called before the first frame update
    void Start()
    {           
        SwitchTo(inGameUI);        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchWithKeyTo(optionsUI);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SwitchWithKeyTo(shopUgradeUI);
        }
    }

    public void HealPlayer()
    {
        if(PlayerManager.instance.player.stats.currentHealth == PlayerManager.instance.player.stats.GetMaxHealthValue())
        {
            return;
        }

        if (PlayerManager.instance.HaveEnoughMoney(100) == false)
        {
            return;
        }
        
        PlayerManager.instance.player.stats.IncreaseHealthBy(20);
    }

    public void UpgradeDamagePlayer()
    {        
        if (PlayerManager.instance.HaveEnoughMoney(100) == false)
        {
            return;
        }        

        PlayerManager.instance.player.stats.damage.AddModifier(10);
    }

    public void EndTheGame()
    {
        if (PlayerManager.instance.HaveEnoughMoney(10000) == false)
        {
            return;
        }        
        
        PlayerManager.instance.MakePlayerInvincible();

        fadeScreen.FadeOut();
        StartCoroutine(EndTheGameCoroutine());

    }

    IEnumerator EndTheGameCoroutine()
    {
        yield return new WaitForSeconds(1);
        SaveManager.instance.DeleteSavedData();

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("EndGame"); 
    }

    public void SwitchTo(GameObject _menu)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            bool FadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;

            if (FadeScreen == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (_menu != null)
        {
            AudioManager.instance.PlaySFX(3, null);
            _menu.SetActive(true);
        }

        if (GameManager.instance != null)
        {
            if (_menu == inGameUI)
            {
                GameManager.instance.PauseGame(false);
            }
            else
            {
                GameManager.instance.PauseGame(true);
            }
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
        AudioManager.instance.PlaySFX(3, null);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
            {
                return;
            }
        }

        SwitchTo(inGameUI);
    }

    public void SwitchOnEndScreen()
    {        
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {                
        yield return new WaitForSeconds(2f);
        restartButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();

    public void BackToMainMenu()
    {
        GameManager.instance.PauseGame(false);
        PlayerManager.instance.MakePlayerInvincible();

        fadeScreen.FadeOut();
        StartCoroutine(BackToMainMenuCoroutine());

    }

    IEnumerator BackToMainMenuCoroutine()
    {
        yield return new WaitForSeconds(1);
        SaveManager.instance.SaveGame();

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volumeSettings)
        {
            foreach (UI_VolumeSlider item in volumeSettings)
            {
                if (item.parameter == pair.Key)
                {
                    item.LoadSlider(pair.Value);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.volumeSettings.Clear();

        foreach (UI_VolumeSlider item in volumeSettings)
        {
            _data.volumeSettings.Add(item.parameter, item.slider.value);
        }
    }

    public void PlayButtonClickSound()
    {
        AudioManager.instance.PlaySFX(2, null);
    }    
}
