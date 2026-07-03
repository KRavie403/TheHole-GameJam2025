using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        AudioListener.pause = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() =>
        {
            GameSceneManager.Inst.LoadSceneDelayed("1. InGame");
        });
    }

}
