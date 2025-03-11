using UnityEngine;

namespace SecondProject
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string PlayerLayerName = "Player";//
        public const string EnemyLayerName = "Enemy";
        public const string PickUpLayerName = "PickUp";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);

        public static readonly int PlayerLayer = LayerMask.NameToLayer(PlayerLayerName);
        public static readonly int EnemyLayer = LayerMask.NameToLayer(EnemyLayerName);

        public static readonly int PlayerAndEnemyMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);
        public static readonly int PickUpsMask = LayerMask.GetMask(PickUpLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer == PickUpLayer;
        public static bool IsPlayer(GameObject other) => other.layer == LayerMask.NameToLayer(PlayerLayerName);
        public static bool IsEnemy(GameObject other) => other.layer == LayerMask.NameToLayer(EnemyLayerName);
    }
}
