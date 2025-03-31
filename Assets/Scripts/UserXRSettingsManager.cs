using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class UserXRSettingsManager : MonoBehaviour
{
    // UI dropdowns for movement and turning styles (Assigned in the Unity Inspector)
    public TMP_Dropdown movementDropdown;
    public TMP_Dropdown turnDropdown;

    // References to the controllers that apply movement and turning styles
    public SetMovementType movementController;
    public SetTurnType turnController;

    private void Start()
    {
        // Load saved user settings when the game starts
        LoadUserXRSettingsUI();
    }

    // Called when the user selects a movement style from the dropdown
    public void ChangeMovementStyle(int index)
    {
        // Update the user's movement style setting
        UserXRSettings.MovementStyle = index;

        // Apply the selected movement style immediately
        movementController.SwitchMovement(index);
    }

    // Called when the user selects a turn style from the dropdown
    public void ChangeTurnStyle(int index)
    {
        // Update the user's turn style setting
        UserXRSettings.TurnStyle = index;

        // Apply the selected turn style immediately
        turnController.SetTypeFromIndex(index);
    }

    // Saves the selected movement and turn styles to PlayerPrefs for persistence
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("XR_MovementStyle", UserXRSettings.MovementStyle);
        PlayerPrefs.SetInt("XR_TurnStyle", UserXRSettings.TurnStyle);
        PlayerPrefs.Save();

        Debug.Log("XR Settings Saved!");
    }

    // Loads saved movement and turn styles from PlayerPrefs and applies them
    public void LoadUserXRSettingsUI()
    {
        // Load Movement Style
        if (PlayerPrefs.HasKey("XR_MovementStyle"))
        {
            UserXRSettings.MovementStyle = PlayerPrefs.GetInt("XR_MovementStyle");
            movementDropdown.value = UserXRSettings.MovementStyle;
            movementController.SwitchMovement(UserXRSettings.MovementStyle);
        }

        // Load Turn Style
        if (PlayerPrefs.HasKey("XR_TurnStyle"))
        {
            UserXRSettings.TurnStyle = PlayerPrefs.GetInt("XR_TurnStyle");
            turnDropdown.value = UserXRSettings.TurnStyle;
            turnController.SetTypeFromIndex(UserXRSettings.TurnStyle);
        }

        Debug.Log("XR Settings Loaded!");
    }
}
