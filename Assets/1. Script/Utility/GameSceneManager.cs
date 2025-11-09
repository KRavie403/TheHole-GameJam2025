using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : Singleton<GameSceneManager>
{

    public static GameSceneManager Instance;


    // 씬 5초 뒤 전환
    public void LoadSceneDelayed(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(sceneName);
    }

    // 씬 번호로 전환
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // 현재 씬 재로딩
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // 비동기 씬 로드
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            // 진행률 확인 가능 (0~0.9까지 로딩)
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Logger.Log("로딩 진척도: " + (progress * 100) + "%");
            yield return null;
        }
    }
}
