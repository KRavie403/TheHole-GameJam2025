using System.Diagnostics;
using UnityEngine;

public static class Logger
{
    [Conditional("UNITY_EDITOR"), Conditional("DEBUG")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEBUG")]
    public static void Log(string message, Object context)
    {
        UnityEngine.Debug.Log(message, context);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEBUG")]
    public static void LogError(string message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEBUG")]
    public static void LogError(string message, Object context)
    {
        UnityEngine.Debug.LogError(message, context);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEBUG")]
    public static void LogWarning(string message)
    {
        UnityEngine.Debug.LogWarning(message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEBUG")]
    public static void LogWarning(string message, Object context)
    {
        UnityEngine.Debug.LogWarning(message, context);
    }
}