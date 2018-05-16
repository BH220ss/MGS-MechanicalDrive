/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract AngularMechanism, EngageMechanism
 *                  and GearMechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  5/16/2018
 *  Description  :  Optimize AngularMechanism, define EngageMechanism
 *                  and GearMechanism.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Mechanism can be drived by angular velocity.
    /// </summary>
    public abstract class AngularMechanism : Mechanism
    {
        #region Public Method
        /// <summary>
        /// Drive Mechanism by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public abstract void AngularDrive(float velocity);
        #endregion
    }

    /// <summary>
    /// Mechanism with engage mechanisms.
    /// </summary>
    public abstract class EngageMechanism : AngularMechanism
    {
        #region Field and Property
        /// <summary>
        /// Engaged mechanisms.
        /// </summary>
        public List<Mechanism> engages;

        /// <summary>
        /// Engage power mechanism.
        /// </summary>
        protected EngageMechanism engage;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive Engaged mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        protected void DriveEngages(float velocity)
        {
            foreach (var engage in engages)
            {
                engage.Drive(velocity);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive mechanism by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            DriveEngages(velocity);
        }

        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="mechanism">Power mechanism.</param>
        public void EngageTo(EngageMechanism mechanism)
        {
            if (mechanism == null || mechanism == engage)
                return;
            else
            {
                BreakEngage();

                if (!mechanism.engages.Contains(this))
                {
                    //Engage this mechanism to new power mechanism.
                    mechanism.engages.Add(this);
                }
                engage = mechanism;
            }
        }

        /// <summary>
        /// Break engage from power mechanism.
        /// </summary>
        public void BreakEngage()
        {
            if (engage != null)
            {
                engage.engages.Remove(this);
                engage = null;
            }
        }
        #endregion
    }

    /// <summary>
    /// Gear with engage mechanisms.
    /// </summary>
    public abstract class GearMechanism : EngageMechanism
    {
        #region Field and Property
        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive gear by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity / radius * Mathf.Rad2Deg * Time.deltaTime, Space.Self);
            base.Drive(velocity);
        }

        /// <summary>
        /// Drive gear by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void AngularDrive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity * Time.deltaTime, Space.Self);
            DriveEngages(velocity * Mathf.Deg2Rad * radius);
        }
        #endregion
    }
}