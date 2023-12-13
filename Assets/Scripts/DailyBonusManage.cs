using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusManage : MonoBehaviour
{
    [SerializeField] private DailyBonusSettings settings;

    [SerializeField] private GameObject dailyBonusView;

    [SerializeField] private GameObject mainDailyBonusView;

    [SerializeField] private Transform rewardsButtonsContainer;
    [SerializeField] private int rewardsCount = 6;
    [SerializeField] private DailyBonusButton rewardButtonPrefab;

    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressLabel;

    [SerializeField] private Toggle dailyCheck;

    [SerializeField] private GameObject progressCompleteView;
    [SerializeField] private TextMeshProUGUI progressCompleteRewardCountLabel;

    [SerializeField] private TextMeshProUGUI timerLabel;

    private DateTime nextBonusTime = DateTime.MinValue;

    private int progressCompleteRewardCount = 30;
    private int progressCount = 0;

    private List<DailyBonusButton> buttons = new List<DailyBonusButton>();

    [HideInInspector] public GlobalEventManager eventManager;

    void Start()
    {
        eventManager.OnUserBonusTimeIsCame += UnlockButtonByProgress;

        progressCompleteRewardCount = settings.ticketsCountForProgressComplete;

        mainDailyBonusView.SetActive(true);
        progressCompleteView.SetActive(false);

        ProgressReset();

        dailyCheck.isOn = false;

        CreateRewardsButtons();
    }

    private void Update()
    {
        if (nextBonusTime > DateTime.Now)
        {
            if ((nextBonusTime.Subtract(DateTime.Now)).Days < 1)
            {
                TimeSpan dateTime = nextBonusTime - DateTime.Now;
                timerLabel.text = dateTime.ToString(@"hh\:mm\:ss");
            }
            else
            {
                timerLabel.text = $"> {(nextBonusTime.Subtract(DateTime.Now)).Days} " + ((nextBonusTime.Subtract(DateTime.Now)).Days % 10 == 1 ? "day" : "days");
            }
        }
        else
        {
            dailyCheck.isOn = false;
            timerLabel.text = "NOW!";
        }
    }

    public void OpenBonusView()
    {
        dailyBonusView.SetActive(true);

        mainDailyBonusView.SetActive(true);
        progressCompleteView.SetActive(false);
    }

    public void CLoseBonusView()
    {
        dailyBonusView.SetActive(false);
    }

    private void CreateRewardsButtons()
    {
        int count = 0;

        for(int i = 0; i < rewardsCount; i++)
        {
            DailyBonusButton button = Instantiate(rewardButtonPrefab, rewardsButtonsContainer);

            if(i % 2 == 0) count++;

            button.Setup(i + 1, settings.ticketsCountMultiplier * count, this);
            buttons.Add(button);
        }

        UnlockButtonByProgress();
    }

    public void SendRewardButtonPressed(int rewardCount)
    {
        AddProgressValue();

        dailyCheck.isOn = true;

        eventManager.SendRewardActivated(rewardCount);
    }

    private void AddProgressValue()
    {
        if (progressCount < progressSlider.maxValue) progressCount++;
        
        if (progressCount == progressSlider.maxValue) ProgressComplete();

        UpdateUserBonusTime();
        UpdateProgress();
    }

    private void UpdateUserBonusTime()
    {
        DateTime newData = DateTime.Now.AddDays(settings.days).AddHours(settings.hours).AddMinutes(settings.minutes).AddSeconds(settings.seconds);
        nextBonusTime = newData;

        eventManager.SendUpdateUserBonusTime(newData);
    }

    private void UnlockButtonByProgress()
    {
        if(buttons.Count > 0)
        {
            buttons[progressCount - 1].SetLockStatus(false);
        }
    }

    private void UpdateProgress()
    {
        progressSlider.value = progressCount;
        progressLabel.text = progressCount + "/" + progressSlider.maxValue;
    }

    private void ProgressComplete()
    {
        mainDailyBonusView.SetActive(false);
        progressCompleteView.SetActive(true);

        progressCompleteRewardCountLabel.text = "x" + progressCompleteRewardCount.ToString();

        eventManager.SendRewardActivated(progressCompleteRewardCount);

        ProgressReset();
    }

    private void ProgressReset()
    {
        progressSlider.minValue = 1;
        progressSlider.maxValue = rewardsCount + 1;

        progressCount = (int)progressSlider.minValue;
        UpdateProgress();

        if (buttons.Count > 0)
        {
            foreach (DailyBonusButton dailyRewardButton in buttons)
            {
                dailyRewardButton.SetLockStatus(true);
                dailyRewardButton.InteractableStatus(true);
            }
        }
    }
}
