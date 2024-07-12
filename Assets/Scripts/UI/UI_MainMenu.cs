using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour, ISaveManager
{
    #region Components & GameObject
    [Header("Main Game")]
    [SerializeField] private string sceneName = "MainGame";

    [Header("Game Object")]
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;

    [Header("Options Volume")]
    [SerializeField] private UI_VolumeSlider[] volumeSettings;
    #endregion

    private void Awake()
    {
        fadeScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        if (SaveManager.instance.HasSaveData() == false)
        {
            continueButton.SetActive(false);
        }
    }

    public void ContinueGame()
    {
        AudioManager.instance.PlaySFX(0, null);
        StartCoroutine(LoadSceneWithFadeEffect("MainGame", 2f));
    }

    public void NewGame()
    {
        AudioManager.instance.PlaySFX(0, null);
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect("StartGame", 2f));
    }

    public void EndCreditGame()
    {
        AudioManager.instance.PlaySFX(0, null);
        StartCoroutine(LoadSceneWithFadeEffect("EndGame", 2f));
    }

    public void ExitGame()
    {
        AudioManager.instance.PlaySFX(0, null);        
        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {
        sceneName = _sceneName;
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
   
    public void SaveGameValue()
    {
        SaveManager.instance.SaveGame();
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
        AudioManager.instance.PlaySFX(0, null);
    }
}
