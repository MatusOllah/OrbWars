using UnityEngine;
using Mirror;
using System.Collections;

public class Hrac : NetworkBehaviour
{
    // je hr�� m�tvy?
    [SyncVar]
    private bool _jeMrtvy = false;
    public bool jeMrtvy  {
        get { return _jeMrtvy; }
        protected set { _jeMrtvy = value; }
    }

    // ko�ko mo�e ma� hr�� najviac �ivotov?
    [SerializeField]
    private int maxZivotov = 100;

    // ko�ko m� hr�� �ivotov?
    [SyncVar]
    private int zivoty;

    // komponenty na vypnutie pri smrti
    [SerializeField]
    private Behaviour[] komponentyNaVypnutie;
    private bool[] bolZapnuty;

    public void NastavSa() {
        bolZapnuty = new bool[komponentyNaVypnutie.Length];
        for (int i = 0; i < bolZapnuty.Length; i++) {
            bolZapnuty[i] = komponentyNaVypnutie[i].enabled;
        }

        SetDefaults();
    }

    // hr�� sa po�kod�
    [ClientRpc]
    public void RpcPoskodSa(int poskodenie) {
        if (_jeMrtvy) return;

        // odr�taj� sa �ivoty
        zivoty -= poskodenie;
        Debug.Log($"{transform.name} m� teraz {zivoty} �ivotov.");

        // ak m� hr�� 0 �ivotov, tak hr�� zomrie
        if (zivoty <= 0) Zomri();
    }

    private void Zomri() {
        jeMrtvy = true;

        // vypne komponenty
        for (int i = 0; i < komponentyNaVypnutie.Length; i++)
        {
            komponentyNaVypnutie[i].enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Debug.Log($"{transform.name} zomrel!");

        StartCoroutine(OzivSa());
    }

    private IEnumerator OzivSa() {
        // po�k� x sek�nd
        yield return new WaitForSeconds(GameManager.instancia.nastaveniaKola.oneskorenieOzitia);

        SetDefaults();

        // objav� sa
        Transform spawn = NetworkManager.singleton.GetStartPosition();
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
    }

    public void SetDefaults() {
        jeMrtvy = false;

        zivoty = maxZivotov;

        for (int i = 0; i < komponentyNaVypnutie.Length; i++) {
            komponentyNaVypnutie[i].enabled = bolZapnuty[i];
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;
    }
}
