using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyAction
{
    Forward = 0, Backward, Left, Right,
    Jump, Interact, Inventory,
    Skip, KeyGuide,
    Menu,
    KeyCount
}

public static class KeySetting
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();
}


public class KeyInputMapper : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[]
    {
        KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
        KeyCode.Space,  KeyCode.F, KeyCode.I,
        KeyCode.T, KeyCode.BackQuote,
        KeyCode.Q,
        KeyCode.None
    };

    private void Awake()
    {
        for (int i = 0; i < (int)KeyAction.KeyCount; i++)
        {
            // 이미 있으면 덮어쓰고, 없으면 추가
            KeySetting.keys[(KeyAction)i] = defaultKeys[i];
        }
        Logger.Log("===== KeySetting.keys 초기화 완료 =====");
        foreach (var kvp in KeySetting.keys)
        {
            Logger.Log($"KeyAction: {kvp.Key}, KeyCode: {kvp.Value}");
        }
    }

    public void TargetKeySetting(KeyAction KeyType, KeyCode keyCode)
    {
        KeySetting.keys[KeyType] = keyCode;
    }

}
