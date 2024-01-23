using UnityEngine;

namespace CircleDiagram
{
    [CreateAssetMenu(fileName = "DiagramPartData" , menuName = "Diagram/PartData")]
    public class DiagramPartSO : ScriptableObject
    {
        public float degrees = 45;
        public float width = 0.2f;
    }
}
