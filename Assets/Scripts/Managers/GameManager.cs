using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Transform player;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }    

    public void PauseGame(bool _pause)
    {
        if(_pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
