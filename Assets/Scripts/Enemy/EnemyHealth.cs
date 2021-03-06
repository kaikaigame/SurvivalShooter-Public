using UnityEngine;
using UnityEngine.AI;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour
    {
        // The amount of health the enemy starts the game with.
        public int startingHealth = 100;

        // The current health the enemy has.
        public int currentHealth;

        // The speed at which the enemy sinks through the floor when dead.
        public float sinkSpeed = 2.5f;

        // The amount added to the player's score when the enemy dies.
        public int scoreValue = 10;

        // The sound to play when the enemy dies.
        public AudioClip deathClip;

        //int currentHealth;

        // Reference to the animator.
        Animator anim;

        // Reference to the audio source.
        AudioSource enemyAudio;

        // Reference to the particle system
        // that plays when the enemy is damaged.
        ParticleSystem hitParticles;

        // Reference to the capsule collider.
        CapsuleCollider capsuleCollider;

        // Whether the enemy is dead.
        bool isDead;

        // Whether the enemy has started sinking through the floor.
        bool isSinking;

        EnemyMovement enemyMovement;

        void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            //enemyMovement = this.GetComponent<EnemyMovement>();

            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
        }

        //void OnEnable()
        //{
        //    currentHealth = startingHealth;
        //    SetKinematics(false);
        //}

        //private void SetKinematics(bool isKinematic)
        //{
        //    capsuleCollider.isTrigger = isKinematic;
        //    capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        //}

        void Update ()
        {
            //if (IsDead())
            //{
            //    transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            //    if (transform.position.y < -10f)
            //    {
            //        Destroy(this.gameObject);
            //    }
            //}


            // If the enemy should be sinking...
            if (isSinking)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }

        //public bool IsDead()
        //{
        //    return (currentHealth <= 0f);
        //}

        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            //if (!IsDead())
            //{
            //    enemyAudio.Play();
            //    currentHealth -= amount;

            //    if (IsDead())
            //    {
            //        Death();
            //    }
            //    else
            //    {
            //        enemyMovement.GoToPlayer();
            //    }
            //}


            // If the enemy is dead...
            if (isDead)
                // ... no need to take damage so exit the function.
                return;

            // Play the hurt sound effect.
            enemyAudio.Play();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;

            // Set the position of the particle system to where the hit was sustained.
            hitParticles.transform.position = hitPoint;

            // And play the particles.
            hitParticles.Play();

            // If the current health is less than or equal to zero...
            if (currentHealth <= 0)
            {
                // ... the enemy is dead.
                Death();
            }
        }

        void Death ()
        {
            // The enemy is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            //EventManager.TriggerEvent("Sound", this.transform.position);

            // Tell the animator that the enemy is dead.
            anim.SetTrigger("Dead");

            // Change the audio clip of the audio source to the death clip and play it 
            // (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }

        public void StartSinking ()
        {
            //GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
            //SetKinematics(true);


            // Find and disable the Nav Mesh Agent.
            GetComponent<NavMeshAgent>().enabled = false;

            // Find the rigidbody component and make it kinematic 
            // (since we use Translate to sink the enemy).
            GetComponent<Rigidbody>().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
            ScoreManager.score += scoreValue;

            // After 2 seconds destory the enemy.
            Destroy(gameObject, 2f);
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }
    }
}