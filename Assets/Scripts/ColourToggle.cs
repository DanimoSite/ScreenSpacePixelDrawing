using UnityEngine;
using UnityEngine.UI;

public class ColourToggle : MonoBehaviour
{
    [SerializeField]
    private Color32 colour = new Color32(0, 0, 0, 255);
    public Color32 Colour => colour;

    [SerializeField]
    private Image background, toggleBackground;

    [SerializeField]
    private bool isEraser;
    public bool IsEraser => isEraser;

    private void OnValidate()
    {
        background.color = colour;
        toggleBackground.color = colour;
    }
}
