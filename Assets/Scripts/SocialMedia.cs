using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMedia : MonoBehaviour
{
    public void OpenInstagram()
    {
        string url = "http://instagram.com/afr.developer";
        Application.OpenURL(url);
    }

    public void OpenYoutube()
    {
        string url = "https://www.youtube.com/channel/UCnkQ74pS1kS6sUljzBvcwPg";
        Application.OpenURL(url);
    }

    public void OpenItchIo()
    {
        string url = "https://afr-developer.itch.io/";
        Application.OpenURL(url);
    }
}
