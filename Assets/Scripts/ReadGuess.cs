using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ReadGuess : MonoBehaviour
{
    [SerializeField]
    private InputField inputField; 

    private string input; 

    private string[][] acceptableAnswerArray = new string[][] 
    {
        new string[] {"smileyface", "smilingface", "smile", "smilyface", "smileemoji", ":)"},
        new string[] {"pac-man", "pacman", "pac man"}
    };

    private string[] acceptableAnswers;

    // Start is called before the first frame update
    void Start()
    {
        acceptableAnswers = acceptableAnswerArray[0];
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {   
            string noWhiteSpace = inputField.text.Replace(" ", string.Empty);
            if (acceptableAnswers.Contains(noWhiteSpace.ToLower())) 
            {
                Debug.Log("correct");
            }
            inputField.text = "";

        }
    }



    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }
}
