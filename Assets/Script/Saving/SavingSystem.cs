using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public IEnumerator loadLastScene(string saveFile)
        {
            Dictionary<string, object> state = loadFile(saveFile);
            if (state.ContainsKey("lastScene"))
            {
                int index = (int)state["lastScene"];
                if (index != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(index);
                }
            }
            restoreState(state);
        }

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = loadFile(saveFile);
            captureState(state);
            saveFileManager(saveFile, state);
        }

        public void Load(string saveFile)
        {
            restoreState(loadFile(saveFile));
        }

        private void restoreState(Dictionary<string, object> restore)
        {
            foreach (SavableEntity i in FindObjectsOfType<SavableEntity>())
            {
                string id = i.getUniqueIdentifier();
                if (restore.ContainsKey(id))
                {
                    i.restoreState(restore[id]);
                }

            }

        }

        private Dictionary<string, object> loadFile(string saveFile)
        {
            string path = getPath(saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void saveFileManager(string saveFile, object state)
        {
            string path = getPath(saveFile);
            print("Save to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            };
        }

        private void captureState(Dictionary<string, object> state)
        {
            foreach (SavableEntity i in FindObjectsOfType<SavableEntity>())
            {
                state[i.getUniqueIdentifier()] = i.captureState();
            }

            state["lastScene"] = SceneManager.GetActiveScene().buildIndex;
        }


        private string getPath(string saveFile)
        {
            return Path.Combine(Application.dataPath, saveFile + ".sav");
        }
    }
}