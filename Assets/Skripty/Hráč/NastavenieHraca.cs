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
        // ak hr�� nen� lok�lny hr�� (hos�)
        if (!isLocalPlayer) {
            VypnutKomponenty(); // vypne komponenty
            AssignRemoteLayer(); // a prid� zna�ku, aby hra vedela, �e tento hr�� je pripojen� a nie lok�lny
        }

        // premenuje hr��a
        RegisterPlayer();

        // nastav� hr��a
        GetComponent<Hrac>().NastavSa();

        // vytvor� UI
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

    void RegisterPlayer() => transform.name = $"Hr�� {GetComponent<NetworkIdentity>().netId}";

    public override void OnStartClient() {
        base.OnStartClient();

        // prid� hr��a do zoznamu hr��ov v GameManageri
        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), GetComponent<Hrac>());
    }

    void OnDisable() {
        // vyma�e UI
        Destroy(uiInstancia);

        // vyma�e hr��a zo zoznamu hr��ov v GameManageri
        GameManager.UnRegisterPlayer(transform.name);
    }
}
