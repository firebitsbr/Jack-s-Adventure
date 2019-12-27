using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SavableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";

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
            if (prop.stringValue == "")
            {
                prop.stringValue = System.Guid.NewGuid().ToString();
                obj.ApplyModifiedProperties();
            }

        }

        public string getUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object captureState()
        {
            return new SerializableVector(transform.position);
        }

        public void restoreState(object state)
        {
            SerializableVector position = (SerializableVector)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.toVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionSchedular>().CancelCurrentAction();
        }
    }
}