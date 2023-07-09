using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

public class ColourPaletteView : MonoBehaviour
{
    [SerializeField]
    private MouseDraw MouseDrawComponent;

    // [SerializeField]
    // private ColourWheel ColourWheel;

    [SerializeField]
    private Slider penWidth;

    [SerializeField]
    private Toggle eraser;

    [SerializeField]
    private Button clearButton, exportButton, exitButton;

    [SerializeField]
    private string SaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    [SerializeField]
    private string FileName = "MyImage";

    private void OnEnable()
    {
        if (!Directory.Exists(SaveDirectory))
            SaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // ColourWheel.ColourChanged += OnPenColourChanged;
        penWidth.onValueChanged.AddListener(OnPenWidthChanged);
        eraser.onValueChanged.AddListener(OnEraserToggled);
        clearButton.onClick.AddListener(OnClearDrawing);
        exportButton.onClick.AddListener(OnExportDrawing);
        exitButton.onClick.AddListener(OnExit);
    }

    private void Start()
    {
        OnPenWidthChanged(penWidth.value);
        OnPenColourChanged(Color.red);
    }

    private void OnDisable()
    {
        // ColourWheel.ColourChanged -= OnPenColourChanged;
        penWidth.onValueChanged.RemoveListener(OnPenWidthChanged);
        eraser.onValueChanged.RemoveListener(OnEraserToggled);
        clearButton.onClick.RemoveListener(OnClearDrawing);
        exportButton.onClick.RemoveListener(OnExportDrawing);
        exitButton.onClick.RemoveListener(OnExit);
    }

    private void OnPenColourChanged(Color32 colour)
    {
        MouseDrawComponent.SetPenColour(colour);
    }

    private void OnPenWidthChanged(float value)
    {
        MouseDrawComponent.SetPenRadius((int)value);
    }

    private void OnEraserToggled(bool value)
    {
        MouseDrawComponent.IsEraser = value;
    }

    private void OnClearDrawing()
    {
        MouseDrawComponent.ClearTexture();
    }

    private void OnExportDrawing()
    {
        MouseDrawComponent.ExportSketch(SaveDirectory, FileName);
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
