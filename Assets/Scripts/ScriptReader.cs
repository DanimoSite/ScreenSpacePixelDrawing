﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptReader : MonoBehaviour
{

    public TextMeshProUGUI textComponent;

    private List<string> linesList = new List<string>();
    public float textSpeed;
    private string[] lines;
    int index;

    void Start()
    {
        index = 0;
        linesList.Add("Let’s start off simple by drawing a circle in the middle of your canvas. Try using the circle tool in the bottom-left!");
        linesList.Add("Then, draw a right-ward facing arrow in the middle of the circle, like a greater-than sign! You can do this using the free-draw tool in the bottom left. It looks like a squiggly line.");
        lines = linesList.ToArray();
        textComponent.text = lines[index];

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            NextLine(); 
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousLine();
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = lines[index];
        }
    }

    void PreviousLine()
    {
        if (index > 0)
        {
            index--;
            textComponent.text = lines[index];
        }
    }


}