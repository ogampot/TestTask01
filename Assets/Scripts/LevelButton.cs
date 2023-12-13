using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI levelNumberLabel;
    [SerializeField] private Image lockImage;

    private int levelNumber = 1;

    private bool isLocked = true;
    public bool IsLocked { get { return isLocked; } }

    private GlobalEventManager eventManager;

    private void Start()
    {
        CheckLockStatus();
    }

    public void Setup(int level, GlobalEventManager eventManager)
    {
        levelNumber = level;
        levelNumberLabel.text = levelNumber.ToString();

        button.onClick.AddListener(LevelButtonActivate);

        this.eventManager = eventManager;
    }

    public bool CheckLockStatus()
    {
        if (isLocked == true)
        {
            lockImage.enabled = true;

            button.enabled = false;
            levelNumberLabel.enabled = false;
        }
        else
        {
            lockImage.enabled = false;

            button.enabled = true;
            levelNumberLabel.enabled = true;
        }

        return isLocked;
    }

    public void SetLockStatus(bool status)
    {
        isLocked = status;

        CheckLockStatus();
    }

    private void LevelButtonActivate()
    {
        eventManager.SendLevelButtonPressed(levelNumber + 1);
    }
}
