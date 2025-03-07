using System;
using UnityEditor;
using UnityEngine;

namespace TigerFrogGames
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : Editor
    {
        private void OnSceneGUI()
        {
            CustomButton customButton = (CustomButton)target;
        }
    }
}