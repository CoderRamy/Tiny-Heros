using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon;

public class GameplayManager : PunBehaviour
{
    [System.Serializable]
    public struct RewardCurrency
    {
        public string currencyId;
        public IntAttribute amount;
    }
    public const int UNIQUE_VIEW_ID = 998;
    public const float REAL_MOVE_SPEED_RATE = 0.1f;
    public static GameplayManager Singleton { get; private set; }
    [Header("Character stats")]
    public int maxLevel = 1000;
    public IntAttribute exp = new IntAttribute() { minValue = 20, maxValue = 1023050, growth = 2.5f };
    public IntAttribute rewardExp = new IntAttribute() { minValue = 8, maxValue = 409220, growth = 2.5f };
    public RewardCurrency[] rewardCurrencies;
    public IntAttribute killScore = new IntAttribute() { minValue = 10, maxValue = 511525, growth = 1f };
    public int minHp = 100;
    public int minAttack = 30;
    public int minDefend = 20;
    public int minMoveSpeed = 30;
    public int maxSpreadDamages = 6;
    public int addingStatPoint = 1;
    public float minAttackVaryRate = -0.07f;
    public float maxAttackVaryRate = 0.07f;
    public CharacterAttributes[] availableAttributes;
    [Header("Game rules")]
    public int watchAdsRespawnAvailable = 2;
    public float respawnDuration = 5f;
    public float invincibleDuration = 1.5f;
    public SpawnArea[] characterSpawnAreas;
    public SpawnArea[] powerUpSpawnAreas;
    public PowerUpSpawnData[] powerUps;
    public readonly Dictionary<string, PowerUpEntity> powerUpEntities = new Dictionary<string, PowerUpEntity>();
    public readonly Dictionary<string, CharacterAttributes> attributes = new Dictionary<string, CharacterAttributes>();
    public Props[] _Props;
    [System.Serializable]
    public struct Props
    {
        public GameObject _Props;
        public SimpleCubeData SpawnZone;
        public int Count;
    }
    public Weapons[] _Weapons;
    [System.Serializable]
    public struct Weapons
    {
        public string WeaponName;
        public GameObject _Weapons;
        public SimpleCubeData SpawnZone;
        public int Count;
    }
    protected virtual void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;

        powerUpEntities.Clear();
        foreach (var powerUp in powerUps)
        {
            var powerUpPrefab = powerUp.powerUpPrefab;
            if (powerUpPrefab != null && !powerUpEntities.ContainsKey(powerUpPrefab.name))
                powerUpEntities.Add(powerUpPrefab.name, powerUpPrefab);
        }
        attributes.Clear();
        foreach (var availableAttribute in availableAttributes)
        {
            attributes[availableAttribute.name] = availableAttribute;
        }
        // Set unique view id
        PhotonView view = GetComponent<PhotonView>();
        if (view == null)
            view = gameObject.AddComponent<PhotonView>();
        view.viewID = UNIQUE_VIEW_ID;
    }

    protected virtual void Start()
    {
        if (PhotonNetwork.isMasterClient)
            OnStartServer();
    }
    
    protected virtual void OnStartServer()
    {
        Debug.Log("6");
        foreach (var powerUp in powerUps)
        {
            if (powerUp.powerUpPrefab == null)
                continue;
            for (var i = 0; i < powerUp.amount; ++i)
                SpawnPowerUp(powerUp.powerUpPrefab.name);
        }

        SpawnProps();
        SpawnWeapons();
  
    }

    public void SpawnPowerUp(string prefabName)
    {
        if (!PhotonNetwork.isMasterClient || string.IsNullOrEmpty(prefabName))
            return;
        PowerUpEntity powerUpPrefab = null;
        if (powerUpEntities.TryGetValue(prefabName, out powerUpPrefab))
        {
            var powerUpEntityGo = PhotonNetwork.InstantiateSceneObject(powerUpPrefab.name, GetPowerUpSpawnPosition(), Quaternion.identity, 0, new object[0]);
            var powerUpEntity = powerUpEntityGo.GetComponent<PowerUpEntity>();
            powerUpEntity.prefabName = prefabName;
        }
    }

    public void SpawnProps()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        for (int i = 0; i < _Props.Length; i++)
        {
            for (int x = 0; x < _Props[i].Count; x++)
            {
                if (_Props[i].SpawnZone != null)
                {
                    var _prop = PhotonNetwork.InstantiateSceneObject(_Props[i]._Props.name, _Props[i].SpawnZone.GetRandomPosition(), Quaternion.identity, 0, new object[0]);
                    _prop.transform.SetParent(_Props[i].SpawnZone.transform);

                }
            }
        }
    }

    public void SpawnWeapons()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        for (int i = 0; i < _Weapons.Length; i++)
        {
            for (int x = 0; x < _Weapons[i].Count; x++)
            {
                var _prop = PhotonNetwork.InstantiateSceneObject(_Weapons[i]._Weapons.name, _Weapons[i].SpawnZone.GetRandomPosition(), Quaternion.identity, 0, new object[0]);
                _prop.transform.SetParent(_Weapons[i].SpawnZone.transform);
            }
        }
    }

    public Vector3 GetCharacterSpawnPosition(CharacterEntity character)
    {
        if (characterSpawnAreas == null || characterSpawnAreas.Length == 0)
            return Vector3.zero;
        return characterSpawnAreas[Random.Range(0, characterSpawnAreas.Length)].GetSpawnPosition();
    }

    public Vector3 GetPowerUpSpawnPosition()
    {
        if (powerUpSpawnAreas == null || powerUpSpawnAreas.Length == 0)
            return Vector3.zero;
        return powerUpSpawnAreas[Random.Range(0, powerUpSpawnAreas.Length)].GetSpawnPosition();
    }

    public int GetExp(int currentLevel)
    {
        return exp.Calculate(currentLevel, maxLevel);
    }

    public int GetRewardExp(int currentLevel)
    {
        return rewardExp.Calculate(currentLevel, maxLevel);
    }

    public int GetKillScore(int currentLevel)
    {
        return killScore.Calculate(currentLevel, maxLevel);
    }

    public virtual bool CanRespawn(CharacterEntity character)
    {
        var networkGameplayManager = BaseNetworkGameManager.Singleton;
        if (networkGameplayManager != null && networkGameplayManager.IsMatchEnded)
            return false;
        return true;
    }

    public virtual bool CanReceiveDamage(CharacterEntity character)
    {
        var networkGameplayManager = BaseNetworkGameManager.Singleton;
        if (networkGameplayManager != null && networkGameplayManager.IsMatchEnded)
            return false;
        return true;
    }
}
