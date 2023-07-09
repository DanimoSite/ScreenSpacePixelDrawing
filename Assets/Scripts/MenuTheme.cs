using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTheme : MonoBehaviour
{
    private static MenuTheme menuTheme;
    void Awake()
    {
        if (menuTheme == null)
        {
            menuTheme = this;
        }
    }
}
