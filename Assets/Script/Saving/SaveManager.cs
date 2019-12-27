using System.Collections;
using RPG.SceneManagement;
using UnityEngine;

namespace RPG.Saving
{
    public class SaveManager : MonoBehaviour
    {
        const string defaultFile = "save";
        [SerializeField] float fadeInTime = 0.5f;

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.fadeOutQuick();
            yield return GetComponent<SavingSystem>().loadLastScene(defaultFile);
            yield return fader.fadeIn(fadeInTime);
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                load();
            }
        }

        public void load()
        {
            GetComponent<SavingSystem>().Load(defaultFile);
        }

        public void save()
        {
            GetComponent<SavingSystem>().Save(defaultFile);
        }
    }
}