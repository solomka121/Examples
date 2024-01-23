using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CircleDiagram
{
    [CustomEditor(typeof(Diagram))]
    public class DiagramEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
         
            Diagram diagram = (Diagram) target;

            if (GUILayout.Button("GenerateCircle"))
            {
                diagram.GenerateCircleEditor();
            }
            
            if (GUILayout.Button("Update Parts"))
            {
                diagram.UpdatePartsEditor();
            }
        }
    }

}