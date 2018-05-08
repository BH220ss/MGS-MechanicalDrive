/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CentrifugalVibratorEditor.cs
 *  Description  :  Custom editor for CentrifugalVibrator.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(CentrifugalVibrator), true)]
    [CanEditMultipleObjects]
    public class CentrifugalVibratorEditor : BaseMEditor
    {
        #region Field and Property
        protected CentrifugalVibrator Target { get { return target as CentrifugalVibrator; } }

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
            DrawSphereCap(StartPosition, Quaternion.identity, NodeSize);
            DrawSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(StartPosition, Target.transform.rotation, Target.amplitudeRadius);

            DrawSphereArrow(StartPosition, Target.transform.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(StartPosition, Target.transform.forward, ArrowLength, NodeSize, Blue, "Axis");
        }
        #endregion
    }
}