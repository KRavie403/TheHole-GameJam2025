using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{

    [Header("Dissolve Object")]
    [SerializeField] private GameObject dissolveObject; // DissolveUI가 붙어있는 오브젝트

    private UIDissolve.DissolveUI dissolveUIComponent;

    private void Start()
    {
        if (dissolveObject != null)
        {
            // 오브젝트에서 DissolveUI 컴포넌트 가져오기
            dissolveUIComponent = dissolveObject.GetComponent<UIDissolve.DissolveUI>();

            if (dissolveUIComponent != null)
            {
                // 시작할 때 비활성화 상태로 두기
                dissolveUIComponent.enabled = false;
            }
            else
            {
                Logger.LogWarning("DissolveUI 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Logger.LogWarning("dissolveObject가 할당되지 않았습니다.");
        }
    }

    public void DissolveActive()
    {
        if (dissolveUIComponent != null)
        {
            dissolveUIComponent.enabled = true; // 활성화
        }
    }
}
