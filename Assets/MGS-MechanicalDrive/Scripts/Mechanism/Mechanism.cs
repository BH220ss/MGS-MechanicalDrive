/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define interface.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  5/16/2018
 *  Description  :  Add interface.
 *************************************************************************/

namespace Mogoson.Machinery
{
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
    public interface ICoaxeMechanism : IMechanism
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
    public interface ICoaxedMechanism : IMechanism
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
}