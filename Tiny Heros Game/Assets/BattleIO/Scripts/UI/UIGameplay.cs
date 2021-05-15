﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    public Text textLevel;
    public Text textExp;
    public Image fillExp;
    public Text textStatPoint;
    public Text textHp;
    public Text textAttack;
    public Text textDefend;
    public Text textMoveSpeed;
    public Text textRespawnCountDown;
    public Text textWatchedAdsCount;
    public Text textMatchCountDown;
    public UIBlackFade blackFade;
    public GameObject respawnUiContainer;
    public GameObject respawnButtonContainer;
    public UINetworkGameScores[] uiGameScores;
    public UIKillNotifies uiKillNotifies;
    public UIRandomAttributes randomAttributes;
    public GameObject matchEndUi;
    public GameObject[] mobileOnlyUis;
    private bool isRespawnShown;
    private bool isRandomedAttributesShown;
    private bool canRandomAttributes;

    private void OnEnable()
    {
        StartCoroutine(SetupCanRandomAttributes());
    }

    IEnumerator SetupCanRandomAttributes()
    {
        canRandomAttributes = false;
        yield return new WaitForSeconds(1);
        canRandomAttributes = true;
    }

    private void Update()
    {
        foreach (var mobileOnlyUi in mobileOnlyUis)
        {
            bool showJoystick = Application.isMobilePlatform;
#if UNITY_EDITOR
            showJoystick = GameInstance.Singleton.showJoystickInEditor;
#endif
            if (mobileOnlyUi != null)
                mobileOnlyUi.SetActive(showJoystick);
        }

        var localCharacter = BaseNetworkGameCharacter.Local as CharacterEntity;
        if (localCharacter == null)
            return;

        var level = localCharacter.level;
        var exp = localCharacter.Exp;
        var nextExp = GameplayManager.Singleton.GetExp(level);
        if (textLevel != null)
            textLevel.text = "LV" + level.ToString("N0");

        if (textExp != null)
            textExp.text = exp.ToString("N0") + "/" + nextExp.ToString("N0");

        if (fillExp != null)
            fillExp.fillAmount = (float)exp / (float)nextExp;

        if (textStatPoint != null)
            textStatPoint.text = localCharacter.statPoint.ToString("N0");

        if (textHp != null)
            textHp.text = localCharacter.TotalHp.ToString("N0");

        if (textAttack != null)
            textAttack.text = localCharacter.TotalAttack.ToString("N0");

        if (textDefend != null)
            textDefend.text = localCharacter.TotalDefend.ToString("N0");

        if (textMoveSpeed != null)
            textMoveSpeed.text = localCharacter.TotalMoveSpeed.ToString("N0");

        if (localCharacter.Hp <= 0)
        {
            if (!isRespawnShown)
            {
                if (respawnUiContainer != null)
                    respawnUiContainer.SetActive(true);
                isRespawnShown = true;
            }
            if (isRespawnShown)
            {
                var remainTime = GameplayManager.Singleton.respawnDuration - (Time.unscaledTime - localCharacter.deathTime);
                var watchAdsRespawnAvailable = GameplayManager.Singleton.watchAdsRespawnAvailable;
                if (remainTime < 0)
                    remainTime = 0;
                if (textRespawnCountDown != null)
                    textRespawnCountDown.text = Mathf.Abs(remainTime).ToString("N0");
                if (textWatchedAdsCount != null)
                    textWatchedAdsCount.text = (watchAdsRespawnAvailable - localCharacter.watchAdsCount) + "/" + watchAdsRespawnAvailable;
                if (respawnButtonContainer != null)
                    respawnButtonContainer.SetActive(remainTime == 0);
            }
        }
        else
        {
            if (respawnUiContainer != null)
                respawnUiContainer.SetActive(false);
            isRespawnShown = false;
        }

        if (localCharacter.Hp > 0 && localCharacter.statPoint > 0 && canRandomAttributes)
        {
            if (!isRandomedAttributesShown)
            {
                if (randomAttributes != null)
                {
                    randomAttributes.uiGameplay = this;
                    randomAttributes.gameObject.SetActive(true);
                    randomAttributes.Random();
                }
                isRandomedAttributesShown = true;
            }
        }
        else
        {
            if (randomAttributes != null)
                randomAttributes.gameObject.SetActive(false);
            isRandomedAttributesShown = false;
        }

        if (textMatchCountDown != null)
        {
            if (localCharacter.NetworkManager != null)
            {
                var formattedTime = string.Empty;
                var timer = localCharacter.NetworkManager.RemainsMatchTime;
                if (timer > 0f)
                {
                    int minutes = Mathf.FloorToInt(timer / 60f);
                    int seconds = Mathf.FloorToInt(timer - minutes * 60);
                    formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
                }
                textMatchCountDown.text = formattedTime;
            }
        }

        if (matchEndUi != null)
        {
            if (localCharacter.NetworkManager != null)
                matchEndUi.SetActive(localCharacter.NetworkManager.IsMatchEnded);
        }
    }

    public void UpdateRankings(NetworkGameScore[] rankings)
    {
        for (var i = 0; i < uiGameScores.Length; ++i)
        {
            var uiGameScore = uiGameScores[i];
            if (uiGameScore != null)
                uiGameScore.UpdateRankings(rankings);
        }
    }

    public void KillNotify(string killerName, string victimName, string weaponId)
    {
        if (uiKillNotifies != null)
        {
            string weaponName = "Unknow Weapon";
            Texture weaponIcon = null;
            var weaponData = GameInstance.GetWeapon(weaponId);
            if (weaponData != null)
            {
                weaponName = weaponData.title;
                weaponIcon = weaponData.iconTexture;
            }
            uiKillNotifies.Notify(killerName, victimName, weaponName, weaponIcon);
        }
    }

    public void AddAttribute(string name)
    {
        var character = BaseNetworkGameCharacter.Local as CharacterEntity;
        if (character == null || character.statPoint == 0)
            return;
        character.CmdAddAttribute(name);
        StartCoroutine(SetupCanRandomAttributes());
    }

    public void Respawn()
    {
        var character = BaseNetworkGameCharacter.Local as CharacterEntity;
        if (character == null)
            return;
        character.CmdRespawn(false);
    }

    public void WatchAdsRespawn()
    {
        var character = BaseNetworkGameCharacter.Local as CharacterEntity;
        if (character == null)
            return;

        if (character.watchAdsCount >= GameplayManager.Singleton.watchAdsRespawnAvailable)
        {
            character.CmdRespawn(false);
            return;
        }
        MonetizationManager.ShowAd(GameInstance.Singleton.watchAdsRespawnPlacement, OnWatchAdsRespawnResult);
    }

    private void OnWatchAdsRespawnResult(MonetizationManager.RemakeShowResult result)
    {
        if (result == MonetizationManager.RemakeShowResult.Finished)
        {
            var character = BaseNetworkGameCharacter.Local as CharacterEntity;
            character.CmdRespawn(true);
        }
    }

    public void ExitGame()
    {
        if (blackFade != null)
        {
            blackFade.onFadeIn.AddListener(() =>
            {
                GameNetworkManager.Singleton.LeaveRoom();
            });
            blackFade.FadeIn();
        }
        else
        {
            Destroy(gameObject);
            GameNetworkManager.Singleton.LeaveRoom();
        }
    }

    public void FadeIn()
    {
        if (blackFade != null)
            blackFade.FadeIn();
    }

    public void FadeOut()
    {
        if (blackFade != null)
            blackFade.FadeOut();
    }
}
