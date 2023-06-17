using UnityEngine;
using Mirror;
using System.Collections;
using OrbWars;

namespace OrbWars.OWPlayer {
    public class Player : NetworkBehaviour {
        // is player dead
        [SyncVar]
        private bool _isDead = false;
        public bool isDead {
            get { return _isDead; }
            protected set { _isDead = value; }
        }

        // max health
        [SerializeField]
        private int maxHealth = 100;

        // current health
        [SyncVar]
        private int health;

        [SerializeField]
        private Behaviour[] disableOnDeath;
        private bool[] wasEnabled;

        public void Setup() {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++) {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            SetDefaults();
        }

        [ClientRpc]
        public void RpcTakeDamage(int damage) {
            if (_isDead) return;

            health -= damage;
            Debug.Log($"{transform.name} has {health} hp.");

            if (health <= 0) Die();
        }

        private void Die() {
            isDead = true;

            // disables components
            for (int i = 0; i < disableOnDeath.Length; i++)
            {
                disableOnDeath[i].enabled = false;
            }

            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;

            Debug.Log($"{transform.name} died!");

            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn() {
            // wait
            yield return new WaitForSeconds(GameManager.instance.matchOpts.respawnDelay);

            SetDefaults();

            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }

        public void SetDefaults() {
            isDead = false;

            health = maxHealth;

            for (int i = 0; i < disableOnDeath.Length; i++) {
                disableOnDeath[i].enabled = wasEnabled[i];
            }

            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = true;
        }
    }
}