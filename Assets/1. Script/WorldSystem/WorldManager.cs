using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    [Header("사라질 오브젝트")]
    [SerializeField] private List<GameObject> vanishObjects = new();

    [Header("시간 설정")]
    [SerializeField] private float vanishInterval = 5f;
    public float VanishInterval
    {
        get => vanishInterval;
        set => vanishInterval = value;
    }

    [Header("오디오 제거 시점 (몇 번째 오브젝트에서 끌지)")]
    [SerializeField] private int audioStopIndex = 13;

    private float timer;
    private int currentIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Update()
    {
        if (currentIndex >= vanishObjects.Count)
            return;

        timer += Time.deltaTime;

        if (timer >= vanishInterval)
        {
            timer -= vanishInterval;
            VanishNext();
        }
    }

    private void VanishNext()
    {
        if (currentIndex >= vanishObjects.Count)
            return;

        // 오브젝트 제거
        GameObject obj = vanishObjects[currentIndex];

        if (obj != null)
        {
            Destroy(obj);
            //obj.SetActive(false);
            Logger.Log($"Vanish: {obj.name}", this);
        }

        // 특정 타이밍에 오디오 제거
        if (currentIndex == audioStopIndex)
        {
            AudioManager.Inst.VanishBGM();
            AudioManager.Inst.VanishEffect();
        }

        currentIndex++;
    }

    public void RegisterVanishObject(GameObject obj)
    {
        if (obj != null && !vanishObjects.Contains(obj))
        {
            vanishObjects.Add(obj);
        }
    }
}