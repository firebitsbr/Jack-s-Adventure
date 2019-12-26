using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingManager : MonoBehaviour
    {
        const string defaultFile = "save";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                loadManager();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                saveManager();
            }
        }

        public void loadManager()
        {
            GetComponent<SavingSystem>().Load(defaultFile);
        }

        public void saveManager()
        {
            GetComponent<SavingSystem>().Save(defaultFile);
        }
    }
}