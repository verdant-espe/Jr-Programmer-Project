using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    // References the color picker
    public ColorPicker ColorPicker;
    
    private void Start()
    {
        // Initializes event numbers for the color picker
        ColorPicker.Init();
        // Changes game object colors to the new color selected
        ColorPicker.onColorChanged += NewColorSelected;
        
        // Pre-selects the saved color in the MainManager when menu is launched.
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }

    public void StartNew()
    {
        // Loads in Main scene
        SceneManager.LoadScene(1);
    }
    
    public void Exit()
    {
        // Saves the last selected color when application exits
        MainManager.Instance.SaveColor();
        
#if UNITY_EDITOR
        // Exits out of play mode
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void NewColorSelected(Color color)
    {
        // Called when the user picks a new color
        MainManager.Instance.TeamColor = color;
    }

    public void SaveColorClicked()
    {
        // Saves the selected color
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        // Loads in the selected color in the scene
        MainManager.Instance.LoadColor();
        
        // Creates an instance of that color
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
