using UnityEngine;
using Mirror;

public class Minimap : NetworkBehaviour
{
    public Transform player;

    void LateUpdate() {
        if (!isLocalPlayer) return;

        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
