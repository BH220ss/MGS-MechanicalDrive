/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GearEditor.cs
 *  Description  :  Custom editor for GearMechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(GearMechanism), true)]
    [CanEditMultipleObjects]
    public class GearEditor : BaseMEditor
    {
        #region Field and Property
        protected GearMechanism Target { get { return target as GearMechanism; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, Target.radius);
            DrawSphereArrow(Target.transform.position, Target.transform.forward, ArrowLength, NodeSize, Blue, "Axis");
        }
        #endregion
    }
}