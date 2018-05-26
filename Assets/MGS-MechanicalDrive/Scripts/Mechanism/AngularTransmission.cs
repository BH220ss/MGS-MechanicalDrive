/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AngularTransmission.cs
 *  Description  :  Define AngularTransmission component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Angular transmission for mechanisms.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/AngularTransmission")]
    public class AngularTransmission : Mechanism, IAngularMechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanism drive by this transmission.
        /// </summary>
        public List<MechanismUnit> mechanismUnits = new List<MechanismUnit>();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Drive velocity.</param>
        public override void Drive(float velocity)
        {
            foreach (var unit in mechanismUnits)
            {
                unit.mechanism.Drive(velocity * unit.coefficient);
            }
        }

        /// <summary>
        /// Drive mechanisms by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public void AngularDrive(float velocity)
        {
            foreach (var unit in mechanismUnits)
            {
                if (unit.mechanism is IAngularMechanism)
                    (unit.mechanism as IAngularMechanism).AngularDrive(velocity * unit.coefficient);
                else
                    unit.mechanism.Drive(velocity * unit.coefficient);
            }
        }
        #endregion
    }
}