using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using WEAPON;
using static UnityEngine.GraphicsBuffer;
using System;


#if UNITY_EDITOR
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
            //Fire type
            //                                                                Nombre       Variable que aparece
            _automaticRfileScript.fireType = (FireType)EditorGUILayout.EnumPopup("Fire Type", _automaticRfileScript.fireType);

            //General
            _automaticRfileScript.gunLaser = (TrailRenderer)EditorGUILayout.ObjectField("Gun laser", _automaticRfileScript.gunLaser, typeof(TrailRenderer), true);
            _automaticRfileScript.laserOrigin = (Transform)EditorGUILayout.ObjectField("Laser Origin", _automaticRfileScript.laserOrigin, typeof(Transform), true);

            _automaticRfileScript.raycastOrigin = (Transform)EditorGUILayout.ObjectField("Raycast Origin", _automaticRfileScript.raycastOrigin, typeof(Transform), true);

            _automaticRfileScript.bulletPrefabSprite = (GameObject)EditorGUILayout.ObjectField("Bullet Prefab Sprite", _automaticRfileScript.bulletPrefabSprite, typeof(GameObject), true);
            _automaticRfileScript.hitMask = EditorGUILayout.MaskField("Hit mask", _automaticRfileScript.hitMask, UnityEditorInternal.InternalEditorUtility.layers);

            _automaticRfileScript.rayDistance = EditorGUILayout.FloatField("Distancia del raycast", _automaticRfileScript.rayDistance);
            _automaticRfileScript.rayForce = EditorGUILayout.FloatField("Fuerza del raycast", _automaticRfileScript.rayForce);
            //_automaticRfileScript.damage = EditorGUILayout.IntField("Daño del arma", _automaticRfileScript.damage);
            //_automaticRfileScript.actualAmmo = EditorGUILayout.IntField("Munición actual", _automaticRfileScript.actualAmmo);
            //_automaticRfileScript.fireRate = EditorGUILayout.FloatField("Fire rate", _automaticRfileScript.fireRate);
            //_automaticRfileScript.maxAmmo = EditorGUILayout.IntField("Munición total posible", _automaticRfileScript.maxAmmo);

            //Reload parameters
            //_automaticRfileScript.magazineAmmo = EditorGUILayout.IntField("Munición del cartucho", _automaticRfileScript.magazineAmmo);
            //_automaticRfileScript.reloadTime = EditorGUILayout.FloatField("Tiempo de recarga", _automaticRfileScript.reloadTime);


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

#endif
