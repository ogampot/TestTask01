using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateSwitch : MonoBehaviour
{
    [SerializeField] private Image buttonImage;

    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    [SerializeField] private bool isOn = true;
    public bool IsOn { get { return isOn; } }

    public Action<bool> OnSwitchEvent;

    private void Start()
    {
        SpriteUpdate();
    }

    public void SwitchButtonState()
    {
        if(isOn == true) isOn = false;
        else isOn = true;

        SpriteUpdate();
    }

    private void SpriteUpdate()
    {
        if(isOn == true) buttonImage.sprite = onSprite;
        else buttonImage.sprite = offSprite;

        OnSwitchEvent?.Invoke(isOn);
    }
}
