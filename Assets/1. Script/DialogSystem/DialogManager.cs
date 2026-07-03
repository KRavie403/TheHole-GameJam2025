using System.Collections;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public InGameUIManager uiManager;

    private bool skipPressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            skipPressed = true;
        }
    }

    public IEnumerator PlayDialog(DialogData dialogData)
    {
        foreach (var node in dialogData.nodes)
        {
            uiManager.SetDialog(dialogData.npcName, node.text);

            float timer = 0f;
            while (timer < node.duration)
            {
                if (Input.GetKeyDown(KeyCode.T)) break;
                timer += Time.deltaTime;
                yield return null;
            }

            uiManager.ClearDialog();
        }
    }
}
