using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ButtonBase
{
    [SerializeField] private GameObject panelElements;
    [SerializeField] private GameObject upgradePanel;
    private GameManager gameManager;
    private bool gameStarted;

    public override void Init()
    {
        base.Init();

        gameManager = GameManager.Instance;
        gameStarted = false;
        if (upgradePanel != null) panelElements.SetActive(true);
    }

    public override void DeInit()
    {
        panelElements.SetActive(false);
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            panelElements.SetActive(false);
            return;
        }
        if (!gameStarted)
        {
            panelElements.SetActive(false);
            gameManager.OnStartTheGame();
            gameStarted = true;
            return;
        }

        panelElements.SetActive(false);
    }
}
