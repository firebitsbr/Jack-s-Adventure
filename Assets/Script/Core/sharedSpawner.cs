using UnityEngine;

namespace RPG.Core
{
    public class sharedSpawner : MonoBehaviour
    {
        [SerializeField] GameObject sharedObject;
        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned)
            {
                return;
            }
            else
            {
                spawnObject();
                hasSpawned = true;
            }
        }

        private void spawnObject()
        {
            GameObject obj = Instantiate(sharedObject);
            DontDestroyOnLoad(obj);
        }
    }
}