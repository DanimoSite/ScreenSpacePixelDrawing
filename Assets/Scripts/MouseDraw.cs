using System;
using System.IO;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDraw : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    [Tooltip("The Canvas which is a parent to this Mouse Drawing Component")]
    private Canvas HostCanvas;

    [Range(2, 20)]
    [Tooltip("The Pens Radius")]
    public int penRadius = 10;

    [Tooltip("The Pens Colour.")]
    public Color32 penColour = new Color32(0, 0, 0, 255);

    [Tooltip("The Drawing Background Colour.")]
    public Color32 backroundColour = new Color32(0,0,0,0);

    [SerializeField]
    [Tooltip("Pen Pointer Graphic GameObject")]
    private Image penPointer;

    [Tooltip("Toggles between Pen and Eraser.")]
    public bool IsEraser = false;

    private bool _isInFocus = false;
    /// <summary>
    /// Is this Component in focus.
    /// </summary>
    public bool IsInFocus
    {
        get => _isInFocus;
        private set
        {
            if (value != _isInFocus)
            {
                _isInFocus = value;
                TogglePenPointerVisibility(value);
            }
        }
    }

    private float m_scaleFactor = 10;
    private RawImage m_image;

    private Vector2? m_lastPos;

    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        m_image = transform.GetComponent<RawImage>();
        TogglePenPointerVisibility(false);
    }

    // TODO: Replace with IPointerDownHandler...
    void Update()
    {
        var pos = Input.mousePosition;

        if (IsInFocus)
        {
            SetPenPointerPosition(pos);

            if (Input.GetMouseButton(0))
                WritePixels(pos);
        }

        if (Input.GetMouseButtonUp(0))
            m_lastPos = null;
    }


    /// <summary>
    /// Initialisation logic.
    /// </summary>
    private void Init()
    {
        // Set scale Factor...
        m_scaleFactor = HostCanvas.scaleFactor * 2;

        var tex = new Texture2D(Convert.ToInt32(Screen.width/m_scaleFactor), Convert.ToInt32(Screen.height/m_scaleFactor), TextureFormat.RGBA32, false);
        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                tex.SetPixel(i, j, backroundColour);
            }
        }

        tex.Apply();
        m_image.texture = tex;
    }

    /// <summary>
    /// Writes the pixels to the Texture at the given ScreenSpace position.
    /// </summary>
    /// <param name="pos"></param>
    private void WritePixels(Vector2 pos)
    {
        pos /= m_scaleFactor;
        var mainTex = m_image.texture;
        var tex2d = new Texture2D(mainTex.width, mainTex.height, TextureFormat.RGBA32, false);

        var curTex = RenderTexture.active;
        var renTex = new RenderTexture(mainTex.width, mainTex.height, 32);

        Graphics.Blit(mainTex, renTex);
        RenderTexture.active = renTex;

        tex2d.ReadPixels(new Rect(0, 0, mainTex.width, mainTex.height), 0, 0);

        var col = IsEraser ? backroundColour : penColour;
        var positions = m_lastPos.HasValue ? GetLinearPositions(m_lastPos.Value, pos) : new List<Vector2>() { pos };

        foreach (var position in positions)
        {
            var pixels = GetNeighbouringPixels(new Vector2(mainTex.width, mainTex.height), position, penRadius);

            if (pixels.Count > 0)
                foreach (var p in pixels)
                    tex2d.SetPixel((int)p.x, (int)p.y, col);
        }

        tex2d.Apply();

        RenderTexture.active = curTex;
        renTex.Release();
        Destroy(renTex);
        Destroy(mainTex);
        curTex = null;
        renTex = null;
        mainTex = null;

        m_image.texture = tex2d;
        m_lastPos = pos;
    }

    /// <summary>
    /// Clears the Texture.
    /// </summary>
    [ContextMenu("Clear Texture")]
    public void ClearTexture()
    {
        var mainTex = m_image.texture;
        var tex2d = new Texture2D(mainTex.width, mainTex.height, TextureFormat.RGBA32, false);

        for (int i = 0; i < tex2d.width; i++)
        {
            for (int j = 0; j < tex2d.height; j++)
            {
                tex2d.SetPixel(i, j, backroundColour);
            }
        }

        tex2d.Apply();
        m_image.texture = tex2d;
    }

    /// <summary>
    /// Gets the neighbouring pixels at a given screenspace position.
    /// </summary>
    /// <param name="textureSize">The texture size or pixel domain.</param>
    /// <param name="position">The ScreenSpace position.</param>
    /// <param name="brushRadius">The Brush radius.</param>
    /// <returns>List of pixel positions.</returns>
    private List<Vector2> GetNeighbouringPixels(Vector2 textureSize, Vector2 position, int brushRadius)
    {
        var pixels = new List<Vector2>();

        for (int i = -brushRadius; i < brushRadius; i++)
        {
            for (int j = -brushRadius; j < brushRadius; j++)
            {
                var pxl = new Vector2(position.x + i, position.y + j);
                if (pxl.x > 0 && pxl.x < textureSize.x && pxl.y > 0 && pxl.y < textureSize.y)
                    pixels.Add(pxl);
            }
        }

        return pixels;
    }

    /// <summary>
    /// Interpolates between two positions with a spacing (default = 2)
    /// </summary>
    /// <param name="firstPos"></param>
    /// <param name="secondPos"></param>
    /// <param name="spacing"></param>
    /// <returns>List of interpolated positions</returns>
    private List<Vector2> GetLinearPositions(Vector2 firstPos, Vector2 secondPos, int spacing = 2)
    {
        var positions = new List<Vector2>();

        var dir = secondPos - firstPos;

        if (dir.magnitude <= spacing)
        {
            positions.Add(secondPos);
            return positions;
        }

        for (int i = 0; i < dir.magnitude; i += spacing)
        {
            var v = Vector2.ClampMagnitude(dir, i);
            positions.Add(firstPos + v);
        }

        positions.Add(secondPos);
        return positions;
    }

    /// <summary>
    /// Sets the Pens Colour.
    /// </summary>
    /// <param name="color"></param>
    public void SetPenColour(Color32 color) => penColour = color;

    /// <summary>
    /// Sets the Radius of the Pen.
    /// </summary>
    /// <param name="radius"></param>
    public void SetPenRadius(int radius) => penRadius = radius;

    /// <summary>
    /// Sets the Size of the Pen Pointer.
    /// </summary>
    private void SetPenPointerSize()
    {
        var rt = penPointer.rectTransform;
        rt.sizeDelta = new Vector2(penRadius * 5, penRadius * 5);
    }

    /// <summary>
    /// Sets the position of the Pen Pointer Graphic.
    /// </summary>
    /// <param name="pos"></param>
    private void SetPenPointerPosition(Vector2 pos)
    {
        penPointer.transform.position = pos;
    }

    /// <summary>
    /// Toggles the visibility of the Pen Pointer Graphic.
    /// </summary>
    /// <param name="isVisible"></param>
    private void TogglePenPointerVisibility(bool isVisible)
    {
        if (isVisible)
            SetPenPointerSize();

        penPointer.gameObject.SetActive(isVisible);
        Cursor.visible = !isVisible;
    }

    /// <summary>
    /// On Mouse Pointer entering this Components Image Space.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) => IsInFocus = true;

    /// <summary>
    /// On Mouse Pointer exiting this Components Image Space.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) => IsInFocus = false;

    /// <summary>
    /// Exports the Sketch as a PNG.
    /// </summary>
    /// <param name="targetDirectory"></param>
    /// <param name="fileName"></param>
    public void ExportSketch(string targetDirectory, string fileName)
    {
        var dt = DateTime.Now.ToString("yyMMdd_hhmmss");
        fileName += $"_{dt}";

        targetDirectory = Path.Combine(targetDirectory, "Pixel Drawings");

        var mainTex = m_image.texture;
        var tex2d = new Texture2D(mainTex.width, mainTex.height, TextureFormat.RGBA32, false);

        var curTex = RenderTexture.active;
        var renTex = new RenderTexture(mainTex.width, mainTex.height, 32);

        Graphics.Blit(mainTex, renTex);
        RenderTexture.active = renTex;

        tex2d.ReadPixels(new Rect(0, 0, mainTex.width, mainTex.height), 0, 0);

        tex2d.Apply();

        RenderTexture.active = curTex;
        Destroy(renTex);
        curTex = null;
        renTex = null;
        mainTex = null;

        var png = tex2d.EncodeToPNG();

        if (!Directory.Exists(targetDirectory))
            Directory.CreateDirectory(targetDirectory);

        var fp = Path.Combine(targetDirectory, fileName + ".png");

        if (File.Exists(fp))
            File.Delete(fp);

        File.WriteAllBytes(fp, png);

        System.Diagnostics.Process.Start(targetDirectory);
    }
}
