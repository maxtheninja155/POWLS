using UnityEngine;

namespace POWL.Data
{
    public enum PowlRarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }

    public enum PowlPersonality
    {
        Grumpy,
        Peppy,
        Sleepy,
        Wise
    }

    [CreateAssetMenu(fileName = "NewPowl", menuName = "POWL/Powl Data")]
    public class PowlSO : ScriptableObject
    {
        [Header("Identity")]
        public string id; // Unique ID
        public string powlName;
        [TextArea] public string flavorText;

        [Header("Visuals")]
        public Sprite restingSprite; // Used in the Aviary
        public Sprite closeUpSprite; // Used in the Care Minigame

        [Header("Stats")]
        public float heightCm;
        public float weightGrams;
        public PowlPersonality personality;
        public PowlRarity rarity;

        [Header("Spawn Logic")]
        // Higher weight = higher chance to spawn relative to others
        [Range(0f, 100f)] public float spawnWeight = 50f;
    }
}