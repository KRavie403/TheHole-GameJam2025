using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public PlayerController player;
    private static bool isCreate = false;

    #region Scene
    private const string LOGIN = "0. Login";
    private const string INGAME = "1. InGame";
    #endregion

    public enum GameState { Login, MatchLobby, Ready, Start, InGame, Over, Result, Reconnect };
    private GameState gameState;

    private void Start()
    {
        // 게임 시작 시 자동으로 데이터 불러오기
        //LoadGame();
    }

    private void Update()
    {
        // 예시: S 키로 저장, L 키로 로드
        if (Input.GetKeyDown(KeyCode.S))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();

        // 테스트: R 키로 데이터 초기화
        if (Input.GetKeyDown(KeyCode.R))
            ResetData();
    }

    /// <summary>
    /// 플레이어 데이터 저장
    /// </summary>
    public void SaveGame()
    {
        SaveSystem.SavePlayer(player.transform.position, player.inventory);
        Logger.Log("[GameManager] 게임 데이터 저장 완료.");
    }

    /// <summary>
    /// 플레이어 데이터 불러오기
    /// </summary>
    public void LoadGame()
    {
        var data = SaveSystem.LoadPlayer();
        player.LoadFromData(data);
        Logger.Log("[GameManager] 게임 데이터 불러오기 완료.");
    }

    /// <summary>
    /// 저장 데이터 초기화
    /// </summary>
    public void ResetData()
    {
        SaveSystem.ClearData();
        Logger.Log("[GameManager] 게임 데이터 초기화 완료.");
    }
}
