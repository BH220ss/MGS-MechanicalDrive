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

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Gear rotate around axis Z.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Gear")]
    public class Gear : EngageGear
    {
        #region Field and Property
        /// <summary>
        /// Conjugated mechanism.
        /// </summary>
        public List<AngularMechanism> conjugates = new List<AngularMechanism>();
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive conjugated mechanism by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        protected void DriveConjugates(float velocity)
        {
            foreach (var conjugate in conjugates)
            {
                conjugate.AngularDrive(velocity);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive gear by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            var angularVelocity = velocity / radius * Mathf.Rad2Deg;
            transform.Rotate(Vector3.forward, angularVelocity * Time.deltaTime, Space.Self);

            DriveConjugates(angularVelocity);
            DriveEngages(velocity);
        }

        /// <summary>
        /// Drive gear by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void AngularDrive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity * Time.deltaTime, Space.Self);

            DriveConjugates(velocity);
            DriveEngages(velocity * Mathf.Deg2Rad * radius);
        }
        #endregion
    }
}