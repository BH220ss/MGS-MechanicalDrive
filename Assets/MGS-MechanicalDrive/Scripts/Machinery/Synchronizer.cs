/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Synchronizer.cs
 *  Description  :  Define Synchronizer component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Synchronizer for mechanisms.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Synchronizer")]
    public class Synchronizer : BaseMechanism
    {
        #region Property and Field
        /// <summary>
        /// Mechanisms drive by this synchronizer.
        /// </summary>
        public BaseMechanism[] mechanisms;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive mechanisms.
        /// </summary>
        /// <param name="speed">Line speed.</param>
        public override void Drive(float speed)
        {
            foreach (var mechanism in mechanisms)
            {
                mechanism.Drive(speed);
            }
        }
        #endregion
    }
}