using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    enum LoadMode { Override,Read };
    LoadMode loadMode = LoadMode.Read;

    public GameObject SlotSelectScreen;
    public GameObject MainScreen;

    public Text Slot1Info;
    public Text Slot2Info;
    public Text Slot3Info;


    void Start()
    {
        SaveManager.InitializeSaveSystem();

        UpdateSlotInfo();
        MainScreen.SetActive(true);
        SlotSelectScreen.SetActive(false);
    }

    public void SlotButtonClick(int slot)
    {
        if(loadMode == LoadMode.Override)
        {
            SaveManager.saveManager.NewGame(slot);
        }
        else
        {
            SaveManager.saveManager.LoadGame(slot);
        }
            
    }
    
    public void NewGameButtonOnClick()
    {
        loadMode = LoadMode.Override;
        MainScreen.SetActive(false);
        SlotSelectScreen.SetActive(true);
    }

    public void LoadGameButtonOnClick()
    {
        loadMode = LoadMode.Read;
        MainScreen.SetActive(false);
        SlotSelectScreen.SetActive(true);
    }

    public void ExitGameButtonOnClick()
    {
        if(Application.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }

    void UpdateSlotInfo()
    {
        Slot1Info.text = SaveManager.GetSlotInfo(1);
        Slot2Info.text = SaveManager.GetSlotInfo(2);
        Slot3Info.text = SaveManager.GetSlotInfo(3);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
