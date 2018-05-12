/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CoaxialGear.cs
 *  Description  :  Define CoaxialGear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/12/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// coaxial gear with the same axis as another gear.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/CoaxialGear")]
    public class CoaxialGear : EngageGear
    {
        #region Public Method
        /// <summary>
        /// This method does not work.
        /// </summary>
        public override void LinearDrive(float velocity) { }

        /// <summary>
        /// Drive gear by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void AngularDrive(float velocity)
        {
            DriveEngageMechanisms(velocity * Mathf.Deg2Rad * radius);
        }
        #endregion
    }
}