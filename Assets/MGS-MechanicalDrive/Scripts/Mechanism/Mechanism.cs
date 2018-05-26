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
        /// <param name="engage">Engage mechanism.</param>
        void BuildEngage(IEngagedMechanism engage);

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        void BreakEngage(IEngagedMechanism engage);
    }

    /// <summary>
    /// Mechanism can be engaged to power mechanism.
    /// </summary>
    public interface IEngagedMechanism : IMechanism
    {
        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="engage">Power mechanism.</param>
        void EngageTo(IEngageMechanism engage);

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
        /// <param name="coaxe">Coaxe mechanism.</param>
        void BuildCoaxed(ICoaxedMechanism coaxe);

        /// <summary>
        /// Break coaxed.
        /// </summary>
        /// <param name="coaxe">Coaxe mechanism.</param>
        void BreakCoaxed(ICoaxedMechanism coaxe);
    }

    /// <summary>
    /// Mechanism can be coaxed to power mechanism.
    /// </summary>
    public interface ICoaxedMechanism : IAngularMechanism
    {
        /// <summary>
        /// Coaxe this mechanism to power mechanism.
        /// </summary>
        /// <param name="coaxe">Power mechanism.</param>
        void CoaxeTo(ICoaxeMechanism coaxe);

        /// <summary>
        /// Break coaxed from power mechanism.
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
        public List<Mechanism> engages;

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
            foreach (var engage in engages)
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
        /// <param name="engage">Engage mechanism.</param>
        public void BuildEngage(IEngagedMechanism engage)
        {
            var Mechanism = engage as Mechanism;
            if (Mechanism && !engages.Contains(Mechanism))
                engages.Add(Mechanism);
        }

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        public void BreakEngage(IEngagedMechanism engage)
        {
            var mechanism = engage as Mechanism;
            if (engages.Contains(mechanism))
                engages.Remove(mechanism);
        }

        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="engage">Power mechanism.</param>
        public void EngageTo(IEngageMechanism engage)
        {
            if (engage == null || engage == this.engage)
                return;
            else
            {
                EngageBreak();

                //Engage this mechanism to new power mechanism.
                engage.BuildEngage(this);
                this.engage = engage;
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
        /// <param name="velocity">Angular velocity.</param>
        protected void DriveCoaxes(float velocity)
        {
            foreach (var coaxe in coaxes)
            {
                if (coaxe is IAngularMechanism)
                    (coaxe as IAngularMechanism).AngularDrive(velocity);
                else
                    coaxe.Drive(velocity);
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