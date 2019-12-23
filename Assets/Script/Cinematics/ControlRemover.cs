using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematic
{
    public class ControlRemover : MonoBehaviour
    {
        private GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += disableControl;
            GetComponent<PlayableDirector>().stopped += enableControl;
        }

        void disableControl(PlayableDirector _)
        {

            player.GetComponent<ActionSchedular>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void enableControl(PlayableDirector _)
        {
            player.GetComponent<PlayerController>().enabled = true;
            print("Enabled");
        }
    }
}