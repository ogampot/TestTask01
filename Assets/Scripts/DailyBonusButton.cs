using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayLabel;
    [SerializeField] private Image ticketImage;
    [SerializeField] private TextMeshProUGUI countLabel;

    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;

    [SerializeField] private Image lockImage;

    private int rewardCount = 1;
    public int RewardCount { get { return rewardCount; } }

    private DailyBonusManage bonusManager;

    private bool isLocked = false;
    public bool IsLocked { get { return isLocked; } }

    private bool isActivated = false;
    public bool IsActivated { get { return isActivated; } }

    public void Setup(int day, int count, DailyBonusManage bonusManager)
    {
        dayLabel.text = "Day " + day.ToString();

        rewardCount = count;
        countLabel.text = "x" + rewardCount.ToString();

        this.bonusManager = bonusManager;

        button = GetComponent<Button>();
        button.onClick.AddListener(RewardButtonAction);

        SetLockStatus(true);
    }

    private void RewardButtonAction()
    {
        bonusManager.SendRewardButtonPressed(rewardCount);

        InteractableStatus(false);
    }

    public void InteractableStatus(bool interactable)
    {
        button.interactable = interactable;
    }

    public bool CheckLockStatus()
    {
        if(isLocked == true)
        {
            lockImage.enabled = true;

            dayLabel.enabled = false;
            ticketImage.enabled = false;
            countLabel.enabled = false;
            button.enabled = false;
            buttonImage.raycastTarget = false;
        }
        else
        {
            lockImage.enabled = false;

            dayLabel.enabled = true;
            ticketImage.enabled = true;
            countLabel.enabled = true;
            button.enabled = true;
            buttonImage.raycastTarget = true;
        }

        return isLocked;
    }

    public void SetLockStatus(bool status)
    {
        isLocked = status;

        CheckLockStatus();
    }
}
