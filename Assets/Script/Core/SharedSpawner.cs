using UnityEngine;

namespace RPG.Core
{
    public class SharedSpawner : MonoBehaviour
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
                generateObject();
                hasSpawned = true;
            }
        }

        private void generateObject()
        {
            GameObject shared = Instantiate(sharedObject);
            DontDestroyOnLoad(shared);
        }
    }
}