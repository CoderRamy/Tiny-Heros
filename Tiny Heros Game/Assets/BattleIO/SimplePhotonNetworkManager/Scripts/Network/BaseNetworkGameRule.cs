using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public abstract class BaseNetworkGameRule : ScriptableObject
{
    public const string MatchTimeCountdownKey = "rTCD";
    public const string BotAddedKey = "rBAD";
    public const string IsMatchEndedKey = "rEND";
    public const string BotCountKey = "rBC";
    public const string MatchTimeKey = "rMT";
    public const string MatchKillKey = "rMK";
    public const string MatchScoreKey = "rMS";

    [SerializeField]
    private string title;
    [SerializeField, TextArea]
    private string description;
    [SerializeField]
    private int defaultBotCount;
    [HideInInspector]
    public int botCount;
    [SerializeField, Tooltip("Time in seconds, 0 = Unlimit")]
    private int defaultMatchTime;
    [HideInInspector]
    public int matchTime;
    [SerializeField, Tooltip("Match kill limit, 0 = Unlimit")]
    private int defaultMatchKill;
    [HideInInspector]
    public int matchKill;
    [SerializeField, Tooltip("Match score limit, 0 = Unlimit")]
    private int defaultMatchScore;
    [HideInInspector]
    public int matchScore;
    protected BaseNetworkGameManager networkManager;
    public string Title { get { return title; } }
    public string Description { get { return description; } }
    protected abstract BaseNetworkGameCharacter NewBot();
    protected abstract void EndMatch();
    public int DefaultBotCount { get { return defaultBotCount; } }
    public int DefaultMatchTime { get { return defaultMatchTime; } }
    public int DefaultMatchKill { get { return defaultMatchKill; } }
    public int DefaultMatchScore { get { return defaultMatchScore; } }
    public virtual bool HasOptionBotCount { get { return false; } }
    public virtual bool HasOptionMatchTime { get { return false; } }
    public virtual bool HasOptionMatchKill { get { return false; } }
    public virtual bool HasOptionMatchScore { get { return false; } }
    public virtual bool ShowZeroScoreWhenDead { get { return false; } }
    public virtual bool ShowZeroKillCountWhenDead { get { return false; } }
    public virtual bool ShowZeroAssistCountWhenDead { get { return false; } }
    public virtual bool ShowZeroDieCountWhenDead { get { return false; ; } }
    public abstract bool CanCharacterRespawn(BaseNetworkGameCharacter character, params object[] extraParams);
    public abstract bool RespawnCharacter(BaseNetworkGameCharacter character, params object[] extraParams);
    public float RemainsMatchTime
    {
        get
        {
            if (HasOptionMatchTime && MatchTime > 0 && MatchTimeCountdown > 0 && !IsMatchEnded)
                return MatchTimeCountdown;
            return 0f;
        }
    }

    public float MatchTimeCountdown
    {
        get { try { return (float)PhotonNetwork.room.CustomProperties[MatchTimeCountdownKey]; } catch { } return 0f; }
        protected set { if (PhotonNetwork.isMasterClient) PhotonNetwork.room.SetCustomProperties(new Hashtable() { { MatchTimeCountdownKey, value } }); }
    }
    public bool IsBotAdded
    {
        get { try { return (bool)PhotonNetwork.room.CustomProperties[BotAddedKey]; } catch { } return false; }
        protected set { if (PhotonNetwork.isMasterClient) PhotonNetwork.room.SetCustomProperties(new Hashtable() { { BotAddedKey, value } }); }
    }
    public bool IsMatchEnded
    {
        get { try { return (bool)PhotonNetwork.room.CustomProperties[IsMatchEndedKey]; } catch { } return false; }
        protected set { if (PhotonNetwork.isMasterClient) PhotonNetwork.room.SetCustomProperties(new Hashtable() { { IsMatchEndedKey, value } }); }
    }
    public int BotCount
    {
        get { try { return (int)PhotonNetwork.room.CustomProperties[BotCountKey]; } catch { } return 0; }
        protected set { if (PhotonNetwork.isMasterClient) PhotonNetwork.room.SetCustomProperties(new Hashtable() { { BotCountKey, value } }); }
    }
    public int MatchTime
    {
        get { try { return (int)PhotonNetwork.room.CustomProperties[MatchTimeKey]; } catch { } return 0; }
        protected set { if (PhotonNetwork.isMasterClient) PhotonNetwork.room.SetCustomProperties(new Hashtable() { { MatchTimeKey, value } }); }
    }
    public int MatchKill
    {
        get { try { return (int)PhotonNetwork.room.CustomProperties[MatchKillKey]; } catch { } return 0; }
        protected set { if (PhotonNetwork.isMasterClient) PhotonNetwork.room.SetCustomProperties(new Hashtable() { { MatchKillKey, value } }); }
    }
    public int MatchScore
    {
        get { try { return (int)PhotonNetwork.room.CustomProperties[MatchScoreKey]; } catch { } return 0; }
        protected set { if (PhotonNetwork.isMasterClient) PhotonNetwork.room.SetCustomProperties(new Hashtable() { { MatchScoreKey, value } }); }
    }

    public virtual void AddBots()
    {
        if (!HasOptionBotCount)
            return;

        for (var i = 0; i < BotCount; ++i)
        {
            var character = NewBot();
            if (character == null)
                continue;
            networkManager.RegisterCharacter(character);
        }
    }

    public virtual void OnStartServer(BaseNetworkGameManager manager)
    {
        networkManager = manager;
        BotCount = botCount;
        MatchTime = matchTime;
        MatchKill = matchKill;
        MatchScore = matchScore;
        MatchTimeCountdown = MatchTime;
        IsBotAdded = false;
        IsMatchEnded = false;
    }

    public virtual void OnStopConnection(BaseNetworkGameManager manager)
    {

    }

    public virtual void OnMasterChange(BaseNetworkGameManager manager)
    {
        networkManager = manager;
    }

    public virtual void OnUpdate()
    {
        if (!IsBotAdded)
        {
            AddBots();
            IsBotAdded = true;
        }

        if (MatchTimeCountdown > 0)
            MatchTimeCountdown -= Time.unscaledDeltaTime;

        if (HasOptionMatchTime && MatchTime > 0 && MatchTimeCountdown <= 0 && !IsMatchEnded)
        {
            IsMatchEnded = true;
            EndMatch();
        }
    }

    public virtual void OnUpdateCharacter(BaseNetworkGameCharacter character)
    {
        if (HasOptionMatchScore && MatchScore > 0 && character.Score >= MatchScore)
        {
            IsMatchEnded = true;
            EndMatch();
        }

        if (HasOptionMatchKill && MatchKill > 0 && character.KillCount >= MatchKill)
        {
            IsMatchEnded = true;
            EndMatch();
        }
    }

    public abstract void InitialClientObjects();
}
