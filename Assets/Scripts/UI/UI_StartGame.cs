using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_StartGame : MonoBehaviour
{
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject button;

    [Header("Typewriter Text")]
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField][TextArea(3, 10)] private string text;
    [SerializeField] private float textDelay = 0.1f;
    private string currentText = "";
    
    private string sceneName;

    private void Awake()
    {        
        fadeScreen.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {        
        button.SetActive(false);
        StartCoroutine(StartTheGameCoroutine());        
    }    

    public void StartTheGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect("MainGame", 2f));
    }

    IEnumerator StartTheGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySFX(1, null);

        yield return new WaitForSeconds(1f);
        StartCoroutine(ShowText());

        yield return new WaitForSeconds(38f);
        AudioManager.instance.StopSFX(1);
        
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {
        sceneName = _sceneName;
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i <= text.Length; i++)
        {
            currentText = text.Substring(0, i);
            textUI.text = currentText;            
            yield return new WaitForSeconds(textDelay);
        }
    }

    public void PlayButtonClickSound()
    {
        AudioManager.instance.PlaySFX(0, null);
    }
}
