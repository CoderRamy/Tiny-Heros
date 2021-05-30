using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsLoader : MonoBehaviour
{
    public static GameSettingsLoader instance;
    DataLoader dataloader = new DataLoader();
    public GameSettings gameSettings = new GameSettings();
    public static bool gameSettingsLoaded = false;


    private void Awake()
    {
        instance = this;
    }

    //set settings for every game config
    private void SetGameSettings(GameSettings settings)
    {
       
        foreach(var game in settings.gameSettings)
        {
            Debug.Log(game.name);
            if (game.name.Equals("Hangman"))
            {
            //    Globals.HangmanConfig.SinglePoints = game.single_mode_points;
            //    Globals.HangmanConfig.SingleGems = game.single_mode_gems;

            //    Globals.HangmanConfig.AIPoints = game.ai_mode_points;
            //    Globals.HangmanConfig.AIGem = game.ai_mode_gems;

            //    Globals.HangmanConfig.OnlinePoints = game.online_mode_points;
            //    Globals.HangmanConfig.OnlineGems = game.online_mode_gems;

            //    Globals.HangmanConfig.GameTimer = game.game_timer;
            //    Globals.HangmanConfig.TurnTimer = game.turn_timer;
            //    Globals.HangmanConfig.Turns = game.turns;

            //    Debug.Log(Globals.HangmanConfig.GameTimer);
            //    Debug.Log(game.game_timer);
            //}

            //if (game.name.Equals("TrueOrFalse"))
            //{
            //    Globals.TrueOrFalseConfig.SinglePoints = game.single_mode_points;
            //    Globals.TrueOrFalseConfig.SingleGems = game.single_mode_gems;

            //    Globals.TrueOrFalseConfig.AIPoints = game.ai_mode_points;
            //    Globals.TrueOrFalseConfig.AIGem = game.ai_mode_gems;

            //    Globals.TrueOrFalseConfig.OnlinePoints = game.online_mode_points;
            //    Globals.TrueOrFalseConfig.OnlineGems = game.online_mode_gems;

            //    Globals.TrueOrFalseConfig.GameTimer = game.game_timer;
            //    Globals.TrueOrFalseConfig.TurnTimer = game.turn_timer;
            //    Globals.TrueOrFalseConfig.Turns = game.turns;
            //}

            //if (game.name.Equals("MemoryGame"))
            //{
            //    Globals.MemoryGameConfig.SinglePoints = game.single_mode_points;
            //    Globals.MemoryGameConfig.SingleGems = game.single_mode_gems;

            //    Globals.MemoryGameConfig.AIPoints = game.ai_mode_points;
            //    Globals.MemoryGameConfig.AIGem = game.ai_mode_gems;

            //    Globals.MemoryGameConfig.OnlinePoints = game.online_mode_points;
            //    Globals.MemoryGameConfig.OnlineGems = game.online_mode_gems;

            //    Globals.MemoryGameConfig.GameTimer = game.game_timer;
            //    Globals.MemoryGameConfig.TurnTimer = game.turn_timer;
            //    Globals.MemoryGameConfig.Turns = game.turns;
            }
        }

      
    }

    public IEnumerator GetData()
    {

        CoroutineWithData cd = new CoroutineWithData(this, dataloader.LoadGameSettings());
        yield return cd.coroutine;

        gameSettings = cd.result as GameSettings;
        gameSettingsLoaded = true;
        SetGameSettings(gameSettings);
    }
}
