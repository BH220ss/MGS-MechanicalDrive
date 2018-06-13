/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AxleEditor.cs
 *  Description  :  Custom editor for Axle.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/13/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(Axle), true)]
    [CanEditMultipleObjects]
    public class AxleEditor : BaseMEditor
    {
        #region Field and Property
        protected Axle Target { get { return target as Axle; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, NodeSize + 0.025f);
            DrawSphereArrow(Target.transform.position, Target.transform.forward, ArrowLength, NodeSize, Blue, "Axis");
        }
        #endregion
    }
}