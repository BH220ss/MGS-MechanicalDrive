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
    public class Gear : CoaxeMechanism
    {
        #region Field and Property
        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive gear by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            var angular = velocity / radius * Mathf.Rad2Deg;
            transform.Rotate(Vector3.forward, angular * Time.deltaTime, Space.Self);
            DriveCoaxes(angular);
            DriveEngages(-velocity);
        }

        /// <summary>
        /// Drive gear by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void AngularDrive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity * Time.deltaTime, Space.Self);
            DriveCoaxes(velocity);
            DriveEngages(-velocity * Mathf.Deg2Rad * radius);
        }
        #endregion
    }
}