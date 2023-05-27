using System.Collections;
using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    // camera
    [SerializeField]
    private Camera cam;

    // weapon
    [SerializeField]
    private PlayerWeapon weapon;

    [SerializeField]
    private LayerMask mask;

    void Start() {
        if (cam == null) {
            Debug.LogError("No camera!");
            this.enabled = false;
        }
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    [Client]
    void Shoot() {
        RaycastHit rchit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rchit, weapon.range, mask)) {
            // play sound
            FindObjectOfType<AudioManager>().PlaySound(weapon.onShootSound);

            if (rchit.collider.tag == "Player") CmdPlayerShot(rchit.collider.name, rchit.collider.tag == "Head");
        }
    }

    [Command]
    void CmdPlayerShot(string id, bool headshot) {
        Debug.Log($"shot {id}");

        // damage player
        if (headshot) GameManager.GetPlayer(id).RpcTakeDamage(weapon.headshotDamage);
        else GameManager.GetPlayer(id).RpcTakeDamage(weapon.damage);
    }
}