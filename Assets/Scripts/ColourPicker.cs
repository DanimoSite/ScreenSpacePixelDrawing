using System.Collections;
using System.Collections.Generic;

using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour
{   
    [SerializeField]
    private MouseDraw MouseDrawComponent;

    [SerializeField]
    private Button redButton, orangeButton, yellowButton, greenButton, blueButton, magentaButton, blackButton;

    [SerializeField]
    private Toggle eraser;

    // Start is called before the first frame update
    void Start()
    {
        MouseDrawComponent.SetPenColour(Color.red);
         MouseDrawComponent.SetPenRadius(2);
        redButton.onClick.AddListener(OnRedPressed);
        orangeButton.onClick.AddListener(OnOrangePressed);
        yellowButton.onClick.AddListener(OnYellowPressed);
        greenButton.onClick.AddListener(OnGreenPressed);
        blueButton.onClick.AddListener(OnBluePressed);
        magentaButton.onClick.AddListener(OnMagentaPressed);
        blackButton.onClick.AddListener(OnBlackPressed);
        eraser.onValueChanged.AddListener(OnEraserToggled);

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnPenColourChanged(Color32 colour)
    {
        MouseDrawComponent.SetPenColour(colour);
    }

    private void OnRedPressed()
    {
        MouseDrawComponent.SetPenColour(Color.red);
    }

    private void OnOrangePressed()
    {
        MouseDrawComponent.SetPenColour(new Color32(255, 160, 0, 255));
    }

    private void OnYellowPressed()
    {
        MouseDrawComponent.SetPenColour(Color.yellow);
    }

    private void OnGreenPressed()
    {
        MouseDrawComponent.SetPenColour(Color.green);
    }

    private void OnBluePressed()
    {
        MouseDrawComponent.SetPenColour(Color.blue);
    }

    private void OnMagentaPressed()
    {
        MouseDrawComponent.SetPenColour(Color.magenta);
    }

        private void OnBlackPressed()
    {
        MouseDrawComponent.SetPenColour(Color.black);
    }


    private void OnEraserToggled(bool value)
    {
        MouseDrawComponent.IsEraser = value;
    }
}
