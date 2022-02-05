using UnityEngine;
using Mirror;

public class Minimapa : NetworkBehaviour
{
    public Transform hrac;

    void LateUpdate() {
        if (!isLocalPlayer) return;

        // pohyb podæa hr·Ëa
        Vector3 novaPozicia = hrac.position;
        novaPozicia.y = transform.position.y;
        transform.position = novaPozicia;

        // ot·Ëanie podæa hr·Ëa
        transform.rotation = Quaternion.Euler(90f, hrac.eulerAngles.y, 0f);
    }
}
