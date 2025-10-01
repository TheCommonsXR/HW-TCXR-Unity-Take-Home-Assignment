using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameModeSelectorUI : MonoBehaviour
{
    [Header("References")]
    public TMP_Dropdown dropdown;               
    public GameModeManager gameModeManager;     
    public GameMode[] availableModes;          

    void Start()
    {
     
        dropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();
        foreach (var mode in availableModes)
        {
            options.Add(mode.modeName);
        }
        dropdown.AddOptions(options);

        dropdown.onValueChanged.AddListener(OnDropdownChanged);

        if (availableModes.Length > 0)
        {
            dropdown.value = 0;
            OnDropdownChanged(0);
        }
    }

    void OnDropdownChanged(int index)
    {
        if (index >= 0 && index < availableModes.Length)
        {
            gameModeManager.currentMode = availableModes[index];
            gameModeManager.ApplyGameMode();
        }
    }
}
