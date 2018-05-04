/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract BaseMechanism and MechanismUnit.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  5/3/2018
 *  Description  :  Optimize BaseMechanism define.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Mechanism unit.
    /// </summary>
    [Serializable]
    public struct MechanismUnit
    {
        /// <summary>
        /// Mechanism to drive.
        /// </summary>
        public BaseMechanism mechanism;

        /// <summary>
        /// Ratio of line speed.
        /// </summary>
        public float ratio;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mechanism">Mechanism to drive.</param>
        /// <param name="ratio">Ratio of line speed.</param>
        public MechanismUnit(BaseMechanism mechanism, float ratio)
        {
            this.mechanism = mechanism;
            this.ratio = ratio;
        }
    }

    /// <summary>
    /// Base mechanism.
    /// </summary>
    public abstract class BaseMechanism : MonoBehaviour
    {
        #region Public Method
        /// <summary>
        /// Drive mechanism.
        /// </summary>
        /// <param name="speed">Line speed.</param>
        public abstract void Drive(float speed);
        #endregion
    }
}