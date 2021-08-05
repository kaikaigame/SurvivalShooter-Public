using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;        // The player's score.

        //private int levelThreshhold;
        const int LEVEL_INCREASE = 300;

        Text sText;
        Text text;                      // Reference to the Text component.

        void Awake ()
        {
            sText = GetComponent <Text> ();

            // Set up the reference.
            text = GetComponent<Text>();

            // Reset the score.
            score = 0;

            //levelThreshhold = LEVEL_INCREASE;
        }


        void Update ()
        {
            //sText.text = "Score: " + score;
            //if (score >= levelThreshhold)
            //{
            //    AdvanceLevel();
            //}

            // Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Score: " + score;
        }

        //private void AdvanceLevel()
        //{
        //    levelThreshhold = score + LEVEL_INCREASE;
        //    LevelManager lm = FindObjectOfType<LevelManager>();
        //    lm.AdvanceLevel();
        //}
    }
}