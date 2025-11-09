using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text npcNameText;
    public TMP_Text dialogText;
    public Transform choicesContainer;
    public Button choiceButtonPrefab;
    public Button continueButton;

    private DialogData currentDialog;
    private DialogNode currentNode;

    public void StartDialog(string dialogFileName)
    {
        // JSON 불러오기
        TextAsset json = Resources.Load<TextAsset>("Dialogs/" + dialogFileName);
        if (json == null)
        {
            Logger.LogError("Dialog JSON을 찾을 수 없습니다: " + dialogFileName);
            return;
        }

        currentDialog = JsonUtility.FromJson<DialogData>(json.text);
        ShowNode(0);
    }

    private void ShowNode(int nodeId)
    {
        currentNode = currentDialog.nodes.Find(n => n.id == nodeId);
        if (currentNode == null)
        {
            Logger.LogError("DialogNode를 찾을 수 없습니다: " + nodeId);
            EndDialog();
            return;
        }

        npcNameText.text = currentDialog.npcName;
        dialogText.text = currentNode.text;

        // 이전 버튼 제거
        foreach (Transform child in choicesContainer)
            Destroy(child.gameObject);

        // 선택지 처리
        if (currentNode.choices.Count == 0)
        {
            // 선택지가 없으면 계속 버튼 활성화
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(EndDialog);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
            foreach (var choice in currentNode.choices)
            {
                Button btn = Instantiate(choiceButtonPrefab, choicesContainer);
                btn.GetComponentInChildren<Text>().text = choice.text;
                btn.onClick.AddListener(() => ShowNode(choice.nextId));
            }
        }
    }

    private void EndDialog()
    {
        npcNameText.text = "";
        dialogText.text = "";
        foreach (Transform child in choicesContainer)
            Destroy(child.gameObject);
        continueButton.gameObject.SetActive(false);
    }
}
