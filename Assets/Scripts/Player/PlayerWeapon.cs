namespace OrbWars.OWPlayer {
    [System.Serializable]
    public class PlayerWeapon {
        public string name = "Glock";

        public int damage = 5;

        public int headshotDamage = 10;

        public float range = 100f;

        public string onShootSound;
    }
}