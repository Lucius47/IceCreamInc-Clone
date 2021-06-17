using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Slider progressBar;

    [SerializeField]
    Text levelText;

    [SerializeField]
    GameObject levelCompletedPopup;

    [SerializeField]
    Text matchPercentageText;

    [SerializeField]
    IceCreamMachine machine;

    [SerializeField]
    LevelManager lvlManager;

    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    Slider durationOfPiecesTween;

    [SerializeField]
    Slider speedOfMachine;


    void Start()
    {
        machine.OnLevelCompleted += LevelCompleted;
        lvlManager.OnNewLevelLoaded += NewLevelLoaded;

        durationOfPiecesTween.minValue = 1f;
        durationOfPiecesTween.maxValue = 5f;
        durationOfPiecesTween.value = 2.5f;

        speedOfMachine.minValue = 1f;
        speedOfMachine.maxValue = 5f;
        speedOfMachine.value = 1.5f;
    }


    void Update()
    {
        progressBar.value = machine.NormalizedT;
    }


    void LevelCompleted()
    {
        levelCompletedPopup.gameObject.SetActive(true);
        matchPercentageText.text = lvlManager.CalculateMatchPercentage() + "% matched.";
    }


    void NewLevelLoaded()
    {
        levelCompletedPopup.gameObject.SetActive(false);
        levelText.text = "Level " + lvlManager.CurrentLevel;
    }

    public void SettingsMenuOpened()
    {
        settingsMenu.SetActive(true);
    }

    public void SettingsMenuClosed()
    {
        settingsMenu.SetActive(false);
        machine.durationOfFallingPiece = durationOfPiecesTween.value;
        machine.speed = speedOfMachine.value;
    }

    public void SettingsReset()
    {
        durationOfPiecesTween.value = 2.5f;
        speedOfMachine.value = 1.5f;
    }

}
