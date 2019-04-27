using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.Users;
using UnityEngine.Experimental.Input.Plugins.UI;

public class PlayerManager : MonoBehaviour
{
    public UIActionInputModule uiInputModule;

    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;

        InputUser.onChange += OnUserChange;
        InputUser.onUnpairedDeviceUsed += OnUnpairedInputDeviceUsed;
    }

    public void OnStartGameButton() {
        Debug.Log("Clicked");
        var playerDevice = uiInputModule.submit.action.lastTriggerControl.device;
        InputUser.PerformPairingWithDevice(playerDevice);
        Debug.Log(InputUser.all);
    }

    void OnDestroy() {
        InputUser.onChange -= OnUserChange;
        InputUser.onUnpairedDeviceUsed -= OnUnpairedInputDeviceUsed;

        instance = null;
    }

    private void OnUserChange(InputUser user, InputUserChange change, InputDevice device) {

    }

    private void OnUnpairedInputDeviceUsed(InputControl control) {

    }
}
