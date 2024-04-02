using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public Texture2D m_MenuCursorTexture; 
    public Vector2 m_HotSpot = Vector2.zero;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "StartScene")
            Cursor.SetCursor(m_MenuCursorTexture, m_HotSpot, CursorMode.Auto);
    }
}
