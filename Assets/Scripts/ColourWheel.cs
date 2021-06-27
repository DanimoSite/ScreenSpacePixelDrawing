using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ColourWheel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Image Swatch;

    [SerializeField]
    private Transform Pin;

    [SerializeField]
    private ColourGradient ColourGradient;

    private Image m_image;
    private Texture2D m_texture;
    private RectTransform m_rectTransform;

    private void Awake()
    {
        m_rectTransform = transform.GetComponent<RectTransform>();
        m_image = transform.GetComponent<Image>();
        m_texture = m_image.sprite.texture;

        ColourGradient.Sampled += UpdateSwatch;
    }

    private void OnDestroy()
    {
        ColourGradient.Sampled -= UpdateSwatch;
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
    /// Samples the pixel colour as the given pointer position.
    /// </summary>
    /// <param name="eventData"></param>
    private void SamplePixel(PointerEventData eventData)
    {
        var mPos = eventData.position;
        var rect = m_rectTransform.rect;

        if (!RectTransformUtility.RectangleContainsScreenPoint(m_rectTransform, mPos))
            return;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rectTransform, mPos, null, out localPoint);

        int px = Mathf.Clamp(0, (int)(((localPoint.x - rect.x) * m_texture.width) / rect.width), m_texture.width);
        int py = Mathf.Clamp(0, (int)(((localPoint.y - rect.y) * m_texture.height) / rect.height), m_texture.height);

        Color32 col = m_texture.GetPixel(px, py);

        if (col.a < 250)
            return;

        SampleGradient(col);

        UpdatePinPosition(mPos);

    }

    /// <summary>
    /// Samples the Gradient of the given colour.
    /// </summary>
    /// <param name="colour"></param>
    /// <remarks>
    /// This assumes Gradient has a position within it's bounds. If not, then original colour will be returned.
    /// </remarks>
    private Color32 SampleGradient(Color32 colour)
    {
        ColourGradient.SetColour(colour);
        var col = ColourGradient.SamplePixel();

        return col.HasValue ? col.Value : colour;
    }

    /// <summary>
    /// Updates the Colour Swatch.
    /// </summary>
    /// <param name="colour"></param>
    private void UpdateSwatch(Color32 colour)
    {
        if (Swatch != null)
            Swatch.color = colour;

        ColourChanged?.Invoke(colour);
    }

    /// <summary>
    /// Updates the Pin Position on the Wheel.
    /// </summary>
    /// <param name="position"></param>
    private void UpdatePinPosition(Vector3 position)
    {
        if (Pin != null)
            Pin.position = position;
    }

    public delegate void OnColourChanged(Color32 colour);
    public event OnColourChanged ColourChanged;

}
