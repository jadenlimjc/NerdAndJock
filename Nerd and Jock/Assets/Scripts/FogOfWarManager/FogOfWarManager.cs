using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FogOfWarManager : MonoBehaviour
{
    public Image visibilityMask;
    public float normalVisibilityWidth = 1f;
    public float normalVisibilityHeight = 1f;
    //public float increasedVisibilityWidth = 1.5f;
    //public float increasedVisibilityHeight = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        SetVisibilitySize(normalVisibilityWidth, normalVisibilityHeight);
    }

    public void collectTorchlight()
    {
        float increasedVisibilityWidth = normalVisibilityWidth * 1.5f;
        float increasedVisibilityHeight = normalVisibilityHeight * 1.5f;
        normalVisibilityWidth = increasedVisibilityWidth;
        normalVisibilityHeight = increasedVisibilityHeight;
        SetVisibilitySize(increasedVisibilityWidth, increasedVisibilityHeight);      
    }

    private void SetVisibilitySize(float width, float height)
    {
        if (visibilityMask != null)
        {
            visibilityMask.rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
    

}
