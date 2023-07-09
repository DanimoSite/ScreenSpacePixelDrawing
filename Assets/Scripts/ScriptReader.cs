﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class ScriptReader : MonoBehaviour
{

    public TextMeshProUGUI textComponent;

    [SerializeField]
    private MouseDraw MouseDrawComponent;
    
    public float textSpeed;
    private string[][] scriptLines = new string[][] 
    {   new string[] 
        {
            "Start off by picking a colour from the options in the bottom left. ", 
            "When you’re ready hit the right arrow key on your keyboard to view the next step!",
            "You can hit the left arrow key to go back a step. Let’s draw a circle in the middle of your canvas.",
            "Then, draw a right-ward facing arrow in the middle of the circle, like a greater-than sign!",
            "Above the arrow, to both the left and the right of it, draw two small vertical lines.",
            "Below the arrow, draw a wide ‘U’ shape. If you mess up, make sure to use the eraser tool!",
            "Now, write down what you’ve drawn in the textbox below!"
        },
        new string[] 
        {
            "Let’s start off simple by drawing a circle in the middle of your canvas. Try using the circle tool in the bottom-left!", 
            "Then, draw a right-ward facing arrow in the middle of the circle, like a greater-than sign! You can do this using the free-draw tool in the bottom left. It looks like a squiggly line."
        },
        new string[] {"cat", "kitten", "kitty", "yuumi"},
        new string[] {"house", "home", "building"}
    };
    private string[] lines;
    public int indexScript;
    public int indexLine;

    [SerializeField]
    private InputField inputField; 

    [SerializeField]
    private Image image;

    private string input; 

    private string[][] acceptableAnswerArray = new string[][] 
    {
        new string[] {"smileyface", "smilingface", "smile", "smilyface", "smileemoji", ":)"},
        new string[] {"pac-man", "pacman"},
        new string[] {"cat", "kitten", "kitty", "yuumi"},
        new string[] {"house", "home", "building"}
    };

    private string[] acceptableAnswers;
    public int indexAnswer;

    void Start()
    {
        indexScript = 0;
        indexLine = 0;
        lines = scriptLines[indexScript];
        textComponent.text = lines[indexLine];
        indexAnswer = 0;
        acceptableAnswers = acceptableAnswerArray[indexAnswer];
        image.color = new Color(255, 255, 255, 0);
    }

    IEnumerator FadeImage()
    {
        // loop over 1 second
        for (float i = 0; i <=0.2f; i += Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(1, 1, 1, i * 5);
            yield return null;
        }

        for (float i = 0; i <=0.5f; i += Time.deltaTime)
        {
            image.color = new Color(1, 1, 1, 1);
            yield return null;
        }

        for (float i = 0.2f; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            image.color = new Color(1, 1, 1, i * 5);
            yield return null;
        }

        UpdateNextScript();
    }

    private void UpdateNextScript() 
    {
        indexAnswer++;
        acceptableAnswers = acceptableAnswerArray[indexAnswer];
        indexLine = 0;
        indexScript++;
        lines = scriptLines[indexScript];
        textComponent.text = lines[indexLine];
        MouseDrawComponent.ClearTexture();
    }

    void Update()
    {           
        if (Input.GetKeyDown(KeyCode.Return))
        {   
            string noWhiteSpace = inputField.text.Replace(" ", string.Empty);
            if (acceptableAnswers.Contains(noWhiteSpace.ToLower())) 
            {                
                StartCoroutine(FadeImage());
            }
            inputField.text = "";
        }

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
        if (indexLine < lines.Length - 1)
        {
            indexLine++;
            textComponent.text = lines[indexLine];
        }
    }

    void PreviousLine()
    {
        if (indexLine > 0)
        {
            indexLine--;
            textComponent.text = lines[indexLine];
        }
    }


}