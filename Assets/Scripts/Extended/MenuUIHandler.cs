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
        
        // When the menu loads, set the color picker to the right color
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }

    public void StartNew()
    {
        // Loads in Main scene
        SceneManager.LoadScene(1);
    }
    
    public void Exit()
    {
        // Save the color before exiting
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
        // Handles the color selected for Main scene
        MainManager.Instance.TeamColor = color;
    }

    public void SaveColorClicked()
    {
        // Saves the selected color
        MainManager.Instance.SaveColor();
    }

    public void LoadColorCliked()
    {
        // Loads in the selected color in the scene
        MainManager.Instance.LoadColor();
        
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
