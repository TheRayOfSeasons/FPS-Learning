using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    public GameObject panel;

    void Awake()
    {
        instance = this;
    }

    public void ToggleDiedScreen(bool toggle)
    {
        this.panel.SetActive(toggle);
    }
}
