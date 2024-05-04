using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public PlayerStatScriptableObject m_PlayerStat;
    public EnemyStats m_EnemyStat;

    private float speed = 3f;
    private float minX, maxX;

    // Start is called before the first frame update
    public void PlayGame()
    {
        GlobalVars.clearedFloors = 0;
        GlobalVars.floor = 0;
        m_PlayerStat.m_Gold = 0;
        m_PlayerStat.m_MaxHealth = 6;
        m_PlayerStat.m_Speed = 0;
        m_PlayerStat.m_AttackSpeed = 0;
        m_PlayerStat.m_AmmoCount = 10;

        m_EnemyStat.m_MaxHealthChase = 2;
        m_EnemyStat.m_MaxHealthSingle = 3;
        m_EnemyStat.m_MaxHealthSpin = 3;
        m_EnemyStat.m_Damage = 10;
        SceneManager.LoadScene("Level");
    }

    public void Retry()
    {
        GlobalVars.clearedFloors = 0;
        GlobalVars.floor = 0;
        PlayGame();
    }

    // Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application QUIT");
    }

    private void Start()
    {
        SetMovementBounds();
    }

    private void Update()
    {
        gameObject.transform.Translate(speed * Time.deltaTime * Vector3.right);

        if (transform.position.x > maxX || transform.position.x < minX)
        {
            speed *= -1;

            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

    }

    private void SetMovementBounds()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera.orthographic)
        {
            float screenHalfWidthInWorldUnits = mainCamera.orthographicSize * Screen.width / Screen.height;
            minX = mainCamera.transform.position.x - screenHalfWidthInWorldUnits;
            maxX = mainCamera.transform.position.x + screenHalfWidthInWorldUnits;
        }
        else
        {
            Debug.LogError("Camera is not orthographic, script requires an orthographic camera to function properly.");
        }
    }
}
