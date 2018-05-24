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
    public interface IAngularMechanism : IMechanism
    {
        /// <summary>
        /// Drive Mechanism by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        void AngularDrive(float velocity);
    }

    /// <summary>
    /// Mechanism with engaged mechanisms.
    /// </summary>
    public interface IEngageMechanism : IMechanism
    {
        /// <summary>
        /// Build engage for mechanism.
        /// </summary>
        /// <param name="mechanism">Engage mechanism.</param>
        void BuildEngage(IEngagedMechanism mechanism);

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="mechanism">Engage mechanism.</param>
        void BreakEngage(IEngagedMechanism mechanism);
    }

    /// <summary>
    /// Mechanism can be engaged to power mechanism.
    /// </summary>
    public interface IEngagedMechanism : IMechanism
    {
        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="mechanism">Power mechanism.</param>
        void EngageTo(IEngageMechanism mechanism);

        /// <summary>
        /// Break engage from power mechanism.
        /// </summary>
        void EngageBreak();
    }

    /// <summary>
    /// Mechanism with coaxed mechanisms.
    /// </summary>
    public interface ICoaxeMechanism : IAngularMechanism
    {
        /// <summary>
        /// Build coaxe for mechanism.
        /// </summary>
        /// <param name="mechanism">Coaxed mechanism.</param>
        void BuildCoaxed(ICoaxedMechanism mechanism);

        /// <summary>
        /// Break coaxed.
        /// </summary>
        /// <param name="mechanism">Coaxed mechanism.</param>
        void BreakCoaxed(ICoaxedMechanism mechanism);
    }

    /// <summary>
    /// Mechanism can be coaxed to power mechanism.
    /// </summary>
    public interface ICoaxedMechanism : IAngularMechanism
    {
        /// <summary>
        /// Coaxe this mechanism to power mechanism.
        /// </summary>
        /// <param name="mechanism">Power mechanism.</param>
        void CoaxeTo(ICoaxeMechanism mechanism);

        /// <summary>
        /// Break coaxe from power mechanism.
        /// </summary>
        void CoaxeBreak();
    }

    /// <summary>
    /// Mechanism with engage mechanisms.
    /// </summary>
    public abstract class EngageMechanism : Mechanism, IEngageMechanism, IEngagedMechanism
    {
        #region Field and Property
        /// <summary>
        /// Engaged mechanisms.
        /// </summary>
        [SerializeField]
        protected List<Mechanism> engages;

        /// <summary>
        /// Engaged mechanisms.
        /// </summary>
        protected List<IEngagedMechanism> iEngages;

        /// <summary>
        /// Engage power mechanism.
        /// </summary>
        protected IEngageMechanism engage;
        #endregion

        #region Protected Method
        /// <summary>
        /// Initialize engages.
        /// </summary>
        protected void InitializeEngages()
        {
            foreach (var engage in engages)
            {
                if (engage is IEngagedMechanism)
                {
                    (engage as IEngagedMechanism).EngageTo(this);
                }
            }
        }

        /// <summary>
        /// Drive Engaged mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        protected void DriveEngages(float velocity)
        {
            foreach (var engage in iEngages)
            {
                engage.Drive(velocity);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public override void Initialize()
        {
            InitializeEngages();
        }

        /// <summary>
        /// Build engage for mechanism.
        /// </summary>
        /// <param name="mechanism">Engage mechanism.</param>
        public void BuildEngage(IEngagedMechanism mechanism)
        {
            if (!iEngages.Contains(mechanism))
                iEngages.Add(mechanism);
        }

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="mechanism">Engage mechanism.</param>
        public void BreakEngage(IEngagedMechanism mechanism)
        {
            if (iEngages.Contains(mechanism))
                iEngages.Remove(mechanism);
        }

        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="mechanism">Power mechanism.</param>
        public void EngageTo(IEngageMechanism mechanism)
        {
            if (mechanism == null || mechanism == engage)
                return;
            else
            {
                EngageBreak();

                //Engage this mechanism to new power mechanism.
                mechanism.BuildEngage(this);
                engage = mechanism;
            }
        }

        /// <summary>
        /// Break engage from power mechanism.
        /// </summary>
        public void EngageBreak()
        {
            if (engage != null)
            {
                engage.BreakEngage(this);
                engage = null;
            }
        }
        #endregion
    }

    /// <summary>
    /// Mechanism with engage and coaxial mechanisms.
    /// </summary>
    public abstract class CoaxeMechanism : EngageMechanism, ICoaxeMechanism, ICoaxedMechanism
    {
        #region Field and Property
        /// <summary>
        /// Coaxial mechanism.
        /// </summary>
        [SerializeField]
        protected List<Mechanism> coaxes;

        /// <summary>
        /// Coaxial mechanism.
        /// </summary>
        protected List<ICoaxedMechanism> iCoaxes = new List<ICoaxedMechanism>();

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
        /// <param name="velocity">Angular velocity.</param>
        protected void DriveCoaxes(float velocity)
        {
            foreach (var coaxe in iCoaxes)
            {
                coaxe.AngularDrive(velocity);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            InitializeCoaxes();
        }

        /// <summary>
        /// Drive Mechanism by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public abstract void AngularDrive(float velocity);

        /// <summary>
        /// Build coaxe for mechanism.
        /// </summary>
        /// <param name="mechanism">Coaxed mechanism.</param>
        public void BuildCoaxed(ICoaxedMechanism mechanism)
        {
            if (!iCoaxes.Contains(mechanism))
                iCoaxes.Add(mechanism);
        }

        /// <summary>
        /// Break coaxed.
        /// </summary>
        /// <param name="mechanism">Coaxed mechanism.</param>
        public void BreakCoaxed(ICoaxedMechanism mechanism)
        {
            if (iCoaxes.Contains(mechanism))
                iCoaxes.Remove(mechanism);
        }

        /// <summary>
        /// Coaxe this mechanism to power mechanism.
        /// </summary>
        /// <param name="mechanism">Power mechanism.</param>
        public void CoaxeTo(ICoaxeMechanism mechanism)
        {
            if (mechanism == null || mechanism == coaxe)
                return;
            else
            {
                CoaxeBreak();

                //Coaxe this mechanism to new power mechanism.
                mechanism.BuildCoaxed(this);
                coaxe = mechanism;
            }
        }

        /// <summary>
        /// Break coaxe from power mechanism.
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