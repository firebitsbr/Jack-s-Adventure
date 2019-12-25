using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(fader());

        }

        IEnumerator fader()
        {
            yield return fadeOut(2f);
            yield return fadeIn(2f);

        }

        IEnumerator fadeOut(float time)
        {
            while (canvasGroup.alpha <= 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        
        IEnumerator fadeIn(float time)
        {
            while (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}