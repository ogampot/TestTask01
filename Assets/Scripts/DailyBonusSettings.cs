using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Daily Bonus Settings", menuName = "Daily Bonus Settings")]
public class DailyBonusSettings : ScriptableObject
{
    public int ticketsCountMultiplier = 5;
    public int ticketsCountForProgressComplete = 30;

    [Header("Available again after: ")]
    public int seconds = 10;
    public int minutes = 0;
    public int hours = 0;
    public int days = 0;
}
