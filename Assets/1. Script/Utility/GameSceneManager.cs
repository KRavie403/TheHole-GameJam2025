using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : Singleton<GameSceneManager>
{
    private bool _isLoading = false;

    public void LoadSceneDelayed(string sceneName)
    {
        if (_isLoading) return;

        _isLoading = true;

        AudioManager.Inst.PlayUIEffect(0);
        LoadSceneDelayed(sceneName, 5f); // 기본 5초
    }

    /// <summary>
    /// 지정한 시간 후 씬 전환
    /// </summary>
    public void LoadSceneDelayed(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);

        _isLoading = false;
    }

    /// <summary>
    /// 씬 번호로 전환
    /// </summary>
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// 씬 이름으로 즉시 전환
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 현재 씬 다시 로드
    /// </summary>
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    /// <summary>
    /// 비동기 씬 로드
    /// </summary>
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
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Logger.Log("로딩 진척도: " + (progress * 100) + "%");
            yield return null;
        }
    }
}