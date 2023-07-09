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
            "When you're ready hit the right arrow key on your keyboard to view the next step!",
            "You can hit the left arrow key to go back a step. Let's draw a circle in the middle of your canvas.",
            "Then, draw a right-ward facing arrow in the middle of the circle, like a greater-than sign!",
            "Above the arrow, to both the left and the right of it, draw two small vertical lines.",
            "Below the arrow, draw a wide 'U' shape. If you mess up, make sure to use the eraser tool!",
            "Now, write down what you've drawn in the textbox below!"
        },
        new string[] 
        {
            "We're going to start out with a familiar dish. Draw a pizza with one slice removed.", 
            "Oh no, the pepperoni fell off! Draw a line of small circles moving away from where you removed the slice.",
            "Everyone can cook, and everyone can game! This one's as easy as pizza pie."
        },
        new string[] 
        {
            "Hey there champ, thanks for spending some time with your old man. Let's put a square in the center of the page, like we used to.", 
            "Your pops likes to keep it simple, so let's just add a horizontal line right on top of that square, like the shelf I put up for your mom way back when.",
            "I always get up early, so let's add a semi-circle on-top of the shelf from one end to the other so it looks like a sunrise.",
            "Your mom was a big fan of polka dots, so we gotta add some small circles inside that semi-circle. She woulda loved that.",
            "Thanks for spending time with me, kiddo. We don't have many moments like these nowadays. You're a good kid."
        },
        new string[] 
        {
            "Alright everybody, start off by drawing the circumference of a circle in the center of your page.", 
            "Now put two isosceles triangles on top of the circle, one on the right and one on the left.",
            "One more triangle, this time an upside-down equilateral triangle right in the middle of your circle.",
            "Good, now draw two curved lines coming out from the lowest tip of that triangle, facing in opposite directions.",
            "Right above the other two vertices of that triangle, draw two small circles.",
            "We're almost done with this lecture! Put an equal sign on both sides of the equilateral triangle.",
            "Guessing what I've described is left as an exercise for the drawer."
        },
        new string[] 
        {
            "I'm gonna have you fixin' up a great drawin' in no time pardner. First draw me a rectangular pen for my horse.",
            "Put one of them big ol' triangles on top, like the head of one of my huntin' arrows.",
            "Add a lil' vertical rectangle inside the pen. Make sure it's touchin' the bottom side.",
            "Hey, that almost looks like a mouth. Y'all gotta add some square eyes now.",
            "I like to use haybales for target practice, so let's put a cross-shaped target in each of those eyes.",
            "And we're done before high noon! Thanks for the help, buckaroo."
        },
        new string[] 
        {
            "Hey, my boss has me on a tight deadline here, so let's make this quick. Draw a long rectangle on your page, like a bill.",
            "You can't make dollars if you don't make cents - Add two circular coins on the bottom of the rectangle, close to each corner.",
            "I'm gonna set my coffee on top of this bill so it doesn't fly away, so there's now a quarter circle in the top right corner.",
            "Gotta make some notes on your performance, so I'll put some sticky notes in a line along my bill here.",
            "Are we done here? My boss will fire me if I miss my next meeting."
        },
        new string[] 
        {
           "Hey you, make sure you have your hard hat on! Can't you see the big, rectangular steel beam stood up in the middle of the page?",
           "We gotta have something to build on, so draw a horizontal platform on top of that beam.",
           "Arches are structurally sound, so put an arch on top of the left side of that platform, facing inward.",
           "Gotta support that arch, so add an L-beam from the top of that arch to the other end of the platform. That's a line to the right, then a line down.",
           "A flag will let us know which way the wind is blowing, so put one right above the arch.",
           "Let's add the finishing touches - put any three-digit number between the arch and the end of your L-Beam.",
           "There we go, another project done and dusted!"
        },
        new string[] 
        {
            "Alright campers! Let's get this tent pitched by drawing two diagonal lines that touch at the top. Make sure it's close to the ground.",
            "Now we gotta keep the animals out. Draw an anti-bear circle around the bottom of your tent!",
            "Here at Camp Dictionary, we always put two circular rocks touching the south-east and south-west ends of our anti-bear circle for good luck!",
            "Who's playing with a flashlight in the tent? I can see a long vertical line coming right out from the top of the tent!",
            "Oh wow, that light's hitting the full-moon. Draw the moon at the end of that line.",
            "Darn, those flat clouds are right in the way. They're forming a horizontal line right between our tent and the moon.",
            "Have a great sleep, campers - I don't wanna see anyone on their phones tonight!"
        },
        new string[] 
        {
            "Let's make this quick; it's arms day. I've put my water bottle in the middle of the page, so it looks like a small circle from above.",
            "Gotta start with some lifting, so let's draw three weight plates in a triangle around that bottle. Those are circles with circles in them, if you don't lift bro.",
            "I always like to end it with some half-crunches, so put a curved line connecting each of those plates to each other.",
            "What a workout! I could crush a protein shake right now."
        },
        new string[] 
        {
            "Hey, thanks for helping me set up the party tonight! Let's draw a rectangular table down the middle of the page.",
            "Huh, I only have triangular chairs. That's fine, I'll put two of them near the bottom of the table on opposite sides with the tips facing away from the table.",
            "Oh, right, we have more guests than that. Let's put another triangular chair at the top of the table as well.",
            "I have some wavy streamers too! Let's attach them to the end of the table without a chair.",
            "Oh, everyone's almost here, let's put out a circular bowl near the top of the table so that I can put some chips in later.",
            "Thanks for helping me set-up! Let's have a rocking party!"
        },
        new string[] 
        {
            "Let me seat you at your table, monsieur. It's the big circle in the center of your page.",
            "A table for four? I see. I've tucked in four circular chairs around the table, equally spaced.",
            "Pardon me, I forgot to put the checkered tablecloth on top of the table.",
            "Oh, you have another guest coming. That's no problem, I'll put one more chair between two of the chairs.",
            "My apologies, but there appears to be two circular stains next to each other near the edge on that last chair. We are deeply sorry.",
            "Oh, you have one more guest? That's fine, but we only have triangular chairs left. I'll put one on the opposite side from the last chair.",
            "Thank you for dining with us! I will return once you've decided what you'll be having tonight."
        }
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
        new string[] {"pacman", "pac-man"},
        new string[] {"mushroom", "toadstool", "toad", "shroom", "fungus"},
        new string[] {"cat", "kitten", "kitty", "yuumi"},
        new string[] {"house", "home", "building"},
        new string[] {"bus", "schoolbus", "citybus", "transitbus"},
        new string[] {"mailbox", "postbox", "pobox", "p.o.box"},
        new string[] {"skateboarder", "skateboarding", "tonyhawk", "skateboard"},
        new string[] {"fidgetspinner", "fidgettoy", "fidget", "toy"},
        new string[] {"rocket", "rocketship", "spaceship"},
        new string[] {"turtle", "seaturtle", "tortoise", "squirtle"}
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