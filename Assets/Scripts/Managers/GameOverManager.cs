using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.
        public float restartDelay = 5f;         // Time to wait before restarting the level

        Animator anim;                          // Reference to the animator component.
        float restartTimer;                     // Timer to count up to restarting the level

        LevelManager lm;
        private UnityEvent listener;

        void Awake ()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            anim = GetComponent <Animator> ();
            lm = FindObjectOfType<LevelManager>();
            //EventManager.StartListening("GameOver", ShowGameOver);
        }

        //void OnDestroy()
        //{
        //    EventManager.StopListening("GameOver", ShowGameOver);
        //}

        //void ShowGameOver()
        //{
        //    anim.SetBool("GameOver", true);
        //}

        //private void ResetLevel()
        //{
        //    ScoreManager.score = 0;
        //    LevelManager lm = FindObjectOfType<LevelManager>();
        //    lm.LoadInitialLevel();
        //    anim.SetBool("GameOver", false);
        //    playerHealth.ResetPlayer();
        //}

        void Update()
        {
            // If the player has run out of health...
            if (playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the game is over.
                anim.SetTrigger("GameOver");

                // .. increment a timer to count up to restarting.
                restartTimer += Time.deltaTime;

                // .. if it reaches the restart delay...
                if (restartTimer >= restartDelay)
                {
                    // .. then reload the currently loaded level.
                    //Application.LoadLevel(Application.loadedLevel);

                    Scene scene = SceneManager.GetActiveScene(); 
                    SceneManager.LoadScene(scene.name);
                }
            }
        }
    }
}