using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum Destiniation
        {
            A, B, C
        }

        [SerializeField] int nextScene = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Destiniation dest;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }

        }

        private IEnumerator Transition()
        {
            if (nextScene < 0)
            {
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.fadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(nextScene);

            updatePlayer(getPortal());
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.fadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private Portal getPortal()
        {
            foreach (Portal i in FindObjectsOfType<Portal>())
            {
                if (i == this)
                {
                    continue;
                }
                if (i.dest != dest)
                {
                    continue;
                }
                return i;
            }
            return null;
        }

        private void updatePlayer(Portal portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(portal.spawnPoint.position);
            player.transform.rotation = portal.spawnPoint.rotation;
        }
    }
}