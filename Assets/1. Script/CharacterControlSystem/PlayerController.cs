using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 플레이어 캐릭터의 이동, 인벤토리, 저장 로직을 관리
/// </summary>
public class PlayerController : MonoBehaviour
{
    public List<string> inventory = new List<string>();

    public void Start()
    {
        //// 저장된 데이터 불러오기
        //var data = SaveSystem.LoadPlayer();
        //transform.position = data.position;
        //inventory = data.inventory;

        //Logger.Log("[PlayerController] 플레이어 데이터 로드 완료.");
    }

    public void LoadFromData(PlayerData data)
    {
        transform.position = data.position;
        inventory = data.inventory ?? new List<string>();
    }
}
