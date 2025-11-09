using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text text;                            // 타이핑할 TMP 텍스트
    public float startDelay = 2f;                   // 시작 지연
    public float typingSpeed = 0.15f;           // 글자 간 딜레이
    public float pauseBetweenTexts = 2f;    // 두 텍스트 사이 정적 시간

    [TextArea]
    public string firstText = "이제 아무것도 남지 않았는데,";
    [TextArea]
    public string secondText = "당신은 여전히 나를 ‘게임’이라 부를 수 있나요?";

    private void Start()
    {
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
        // 시작 대기
        yield return new WaitForSeconds(startDelay);

        // 1번째 텍스트 타이핑
        yield return TypeText(firstText);

        // 정적 시간
        yield return new WaitForSeconds(pauseBetweenTexts);

        // 화면 초기화
        text.text = "";
        text.maxVisibleCharacters = 0;

        // 정적 시간
        yield return new WaitForSeconds(pauseBetweenTexts);
        
        // 2번째 텍스트 타이핑
        yield return TypeText(secondText);
    }
}
