using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float gizmoRadius = 0.2f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(getPoint(i), gizmoRadius);
                Gizmos.DrawLine(getPoint(i), getPoint(getNextIndex(i)));
            }
        }

        public int getNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            else
            {
                return i + 1;
            }
        }

        public Vector3 getPoint(int i)
        {
            return transform.GetChild(i).position; 
        }
    }
}
