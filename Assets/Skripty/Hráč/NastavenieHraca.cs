using UnityEngine;
using Mirror;

[RequireComponent(typeof(Hrac))]
public class NastavenieHraca : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] komponentyNaVypnutie;

    [SerializeField]
    GameObject uiPrefab;

    private GameObject uiInstancia;

    void Start()
    {
        // ak hr·Ë nenÌ lok·lny hr·Ë (hosù)
        if (!isLocalPlayer) {
            VypnutKomponenty(); // vypne komponenty
            AssignRemoteLayer(); // a prid· znaËku, aby hra vedela, ûe tento hr·Ë je pripojen˝ a nie lok·lny
        }

        // premenuje hr·Ëa
        RegisterPlayer();

        // nastavÌ hr·Ëa
        GetComponent<Hrac>().NastavSa();

        // vytvorÌ UI
        uiInstancia = Instantiate(uiPrefab);
        uiInstancia.name = uiPrefab.name;
    }

    void VypnutKomponenty() {
        foreach (Behaviour k in komponentyNaVypnutie)
        {
            k.enabled = false;
        }
    }

    void AssignRemoteLayer() => gameObject.layer = LayerMask.NameToLayer("RemotePlayer");

    void RegisterPlayer() => transform.name = $"Hr·Ë {GetComponent<NetworkIdentity>().netId}";

    public override void OnStartClient() {
        base.OnStartClient();

        // prid· hr·Ëa do zoznamu hr·Ëov v GameManageri
        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Hrac>());
    }

    void OnDisable() {
        // vymaûe UI
        Destroy(uiInstancia);

        // vymaûe hr·Ëa zo zoznamu hr·Ëov v GameManageri
        GameManager.UnRegisterPlayer(transform.name);
    }
}
