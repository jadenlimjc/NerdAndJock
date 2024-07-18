using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationHandler : MonoBehaviour
{
    public Button button;

    void Start()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }

    // Function to handle button hover scaling
    public void OnHover()
    {
        // Example logic to scale the button
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    // Function to handle button color change on click
    public void OnClickColorChange()
    {
        // Example logic to change button color
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = Color.green;
        button.colors = colorBlock;
    }

    // Function to reset the scale
    public void OnHoverExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
