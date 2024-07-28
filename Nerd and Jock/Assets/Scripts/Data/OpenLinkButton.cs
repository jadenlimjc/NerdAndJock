using UnityEngine;

public class OpenLinkButton : MonoBehaviour
{
    public string url; // The URL to open

    public void OpenLink()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("URL is not set or is empty.");
        }
    }
}