using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NastaveniaKola nastaveniaKola;

    #region Nastavenie in�tancie

    public static GameManager instancia;

    void Awake() {
        if (instancia != null) Debug.LogError("Viac ne� 1 GameManager v sc�ne!");
        else instancia = this;
    }

    #endregion

    #region Hr��i

    // ID predpona
    private const string ID_PREFIX = "Hr�� ";

    // zoznam hr��ov
    private static Dictionary<string, Hrac> hraci = new Dictionary<string, Hrac>();

    // prid� hr��a do zoznamu hr��ov
    public static void RegisterPlayer(string id, Hrac hrac) {
        hraci.Add(ID_PREFIX + id, hrac);
        hrac.transform.name = ID_PREFIX + id;
    }

    // odstr�ni hr��a zo zoznamu hr��ov
    public static void UnRegisterPlayer(string id) => hraci.Remove(id);

    // n�jde hr��a
    public static Hrac GetPlayer(string id) => hraci[id];

    #endregion
}
