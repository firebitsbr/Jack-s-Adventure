using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = getPath(saveFile);
            print("Save to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, captureState());
            };
        }

        public void Load(string saveFile)
        {
            string path = getPath(saveFile);
            print("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                restoreState(formatter.Deserialize(stream));
            }
        }

        private void restoreState(object restore)
        {
            Dictionary<string, object> state = (Dictionary<string, object>)restore;
            foreach (SavableEntity i in FindObjectsOfType<SavableEntity>())
            {
                i.restoreState(state[i.getUniqueIdentifier()]);
            }

        }

        private object captureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (SavableEntity i in FindObjectsOfType<SavableEntity>())
            {
                state[i.getUniqueIdentifier()] = i.captureState();
            }
            return state;
        }


        private string getPath(string saveFile)
        {
            return Path.Combine(Application.dataPath, saveFile + ".sav");
        }
    }
}