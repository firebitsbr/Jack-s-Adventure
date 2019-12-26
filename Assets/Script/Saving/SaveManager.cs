using UnityEngine;

namespace RPG.Saving
{
    public class SaveManager : MonoBehaviour
    {
        const string defaultFile = "save";

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<SavingSystem>().Save(defaultFile);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SavingSystem>().Load(defaultFile);
            }
        }
    }
}