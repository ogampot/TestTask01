using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    [SerializeField] private ButtonStateSwitch buttonStateSwitch;

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        buttonStateSwitch.OnSwitchEvent += SwitchAudioState;
    }

    public void SwitchAudioState(bool state)
    {
        audioSource.enabled = state;
    }
}
