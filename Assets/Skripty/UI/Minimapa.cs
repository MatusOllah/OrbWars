using UnityEngine;
using Mirror;

public class Minimapa : NetworkBehaviour
{
    public Transform hrac;

    void LateUpdate() {
        if (!isLocalPlayer) return;

        // pohyb pod�a hr��a
        Vector3 novaPozicia = hrac.position;
        novaPozicia.y = transform.position.y;
        transform.position = novaPozicia;

        // ot��anie pod�a hr��a
        transform.rotation = Quaternion.Euler(90f, hrac.eulerAngles.y, 0f);
    }
}
