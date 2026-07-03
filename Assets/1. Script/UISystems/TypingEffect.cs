using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text text;
    public float startDelay = 2f;
    public float typingSpeed = 0.1f;
    public float pauseBetweenTexts = 3f;

    [TextArea]
    public string firstText = "Nothing’s left now.";

    [TextArea]
    public string secondText = "Would you still call this a game?";

    // 대화 종료 여부
    public bool IsFinished { get; private set; }

    private void Start()
    {
        Logger.Log("TYPINGEFFECT 시작");
        IsFinished = false;
        StartCoroutine(TypeSequence());
    }

    private IEnumerator TypeText(string message)
    {
        text.text = message;
        text.maxVisibleCharacters = 0;

        for (int i = 1; i <= message.Length; i++)
        {
            text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator TypeSequence()
    {
        yield return new WaitForSeconds(startDelay);

        yield return TypeText(firstText);

        yield return new WaitForSeconds(pauseBetweenTexts);

        text.text = "";
        text.maxVisibleCharacters = 0;

        yield return new WaitForSeconds(pauseBetweenTexts);

        yield return TypeText(secondText);

        // 대화 종료
        IsFinished = true;
    }
}