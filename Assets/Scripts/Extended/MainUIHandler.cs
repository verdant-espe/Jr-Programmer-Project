using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    // References instance of the MainUIHandler script
    public static MainUIHandler Instance { get; private set; }
    
    public interface IUIInfoContent
    {
        // Gets the name of game object
        string GetName();
        // Gets game object data
        string GetData();
        // Gets contents the game object has picked up
        void GetContent(ref List<Building.InventoryEntry> content);
    }
    
    // References InfoPopup script
    public InfoPopup InfoPopup;

    // Shows content of currently selected object
    protected IUIInfoContent m_CurrentContent;
    protected List<Building.InventoryEntry> m_ContentBuffer = new List<Building.InventoryEntry>();


    private void Awake()
    {
        Instance = this;
        // Disables InfoPopup on awake
        InfoPopup.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Sets Instance to null when monobehavior is destroyed
        Instance = null;
    }

    private void Update()
    {
        // If m_CurrentContent is equal to null, return
        if (m_CurrentContent == null)
            return; 

        // Shows current content data in InfoPopup
        InfoPopup.Data.text = m_CurrentContent.GetData();
        
        // Clears content
        InfoPopup.ClearContent();
        // Clears content buffer
        m_ContentBuffer.Clear();
        
        // Gets current content and passes through m_ContentBuffer
        m_CurrentContent.GetContent(ref m_ContentBuffer);
        // Loops through each entry
        foreach (var entry in m_ContentBuffer)
        {
            // Sets icon equal to null
            Sprite icon = null;

            // If MainManager Instance is not equal to null, get item icon
            if (MainManager.Instance != null)
                icon = MainManager.Instance.ResourceDatabase.GetItem(entry.ResourceId)?.Icone;
            
            // Adds icon to content
            InfoPopup.AddToContent(entry.Count, icon);
        }
    }

    public void SetNewInfoContent(IUIInfoContent content)
    {
        // If content is equal to null, disable InfoPopup
        if (content == null)
        {
            InfoPopup.gameObject.SetActive(false);
        }
        // Else, set to true and show current information of object
        else
        {
            InfoPopup.gameObject.SetActive(true);
            m_CurrentContent = content;
            InfoPopup.Name.text = content.GetName();
        }
    }

    public void BackToMenu()
    {
        // Sends player back to menu
        SceneManager.LoadScene(0);
    }
}
