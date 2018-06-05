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
    public class CoaxialGear : Gear
    {
        #region Public Method
        /// <summary>
        /// Drive gear by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity, DriveType type)
        {
            var angular = velocity;
            var linear = velocity;

            if (type == DriveType.Linear)
                angular = velocity / radius * Mathf.Rad2Deg;
            else
                linear = velocity * Mathf.Deg2Rad * radius;

            DriveCoaxes(angular);
            DriveEngages(-linear);
        }
        #endregion
    }
}