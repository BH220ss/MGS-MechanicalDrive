/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Worm.cs
 *  Description  :  Define Worm component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/18/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Worm shaft.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Worm")]
    public class Worm : Gear
    {
        #region Field and Property
        /// <summary>
        /// Count of worm threads.
        /// </summary>
        public int threads = 1;

        /// <summary>
        /// Gears drived by this worm.
        /// </summary>
        public List<WormGear> gears;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive worm gears by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        protected void DriveGears(float velocity)
        {
            foreach (var gear in gears)
            {
                gear.AngularDrive(velocity * threads / gear.teeth);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive worm by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            base.Drive(velocity);
            DriveGears(velocity / radius * Mathf.Rad2Deg);
        }

        /// <summary>
        /// Drive worm by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void AngularDrive(float velocity)
        {
            base.AngularDrive(velocity);
            DriveGears(velocity);
        }
        #endregion
    }
}