using UnityEngine;
using Mirror;
using OrbWars;

namespace OrbWars.OWPlayer {
    [RequireComponent(typeof(Player))]
    public class PlayerSetup : NetworkBehaviour {
        [SerializeField]
        Behaviour[] componentsToDisable;

        [SerializeField]
        GameObject uiPrefab;

        private GameObject uiInstance;

        void Start()
        {
            if (!isLocalPlayer) {
                DisableComponents();
                AssignRemoteLayer();
            }

            RegisterPlayer();

            // player setup
            GetComponent<Player>().Setup();

            // make UI
            uiInstance = Instantiate(uiPrefab);
            uiInstance.name = uiPrefab.name;
        }

        void DisableComponents() {
            foreach (Behaviour k in componentsToDisable) k.enabled = false;
        }

        void AssignRemoteLayer() => gameObject.layer = LayerMask.NameToLayer("RemotePlayer");

        void RegisterPlayer() => transform.name = $"Player {GetComponent<NetworkIdentity>().netId}";

        public override void OnStartClient() {
            base.OnStartClient();

            GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Player>());
        }

        void OnDisable() {
            Destroy(uiInstance);

            GameManager.UnregisterPlayer(transform.name);
        }
    }
}