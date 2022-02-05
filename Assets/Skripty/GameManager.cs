using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NastaveniaKola nastaveniaKola;

    #region Nastavenie inötancie

    public static GameManager instancia;

    void Awake() {
        if (instancia != null) Debug.LogError("Viac neû 1 GameManager v scÈne!");
        else instancia = this;
    }

    #endregion

    #region Hr·Ëi

    // ID predpona
    private const string ID_PREFIX = "Hr·Ë ";

    // zoznam hr·Ëov
    private static Dictionary<string, Hrac> hraci = new Dictionary<string, Hrac>();

    // prid· hr·Ëa do zoznamu hr·Ëov
    public static void RegisterPlayer(string id, Hrac hrac) {
        hraci.Add(ID_PREFIX + id, hrac);
        hrac.transform.name = ID_PREFIX + id;
    }

    // odstr·ni hr·Ëa zo zoznamu hr·Ëov
    public static void UnRegisterPlayer(string id) => hraci.Remove(id);

    // n·jde hr·Ëa
    public static Hrac GetPlayer(string id) => hraci[id];

    #endregion
}
