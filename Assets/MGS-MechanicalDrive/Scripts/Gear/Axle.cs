/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Axle.cs
 *  Description  :  Define Axle component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/5/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Axle rotate around axis Z.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Axle")]
    public class Axle : Mechanism, ICoaxeMechanism, ICoaxedMechanism
    {
        #region Field and Property
        /// <summary>
        /// Coaxial mechanism.
        /// </summary>
        public List<Mechanism> coaxes;

        /// <summary>
        /// Coaxial power mechanism.
        /// </summary>
        protected ICoaxeMechanism coaxe;
        #endregion

        #region Protected Method
        /// <summary>
        /// Initialize coaxes.
        /// </summary>
        protected void InitializeCoaxes()
        {
            foreach (var coaxe in coaxes)
            {
                if (coaxe is ICoaxedMechanism)
                {
                    (coaxe as ICoaxedMechanism).CoaxeTo(this);
                }
            }
        }

        /// <summary>
        /// Drive coaxial mechanisms by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity of drive.</param>
        protected void DriveCoaxes(float velocity)
        {
            foreach (var coaxe in coaxes)
            {
                coaxe.Drive(velocity, DriveType.Angular);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize axle.
        /// </summary>
        public override void Initialize()
        {
            InitializeCoaxes();
        }

        /// <summary>
        /// Drive axle by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity of drive.</param>
        /// <param name="type">Invalid parameter (Axle can only drived by angular velocity).</param>
        public override void Drive(float velocity, DriveType type = DriveType.Ignore)
        {
            transform.Rotate(Vector3.forward, velocity * Time.deltaTime, Space.Self);
            DriveCoaxes(velocity);
        }

        /// <summary>
        /// Build coaxe for mechanism.
        /// </summary>
        /// <param name="coaxe">Coaxe mechanism.</param>
        public void BuildCoaxed(ICoaxedMechanism coaxe)
        {
            var mechanism = coaxe as Mechanism;
            if (mechanism && !coaxes.Contains(mechanism))
                coaxes.Add(mechanism);
        }

        /// <summary>
        /// Break coaxed.
        /// </summary>
        /// <param name="coaxe">Coaxe mechanism.</param>
        public void BreakCoaxed(ICoaxedMechanism coaxe)
        {
            var mechanism = coaxe as Mechanism;
            if (coaxes.Contains(mechanism))
                coaxes.Remove(mechanism);
        }

        /// <summary>
        /// Coaxe this mechanism to power mechanism.
        /// </summary>
        /// <param name="coaxe">Power mechanism.</param>
        public void CoaxeTo(ICoaxeMechanism coaxe)
        {
            if (coaxe == null || coaxe == this.coaxe)
                return;
            else
            {
                CoaxeBreak();

                //Coaxe this mechanism to new power mechanism.
                coaxe.BuildCoaxed(this);
                this.coaxe = coaxe;
            }
        }

        /// <summary>
        /// Break coaxed from power mechanism.
        /// </summary>
        public void CoaxeBreak()
        {
            if (coaxe != null)
            {
                coaxe.BreakCoaxed(this);
                coaxe = null;
            }
        }
        #endregion
    }
}