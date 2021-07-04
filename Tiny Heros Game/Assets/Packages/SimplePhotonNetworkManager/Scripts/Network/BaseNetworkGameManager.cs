using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public abstract class BaseNetworkGameManager : SimplePhotonNetworkManager
{
    public static new BaseNetworkGameManager Singleton
    {
        get { return SimplePhotonNetworkManager.Singleton as BaseNetworkGameManager; }
    }

    public const string CUSTOM_ROOM_GAME_RULE = "G";

    public BaseNetworkGameRule gameRule;
    protected float updateScoreTime;
    protected float updateMatchTime;
    protected bool startUpdateGameRule;
    public readonly List<BaseNetworkGameCharacter> Characters = new List<BaseNetworkGameCharacter>();
    public float RemainsMatchTime { get; protected set; }
    public bool IsMatchEnded { get; protected set; }
    public float MatchEndedAt { get; protected set; }

    public int CountAliveCharacters()
    {
        var count = 0;
        foreach (var character in Characters)
        {
            if (character == null)
                continue;
            if (!character.IsDead)
                ++count;
        }
        return count;
    }

    public int CountDeadCharacters()
    {
        var count = 0;
        foreach (var character in Characters)
        {
            if (character == null)
                continue;
            if (character.IsDead)
                ++count;
        }
        return count;
    }

    public override void LeaveRoom()
    {
        if (gameRule != null)
            gameRule.OnStopConnection(this);
        base.LeaveRoom();
    }

    public override void Disconnect()
    {
        if (gameRule != null)
            gameRule.OnStopConnection(this);
        base.Disconnect();
    }

    protected virtual void Update()
    {
        if (PhotonNetwork.isMasterClient)
            ServerUpdate();
        if (PhotonNetwork.isNonMasterClientInRoom)
            ClientUpdate();
    }

    protected virtual void ServerUpdate()
    {
        if (gameRule != null && startUpdateGameRule)
            gameRule.OnUpdate();

        if (Time.unscaledTime - updateScoreTime >= 1f)
        {
            int length = 0;
            List<object> objects;
            GetSortedScoresAsObjects(out length, out objects);
            if (isConnectOffline)
                RpcUpdateScores(length, objects.ToArray());
            else
                photonView.RPC("RpcUpdateScores", PhotonTargets.All, length, objects.ToArray());
            updateScoreTime = Time.unscaledTime;
        }

        if (gameRule != null && Time.unscaledTime - updateMatchTime >= 1f)
        {
            RemainsMatchTime = gameRule.RemainsMatchTime;
            if (isConnectOffline)
                RpcMatchStatus(gameRule.RemainsMatchTime, gameRule.IsMatchEnded);
            else
                photonView.RPC("RpcMatchStatus", PhotonTargets.All, gameRule.RemainsMatchTime, gameRule.IsMatchEnded);

            if (!IsMatchEnded && gameRule.IsMatchEnded)
            {
                IsMatchEnded = true;
                MatchEndedAt = Time.unscaledTime;
            }

            updateMatchTime = Time.unscaledTime;
        }
    }

    protected virtual void ClientUpdate()
    {

    }

    public void SendKillNotify(string killerName, string victimName, string weaponId)
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        photonView.RPC("RpcKillNotify", PhotonTargets.All, killerName, victimName, weaponId);
    }

    public NetworkGameScore[] GetSortedScores()
    {
        for (var i = Characters.Count - 1; i >= 0; --i)
        {
            var character = Characters[i];
            if (character == null)
                Characters.RemoveAt(i);
        }
        Characters.Sort();
        var scores = new NetworkGameScore[Characters.Count];
        for (var i = 0; i < Characters.Count; ++i)
        {
            var character = Characters[i];
            var ranking = new NetworkGameScore();
            ranking.viewId = character.photonView.viewID;
            ranking.playerName = character.playerName;
            ranking.score = character.Score;
            ranking.killCount = character.KillCount;
            ranking.assistCount = character.AssistCount;
            ranking.dieCount = character.DieCount;
            scores[i] = ranking;
        }
        return scores;
    }

    public void GetSortedScoresAsObjects(out int length, out List<object> objects)
    {
        length = 0;
        objects = new List<object>();
        var sortedScores = GetSortedScores();
        length = (sortedScores.Length);
        foreach (var sortedScore in sortedScores)
        {
            objects.Add(sortedScore.viewId);
            objects.Add(sortedScore.playerName);
            objects.Add(sortedScore.score);
            objects.Add(sortedScore.killCount);
            objects.Add(sortedScore.assistCount);
            objects.Add(sortedScore.dieCount);
        }
    }

    public void RegisterCharacter(BaseNetworkGameCharacter character)
    {
        if (character == null || Characters.Contains(character))
            return;
        character.RegisterNetworkGameManager(this);
        Characters.Add(character);
    }

    public bool CanCharacterRespawn(BaseNetworkGameCharacter character, params object[] extraParams)
    {
        if (gameRule != null)
            return gameRule.CanCharacterRespawn(character, extraParams);
        return true;
    }

    public bool RespawnCharacter(BaseNetworkGameCharacter character, params object[] extraParams)
    {
        if (gameRule != null)
            return gameRule.RespawnCharacter(character, extraParams);
        return true;
    }

    public void OnUpdateCharacter(BaseNetworkGameCharacter character)
    {
        if (gameRule != null)
            gameRule.OnUpdateCharacter(character);
    }

    public override void OnCreatedRoom()
    {
        ResetGame();
        var customProperties = PhotonNetwork.room.CustomProperties;
        customProperties[CUSTOM_ROOM_GAME_RULE] = gameRule == null ? "" : gameRule.name;
        PhotonNetwork.room.SetCustomProperties(customProperties);
        base.OnCreatedRoom();
    }

    public override void OnOnlineSceneChanged()
    {
        ResetGame();
        var customProperties = PhotonNetwork.room.CustomProperties;
        var gameRuleName = (string)PhotonNetwork.room.CustomProperties[CUSTOM_ROOM_GAME_RULE];
        BaseNetworkGameRule foundGameRule;
        if (BaseNetworkGameInstance.GameRules.TryGetValue(gameRuleName, out foundGameRule))
        {
            gameRule = foundGameRule;
            gameRule.InitialClientObjects();
            if (PhotonNetwork.isMasterClient && !startUpdateGameRule)
            {
                startUpdateGameRule = true;
                gameRule.OnStartServer(this);
            }
        }
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        Characters.Clear();
        var characters = FindObjectsOfType<BaseNetworkGameCharacter>();
        foreach (var character in characters)
        {
            Characters.Add(character);
        }
        if (gameRule != null)
            gameRule.OnMasterChange(this);
        startUpdateGameRule = true;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("OnPhotonPlayerConnected");


        if (!PhotonNetwork.isMasterClient)
            return;

        int length = 0;
        List<object> objects;
        GetSortedScoresAsObjects(out length, out objects);
        photonView.RPC("RpcUpdateScores", newPlayer, length, objects.ToArray());
        if (gameRule != null)
            photonView.RPC("RpcMatchStatus", newPlayer, gameRule.RemainsMatchTime, gameRule.IsMatchEnded);

        base.OnPhotonPlayerConnected(newPlayer);
    }

    [PunRPC]
    protected void RpcUpdateScores(int length, object[] objects)
    {
        if (length == 0 || objects == null)
            return;
        var scores = new NetworkGameScore[length];
        var j = 0;
        for (var i = 0; i < length; ++i)
        {
            var score = new NetworkGameScore();
            score.viewId = (int)objects[j++];
            score.playerName = (string)objects[j++];
            score.score = (int)objects[j++];
            score.killCount = (int)objects[j++];
            score.assistCount = (int)objects[j++];
            score.dieCount = (int)objects[j++];
            scores[i] = score;
        }
        UpdateScores(scores);
    }

    [PunRPC]
    protected void RpcMatchStatus(float remainsMatchTime, bool isMatchEnded)
    {
        RemainsMatchTime = remainsMatchTime;
        if (!IsMatchEnded && isMatchEnded)
        {
            IsMatchEnded = true;
            MatchEndedAt = Time.unscaledTime;
        }
    }

    [PunRPC]
    protected void RpcKillNotify(string killerName, string victimName, string weaponId)
    {
        KillNotify(killerName, victimName, weaponId);
    }

    protected void ResetGame()
    {
        Characters.Clear();
        updateScoreTime = 0f;
        updateMatchTime = 0f;
        RemainsMatchTime = 0f;
        IsMatchEnded = false;
        MatchEndedAt = 0f;
        startUpdateGameRule = false;
    }

    protected abstract void UpdateScores(NetworkGameScore[] scores);
    protected abstract void KillNotify(string killerName, string victimName, string weaponId);
}
