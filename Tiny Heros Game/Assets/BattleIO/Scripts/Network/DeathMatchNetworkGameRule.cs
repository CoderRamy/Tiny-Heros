﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMatchNetworkGameRule : IONetworkGameRule
{
    public int endMatchCountDown = 10;
    public int EndMatchCountingDown { get; protected set; }
    public override bool HasOptionBotCount { get { return true; } }
    public override bool HasOptionMatchTime { get { return true; } }
    public override bool HasOptionMatchKill { get { return true; } }
    public override bool HasOptionMatchScore { get { return false; } }
    public override bool ShowZeroScoreWhenDead { get { return false; } }
    public override bool ShowZeroKillCountWhenDead { get { return false; } }
    public override bool ShowZeroAssistCountWhenDead { get { return false; } }
    public override bool ShowZeroDieCountWhenDead { get { return false; } }

    protected bool endMatchCalled;
    protected bool isLeavingRoom;
    protected Coroutine endMatchCoroutine;

    protected override void EndMatch()
    {
        if (!endMatchCalled)
        {
            isLeavingRoom = true;
            endMatchCoroutine = networkManager.StartCoroutine(EndMatchRoutine());
            endMatchCalled = true;
        }
    }

    public override void OnStartServer(BaseNetworkGameManager manager)
    {
        base.OnStartServer(manager);
        endMatchCalled = false;
    }

    public override void OnStopConnection(BaseNetworkGameManager manager)
    {
        base.OnStopConnection(manager);
        isLeavingRoom = false;
        networkManager.StopCoroutine(endMatchCoroutine);
    }

    IEnumerator EndMatchRoutine()
    {
        EndMatchCountingDown = endMatchCountDown;
        while (EndMatchCountingDown > 0)
        {
            yield return new WaitForSeconds(1);
            --EndMatchCountingDown;
        }
        if (isLeavingRoom)
            networkManager.LeaveRoom();
    }

    public override bool RespawnCharacter(BaseNetworkGameCharacter character, params object[] extraParams)
    {
        var targetCharacter = character as CharacterEntity;
        var gameplayManager = GameplayManager.Singleton;
        // In death match mode will not reset score, kill, assist, death
        targetCharacter.Exp = 0;
        targetCharacter.level = 1;
        targetCharacter.statPoint = 0;
        targetCharacter.watchAdsCount = 0;
        targetCharacter.addStats = new CharacterStats();

        return true;
    }
}
