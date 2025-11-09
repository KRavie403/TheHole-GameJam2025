using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 플레이어 데이터를 담는 클래스 (위치, 인벤토리 등)
/// </summary>
[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public List<string> inventory = new List<string>();
}

/// <summary>
/// 간단한 저장 시스템 (WebGL / Standalone 공용)
/// </summary>
public static class SaveSystem
{
    private const string PlayerDataKey = "PlayerData"; // 저장 키 이름 (브라우저 LocalStorage에 사용됨)

    /// 플레이어의 현재 상태를 저장
    public static void SavePlayer(Vector3 position, List<string> inventory)
    {
        // PlayerData 생성 및 값 복사
        PlayerData data = new PlayerData
        {
            position = position,
            inventory = new List<string>(inventory) // 복사본 저장 (참조 방지)
        };

        // JSON 변환 및 저장
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PlayerDataKey, json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 저장된 플레이어 데이터를 불러오기
    /// 저장 데이터가 없으면 기본값을 반환
    /// </summary>
    public static PlayerData LoadPlayer()
    {
        if (!PlayerPrefs.HasKey(PlayerDataKey))
        {
            Logger.Log("[SaveSystem] 저장된 데이터가 없습니다. 기본값을 반환합니다.");
            return new PlayerData();
        }

        string json = PlayerPrefs.GetString(PlayerDataKey);
        return JsonUtility.FromJson<PlayerData>(json) ?? new PlayerData();
    }

    /// <summary>
    /// 저장된 데이터 삭제
    /// </summary>
    public static void ClearData()
    {
        PlayerPrefs.DeleteKey(PlayerDataKey);
        PlayerPrefs.Save();
        Logger.Log("[SaveSystem] 저장 데이터가 삭제되었습니다.");
    }
}
