using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using System.Collections.Generic;
using System;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SavableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        static Dictionary<string, SavableEntity> result = new Dictionary<string, SavableEntity>();

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject))
            {
                return;
            }
            if (string.IsNullOrEmpty(gameObject.scene.path))
            {
                return;
            }

            SerializedObject obj = new SerializedObject(this);
            SerializedProperty prop = obj.FindProperty("uniqueIdentifier");
            if (string.IsNullOrEmpty(prop.stringValue) || !isUnique(prop.stringValue))
            {
                prop.stringValue = System.Guid.NewGuid().ToString();
                obj.ApplyModifiedProperties();
            }

            result[prop.stringValue] = this;
        }
#endif

        private bool isUnique(string stringValue)
        {
            if (!result.ContainsKey(stringValue) || result[stringValue] == this)
            {
                return true;
            }
            if (result[stringValue] == null || result[stringValue].getUniqueIdentifier() != stringValue)
            {
                result.Remove(stringValue);
                return true;
            }
            return false;
        }

        public string getUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object captureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (Saveable i in GetComponents<Saveable>())
            {
                state[i.GetType().ToString()] = i.captureState();
            }
            return state;
        }

        public void restoreState(object restoreState)
        {
            Dictionary<string, object> state = (Dictionary<string, object>)restoreState;
            foreach (Saveable i in GetComponents<Saveable>())
            {
                string type = i.GetType().ToString();
                if (state.ContainsKey(type))
                {
                    i.restoreState(state[type]);
                }
            }
        }
    }
}