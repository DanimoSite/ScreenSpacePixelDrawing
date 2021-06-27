using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColourGradient : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Image GradientBackground;

    [SerializeField]
    private Transform Pin;

    private RectTransform m_rectTransform;

    private void Awake()
    {
        m_rectTransform = transform.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Triggered when user is pressing mouse or finger down.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData) => SamplePixel(eventData);

    /// <summary>
    /// Triggered when user is dragging with the mouse or finger.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData) => SamplePixel(eventData);

    /// <summary>
    /// Samples the pixel colour at a given position.
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns>
    /// The pixel colour or null if could not evaluate.
    /// </returns>
    /// <remarks>
    /// If no EventData is passed, then the Pin Position will be used instead. This is useful if user is changing the colour wheels position and there is no event data to pass but still needs to sample the gradient with the new colour.
    /// </remarks>
    public Color32? SamplePixel(PointerEventData eventData = null)
    {
        Color32? col = null;

        var mPos = eventData != null ? eventData.position : (Vector2)Pin.position;

        if (RectTransformUtility.RectangleContainsScreenPoint(m_rectTransform, mPos))
            StartCoroutine(SampleScreenshot(mPos, returnValue =>
            {
                col = returnValue;
                Sampled?.Invoke(returnValue);

                UpdatePin(mPos, returnValue);

            }));

        return col;
    }

    // Here we are sampling the screenshot as the gradient swatch is a combination of 2 images. A better way would be to sample both images and blend the colours. Will look into later, for now it works.
    /// <summary>
    /// Samples the pixels colour at a given position.
    /// </summary>
    /// <param name="position">The screenspace position to sample.</param>
    /// <param name="result">Callback which returns the colour of the pixel.</param>
    /// <remarks>
    /// This method uses ScreenCapture to take an image for sampling and therefore needs to wait until the end-of-frame to capture screenshot before sampling.
    /// </remarks>
    IEnumerator SampleScreenshot(Vector2 position, Action<Color32> result = null)
    {
        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        Color32 col = texture.GetPixel((int)position.x, (int)position.y);
        // Ensure alpha is max...
        col.a = 255;
        result(col);

        Destroy(texture);
    }

    /// <summary>
    /// Sets the base colour.
    /// </summary>
    /// <param name="colour"></param>
    public void SetColour(Color32 colour)
    {
        if (GradientBackground != null)
            GradientBackground.color = colour;
    }

    /// <summary>
    /// Update the Pins position and colour.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="colour"></param>
    private void UpdatePin(Vector2 position, Color32 colour)
    {
        if (Pin == null)
            return;

        Pin.position = position;

        // We want the pin to be clearly visible no matter what colour the pin is over. Easiest way is to simply invert the colour...
        Pin.GetComponent<Image>().color = InvertColour(colour);
    }

    /// <summary>
    /// Inverts the given colour.
    /// </summary>
    /// <param name="colour">Colour to invert</param>
    /// <returns>Inverted Colour</returns>
    private Color32 InvertColour(Color32 colour)
    {
        return new Color32((byte)(255 -colour.r),(byte)(255 - colour.g), (byte)(255 -colour.b), 255);
    }

    public delegate void OnSampled(Color32 colour);
    public event OnSampled Sampled;
}
