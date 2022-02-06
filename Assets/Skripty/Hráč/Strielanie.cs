using System.Collections;
using UnityEngine;
using Mirror;

public class Strielanie : NetworkBehaviour
{
    // kamera hr·Ëa
    [SerializeField]
    private Camera kamera;

    // zbraÚ hr·Ëa
    [SerializeField]
    private ZbranHraca zbranHraca;

    // Ëo strieæaù?
    [SerializeField]
    private LayerMask maska;

    void Start() {
        // ak kamera nenÌ nastaven·, tak nestrieæaj
        if (kamera == null) {
            Debug.LogError("éiadna kamera!");
            this.enabled = false;
        }
    }

    void Update() {
        // ak hr·Ë stlaËÌ Fire1, tak strieæaj
        if (Input.GetButtonDown("Fire1")) Strielaj();
    }

    [Client]
    void Strielaj() {
        RaycastHit rchit;

        // spravÌ Raycast a uloû info do rchit
        if (Physics.Raycast(kamera.transform.position, kamera.transform.forward, out rchit, zbranHraca.dosah, maska)) {
            // prehr· zvuk
            FindObjectOfType<AudioManager>().PrehratZvuk(zbranHraca.strielaciZvuk);
            // ak to, Ëo sme strelili je hr·Ë, tak poökoÔ hr·Ëa
            if (rchit.collider.tag == "Player") CmdHracZastreleny(rchit.collider.name, rchit.collider.tag == "Hlava");
        }
    }

    [Command]
    void CmdHracZastreleny(string id, bool headshot) {
        Debug.Log($"{id} bol zastrelen˝!");

        // poökodÌ hr·Ëa
        if (headshot) GameManager.GetPlayer(id).RpcPoskodSa(zbranHraca.headshotPoskodenie);
        else GameManager.GetPlayer(id).RpcPoskodSa(zbranHraca.poskodenie);
    }
}