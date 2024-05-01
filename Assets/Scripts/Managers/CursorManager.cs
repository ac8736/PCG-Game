using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public Texture2D m_MenuCursorTexture;
    public Texture2D m_GameCursorTexture;
    public Vector2 m_HotSpot = Vector2.zero;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "StartScene" || currentScene.name == "GameWin" || currentScene.name == "GameOver" || currentScene.name == "LevelToDeathTransition")
            Cursor.SetCursor(m_MenuCursorTexture, m_HotSpot, CursorMode.Auto);
        if (currentScene.name == "Level" || currentScene.name == "Death")
            Cursor.SetCursor(m_GameCursorTexture, m_HotSpot, CursorMode.Auto);
    }
}
