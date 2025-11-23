using UnityEngine;
using POWL.Data;

namespace POWL.Game
{
    /// <summary>
    /// Attached to the Prefab of the Owl.
    /// Handles displaying the sprite and detecting clicks.
    /// </summary>
    public class OwlUnit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        private PowlSO _currentData;

        public void Setup(PowlSO data)
        {
            _currentData = data;
            if (_renderer != null)
            {
                _renderer.sprite = data.restingSprite;
            }
        }

        public void OnInteract()
        {
            Debug.Log($"Interacted with {_currentData.powlName}! Opening minigame selection...");
            // TODO: Trigger UI popup for Clean/Bandage/Groom
        }
    }
}