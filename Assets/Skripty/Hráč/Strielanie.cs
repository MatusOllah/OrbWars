using System.Collections;
using UnityEngine;
using Mirror;

public class Strielanie : NetworkBehaviour
{
    // kamera hr��a
    [SerializeField]
    private Camera kamera;

    // zbra� hr��a
    [SerializeField]
    private ZbranHraca zbranHraca;

    // �o strie�a�?
    [SerializeField]
    private LayerMask maska;

    void Start() {
        // ak kamera nen� nastaven�, tak nestrie�aj
        if (kamera == null) {
            Debug.LogError("�iadna kamera!");
            this.enabled = false;
        }
    }

    void Update() {
        // ak hr�� stla�� Fire1, tak strie�aj
        if (Input.GetButtonDown("Fire1")) Strielaj();
    }

    [Client]
    void Strielaj() {
        RaycastHit rchit;

        // sprav� Raycast a ulo� info do rchit
        if (Physics.Raycast(kamera.transform.position, kamera.transform.forward, out rchit, zbranHraca.dosah, maska)) {
            // prehr� zvuk
            FindObjectOfType<AudioManager>().PrehratZvuk(zbranHraca.strielaciZvuk);
            // ak to, �o sme strelili je hr��, tak po�ko� hr��a
            if (rchit.collider.tag == "Player") CmdHracZastreleny(rchit.collider.name, rchit.collider.tag == "Hlava");
        }
    }

    [Command]
    void CmdHracZastreleny(string id, bool headshot) {
        Debug.Log($"{id} bol zastrelen�!");

        // po�kod� hr��a
        if (headshot) GameManager.GetPlayer(id).RpcPoskodSa(zbranHraca.headshotPoskodenie);
        else GameManager.GetPlayer(id).RpcPoskodSa(zbranHraca.poskodenie);
    }
}