using UnityEngine;
using POWL.Data;
using POWL.Input;
using POWL.Game;

namespace POWL.Managers
{
    /// <summary>
    /// The Brain. Handles Time tracking and Spawning logic.
    /// </summary>
    public class PowlManager : MonoBehaviour
    {
        [Header("Scene References")]
        public Transform aviarySpawnPoint;
        public GameObject owlPrefab;

        [Header("Settings")]
        public float spawnChancePerCheck = 0.3f; // 30% chance an owl is there when checked

        private GameObject _currentOwlInstance;

        private void Start()
        {
            if (InputHandler.Instance != null)
                InputHandler.Instance.OnInteract += HandleInputInteraction;

            CheckForArrival();
        }

        private void OnDestroy()
        {
            if (InputHandler.Instance != null)
                InputHandler.Instance.OnInteract -= HandleInputInteraction;
        }

        public void CheckForArrival()
        {
            // TODO: Add Time Check Logic here (LastLoginTime vs DateTime.Now)

            // For now, let's force a spawn check for testing
            bool shouldSpawn = Random.value < 0.8f;

            if (shouldSpawn && _currentOwlInstance == null)
            {
                SpawnRandomOwl();
            }
        }

        private void SpawnRandomOwl()
        {
            if (PowlDatabase.Instance == null) return;

            PowlSO selectedOwl = PowlDatabase.Instance.GetRandomPowlWeighted();

            if (selectedOwl != null)
            {
                _currentOwlInstance = Instantiate(owlPrefab, aviarySpawnPoint.position, Quaternion.identity);
                OwlUnit unit = _currentOwlInstance.GetComponent<OwlUnit>();
                unit.Setup(selectedOwl);

                Debug.Log($"A wild {selectedOwl.powlName} appeared!");
            }
        }


        private void HandleInputInteraction(Vector2 screenPos)
        {
            Camera mainCam = Camera.main;
            if (mainCam == null) return;

            Vector2 worldPoint = mainCam.ScreenToWorldPoint(screenPos);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                OwlUnit owl = hit.collider.GetComponent<OwlUnit>();
                if (owl != null)
                {
                    owl.OnInteract();
                }
            }
        }
    }
}