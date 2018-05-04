/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Transmission.cs
 *  Description  :  Define Transmission component.
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
    /// Transmission for Mechanisms.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Transmission")]
    public class Transmission : BaseMechanism
    {
        #region Property and Field
        /// <summary>
        /// Mechanism units drive by this Transmission.
        /// </summary>
        public MechanismUnit[] mechanismUnits;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive mechanisms.
        /// </summary>
        /// <param name="speed">Line speed.</param>
        public override void Drive(float speed)
        {
            foreach (var unit in mechanismUnits)
            {
                unit.mechanism.Drive(speed * unit.ratio);
            }
        }
        #endregion
    }
}