/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LinearVibratorEditor.cs
 *  Description  :  Custom editor for LinearVibrator.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(LinearVibrator), true)]
    [CanEditMultipleObjects]
    public class LinearVibratorEditor : GenericEditor
    {
        #region Field and Property
        protected LinearVibrator Target { get { return target as LinearVibrator; } }

        protected Vector3 StartPosition
        {
            get
            {
                if (Application.isPlaying)
                {
                    if (Target.transform.parent)
                        return Target.transform.parent.TransformPoint(Target.StartPosition);
                    else
                        return Target.StartPosition;
                }
                else
                    return Target.transform.position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawAdaptiveSphereCap(StartPosition, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);

            DrawSphereArrow(StartPosition, Target.transform.forward, -Target.amplitudeRadius, NodeSize);
            DrawSphereArrow(StartPosition, Target.transform.forward, Target.amplitudeRadius, NodeSize);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
                Target.amplitudeRadius = Mathf.Max(Mathf.Epsilon, Target.amplitudeRadius);
        }
        #endregion
    }
}