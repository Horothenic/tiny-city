using UnityEngine;

namespace TinyWar
{
    [CreateAssetMenu(menuName = "Tiny War/Stats Container")]
    public class StatsContainer : ScriptableObject
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private float _tickTime = 1;

        public float TickTime => _tickTime;

        #endregion
    }
}
