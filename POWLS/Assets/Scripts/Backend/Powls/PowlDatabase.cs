using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using POWL.Data;

namespace POWL.Managers
{
    /// <summary>
    /// Loads all PowlSO assets from a "Resources/Powls" folder 
    /// or acts as a manual list holder.
    /// </summary>
    public class PowlDatabase : MonoBehaviour
    {
        public static PowlDatabase Instance { get; private set; }

        [Tooltip("Drag all created Powl scriptable objects here")]
        public List<PowlSO> allPowls = new List<PowlSO>();

        private Dictionary<string, PowlSO> _powlLookup;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            _powlLookup = new Dictionary<string, PowlSO>();
            foreach (var powl in allPowls)
            {
                if (!_powlLookup.ContainsKey(powl.id))
                {
                    _powlLookup.Add(powl.id, powl);
                }
                else
                {
                    Debug.LogWarning($"Duplicate Powl ID found: {powl.id}. Check your Scriptable Objects.");
                }
            }
        }

        public PowlSO GetPowlById(string id)
        {
            if (_powlLookup.TryGetValue(id, out PowlSO powl))
            {
                return powl;
            }
            return null;
        }

        public PowlSO GetRandomPowlWeighted()
        {
            float totalWeight = allPowls.Sum(p => p.spawnWeight);
            float randomValue = Random.Range(0, totalWeight);
            float currentSum = 0;

            foreach (var powl in allPowls)
            {
                currentSum += powl.spawnWeight;
                if (randomValue <= currentSum)
                {
                    return powl;
                }
            }
            return allPowls[0]; // Fallback
        }
    }
}