using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private bool canUpdateGame;
    private PlayerManager playerManager;

    public void Init()
    {
        canUpdateGame = true;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void DeInit()
    {
        canUpdateGame = false;
    }

    void Update()
    {
        if (!canUpdateGame) return;

        ActionManager.Updater?.Invoke(Time.deltaTime);
        ActionManager.AiUpdater?.Invoke(playerManager.GetCharacterTransform.position);
        CoroutineManager.Tick();
    }
}
