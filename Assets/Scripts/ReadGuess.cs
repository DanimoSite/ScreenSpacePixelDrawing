using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ReadGuess : MonoBehaviour
{
    // [SerializeField]
    // private InputField inputField; 

    // [SerializeField]
    // private Image image;

    // [SerializeField]
    // private ScriptReader scriptReader;

    // private string input; 


    // private string[][] acceptableAnswerArray = new string[][] 
    // {
    //     new string[] {"smileyface", "smilingface", "smile", "smilyface", "smileemoji", ":)"},
    //     new string[] {"pac-man", "pacman"},
    //     new string[] {"cat", "kitten", "kitty", "yuumi"},
    //     new string[] {"house", "home", "building"}
    // };

    // private string[] acceptableAnswers;
    // private int indexAnswer;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     indexAnswer = 0;
    //     acceptableAnswers = acceptableAnswerArray[indexAnswer];
    //     image.color = new Color(255, 255, 255, 0);
    // }


    // // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Return))
    //     {   
    //         string noWhiteSpace = inputField.text.Replace(" ", string.Empty);
    //         if (acceptableAnswers.Contains(noWhiteSpace.ToLower())) 
    //         {                
    //             StartCoroutine(FadeImage());
    //         }
    //         inputField.text = "";

    //     }
    // }

    // IEnumerator FadeImage()
    // {
    //     // loop over 1 second
    //     for (float i = 0; i <=0.2f; i += Time.deltaTime)
    //     {
    //         // set color with i as alpha
    //         image.color = new Color(1, 1, 1, i * 5);
    //         yield return null;
    //     }

    //     for (float i = 0; i <=0.5f; i += Time.deltaTime)
    //     {
    //         image.color = new Color(1, 1, 1, 1);
    //         yield return null;
    //     }

    //     for (float i = 0.2f; i >= 0; i -= Time.deltaTime)
    //     {
    //         // set color with i as alpha
    //         image.color = new Color(1, 1, 1, i * 5);
    //         yield return null;
    //     }

    //     UpdateNextScript();
    // }

    // private void UpdateNextScript() 
    // {
    //     indexAnswer++;
    //     acceptableAnswers = acceptableAnswerArray[indexAnswer];
    //     ScriptReader.UpdateIndexLine(0);
    //     ScriptReader.UpdateindexAnswer(indexAnswer);
    // }

    // public void ReadStringInput(string s)
    // {
    //     input = s;
    //     Debug.Log(input);
    // }
//
}