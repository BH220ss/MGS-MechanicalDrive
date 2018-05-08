/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Gear.cs
 *  Description  :  Define Gear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Gear rotate around axis Z.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Gear")]
    public class Gear : BaseMechanism
    {
        #region Field and Property
        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive gear.
        /// </summary>
        /// <param name="speed">Line speed.</param>
        public override void Drive(float speed)
        {
            transform.Rotate(Vector3.forward, speed / radius * Time.deltaTime, Space.Self);
        }
        #endregion
    }
}