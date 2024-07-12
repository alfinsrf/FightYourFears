using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_EndGame : MonoBehaviour
{
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private RectTransform rectTransformCredit;
    [SerializeField] private float scrollSpeed = 200;
    [SerializeField] private float offScreenPosition;

    private string sceneName = "MainMenu";
    private bool creditsSkipped;

    private void Awake()
    {
        fadeScreen.gameObject.SetActive(true);
    }    

    // Update is called once per frame
    void Update()
    {
        rectTransformCredit.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;   

        if(rectTransformCredit.anchoredPosition.y > offScreenPosition)
        {
            BackhToMainMenu();
        }
    }

    public void SkipCredit()
    {
        if(creditsSkipped == false)
        {
            scrollSpeed += 10;
            creditsSkipped = true;
        }
        else
        {
            BackhToMainMenu();
        }
    }

    private void BackhToMainMenu()
    {
        StartCoroutine(LoadSceneWithFadeEffect("MainMenu", 2f));
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {
        sceneName = _sceneName;
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }   
}
