using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestZone : MonoBehaviour
{

    public class GameManager : MonoBehaviour
    {

        public int score;
        public Text scoretext;
        [SerializeField]
        int addScore = 50;

        void Start()
        {
            score = PlayerPrefs.GetInt("score");
            scoretext.text = score.ToString();
        }

        public void addscore()
        {
            score = PlayerPrefs.GetInt("score") + addScore;
            PlayerPrefs.SetInt("score", score);
            scoretext.text = score.ToString();
        }
    }
}
