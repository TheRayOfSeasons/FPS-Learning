using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    public int enemyCount = 0;

    public void IncrementEnemyCount()
    {
        this.enemyCount++;
    }

    public void DecrementEnemyCount()
    {
        this.enemyCount--;
        if (this.enemyCount <= 0)
        {
            this.Win();
        }
    }

    public void Win()
    {
        UIManager.Instance.ToggleWinScreen(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
