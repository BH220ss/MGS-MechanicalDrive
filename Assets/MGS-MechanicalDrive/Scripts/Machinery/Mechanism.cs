/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract BaseMechanism and EngageGear.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  5/3/2018
 *  Description  :  Optimize BaseMechanism and define EngageGear.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Mechanism unit.
    /// </summary>
    [Serializable]
    public struct MechanismUnit
    {
        #region Field and Property
        /// <summary>
        /// Mechanism to drive.
        /// </summary>
        public BaseMechanism mechanism;

        /// <summary>
        /// Ratio of linear velocity.
        /// </summary>
        public float ratio;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mechanism">Mechanism to drive.</param>
        /// <param name="ratio">Ratio of linear velocity.</param>
        public MechanismUnit(BaseMechanism mechanism, float ratio)
        {
            this.mechanism = mechanism;
            this.ratio = ratio;
        }
        #endregion
    }

    /// <summary>
    /// Base mechanism.
    /// </summary>
    public abstract class BaseMechanism : MonoBehaviour
    {
        #region Public Method
        /// <summary>
        /// Drive mechanism by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public abstract void LinearDrive(float velocity);
        #endregion
    }

    /// <summary>
    /// Gear with engage mechanisms.
    /// </summary>
    public abstract class EngageGear : BaseMechanism
    {
        #region Field and Property
        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;

        /// <summary>
        /// Engaged mechanisms.
        /// </summary>
        public List<BaseMechanism> engageMechanisms;

        /// <summary>
        /// Power gear.
        /// </summary>
        protected EngageGear powerGear;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive Engaged mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        protected void DriveEngageMechanisms(float velocity)
        {
            foreach (var mechanism in engageMechanisms)
            {
                mechanism.LinearDrive(velocity);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive gear by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void LinearDrive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity / radius * Mathf.Rad2Deg * Time.deltaTime, Space.Self);
            DriveEngageMechanisms(velocity);
        }

        /// <summary>
        /// Drive gear by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public virtual void AngularDrive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity * Time.deltaTime, Space.Self);
            DriveEngageMechanisms(velocity * Mathf.Deg2Rad * radius);
        }

        /// <summary>
        /// Engage this gear to power gear.
        /// </summary>
        /// <param name="gear"></param>
        public void EngageTo(EngageGear gear)
        {
            if (gear == null || gear == powerGear)
                return;
            else
            {
                BreakEngage();

                if (!gear.engageMechanisms.Contains(this))
                {
                    //Engage this gear to new power gear.
                    gear.engageMechanisms.Add(this);
                }
                powerGear = gear;
            }
        }

        /// <summary>
        /// Break engage from power gear.
        /// </summary>
        public void BreakEngage()
        {
            if (powerGear != null)
            {
                powerGear.engageMechanisms.Remove(this);
                powerGear = null;
            }
        }
        #endregion
    }
}