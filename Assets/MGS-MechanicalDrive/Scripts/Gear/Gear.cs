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
    public class Gear : GearMechanism
    {
        #region Field and Property
        /// <summary>
        /// Coaxial mechanism.
        /// </summary>
        public List<AngularMechanism> coaxials = new List<AngularMechanism>();
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive coaxial mechanism by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        protected void DriveCoaxials(float velocity)
        {
            foreach (var coaxial in coaxials)
            {
                coaxial.AngularDrive(velocity);
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
            base.Drive(velocity);
            DriveCoaxials(velocity / radius * Mathf.Rad2Deg);
        }

        /// <summary>
        /// Drive gear by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void AngularDrive(float velocity)
        {
            base.AngularDrive(velocity);
            DriveCoaxials(velocity);
        }
        #endregion
    }
}