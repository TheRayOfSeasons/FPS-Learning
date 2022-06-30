using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    public GameObject panel;
    public GameObject winPanel;
    public Text ammoCount;
    public Text playerHealth;

    void Awake()
    {
        instance = this;
    }

    public void ToggleDiedScreen(bool toggle)
    {
        this.panel.SetActive(toggle);
    }

    public void ToggleWinScreen(bool toggle)
    {
        this.winPanel.SetActive(toggle);
    }

    public void SetAmmo(int count)
    {
        this.ammoCount.text = $"Ammo: {count}";
    }

    public void SetPlayerHealth(float health)
    {
        this.playerHealth.text = $"Health: {health}";
    }
}
