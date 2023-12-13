using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManage : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private List<LevelButton> levelButtons;

    [HideInInspector] public GlobalEventManager eventManager;

    private int userLevel = 0;

    private void Start()
    {
        eventManager.OnUserLevelCountSent += GetUserLevelCount;
        eventManager.SendNeedCheckUserLevelCount();

        if (levelButtons.Count > 0)
        {
            line.positionCount = levelButtons.Count + 1;

            for (int i = 0; i < levelButtons.Count; i++)
            {
                bool checkLevel = i > (userLevel - 1);

                levelButtons[i].Setup(i + 1, eventManager);

                line.SetPosition(i + 1, levelButtons[i].gameObject.transform.position);
            }
        }
    }

    private void GetUserLevelCount(int level)
    {
        userLevel = level;

        UpdateLevels();
    }

    private void UpdateLevels()
    {
        if (levelButtons.Count > 0)
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                bool checkLevel = i > (userLevel - 1);

                levelButtons[i].SetLockStatus(checkLevel);
            }
        }
    }
}
