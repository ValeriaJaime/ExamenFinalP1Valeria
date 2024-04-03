using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using WEAPON;
using static UnityEngine.GraphicsBuffer;
using System;

namespace WEAPON
{
    // Esta linea, indica que script o que parte del editor va
    // a modificar
    [CustomEditor(typeof(AutomaticRfile))]
    public class EditorAutomaticRfile : Editor
    {
        private AutomaticRfile _automaticRfileScript;

        private void OnEnable()
        {
            // Se asigna el target, es decir, el script objetivo a modificar
            _automaticRfileScript = (AutomaticRfile)target;
        }

        // Sobreescribe el inspector
        public override void OnInspectorGUI()
        {
            //                                                                Nombre       Variable que aparece
            _automaticRfileScript.fireType = (FireType)EditorGUILayout.EnumPopup("Fire Type", _automaticRfileScript.fireType);
            _automaticRfileScript.laserOrigin = (Transform)EditorGUILayout.ObjectField("Laser Origin", _automaticRfileScript.laserOrigin, typeof(Transform), true);
            _automaticRfileScript.proyectile = (GameObject)EditorGUILayout.ObjectField("Proyectil", _automaticRfileScript.proyectile, typeof(GameObject), true);
            _automaticRfileScript.rayDistance = EditorGUILayout.FloatField("Distancia del raycast", _automaticRfileScript.rayDistance);
            _automaticRfileScript.hitMask = EditorGUILayout.MaskField("Hit mask", _automaticRfileScript.hitMask, UnityEditorInternal.InternalEditorUtility.layers);

            switch (_automaticRfileScript.fireType)
            {
                case FireType.Automatic:
                    {
                        break;
                    }

                case FireType.Burst:
                    {
                        _automaticRfileScript.bulletPerBurst = EditorGUILayout.IntField("Cantidad de balas por burst", _automaticRfileScript.bulletPerBurst);
                        break;
                    }
            }
        }
    }
}
