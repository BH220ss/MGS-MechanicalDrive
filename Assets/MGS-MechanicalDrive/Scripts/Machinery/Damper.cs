/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Damper.cs
 *  Description  :  Define Damper component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// State of damper.
    /// </summary>
    public enum DamperState
    {
        Accelerating = 0,
        Decelerating = 1,
        Stop = 2
    }

    /// <summary>
    /// Damper for engine power.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Damper")]
    [RequireComponent(typeof(Engine))]
    public class Damper : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// AnimationCurve for damper acceleration.
        /// </summary>
        public AnimationCurve acceleration = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(2, 100) });

        /// <summary>
        /// AnimationCurve for damper deceleration.
        /// </summary>
        public AnimationCurve deceleration = new AnimationCurve(new Keyframe[] { new Keyframe(0, 100), new Keyframe(3, 0) });

        /// <summary>
        /// State of damper.
        /// </summary>
        protected DamperState state = DamperState.Accelerating;

        /// <summary>
        /// Timer for animation curve.
        /// </summary>
        protected float timer = 0;

        /// <summary>
        /// Damper attached engine.
        /// </summary>
        protected Engine engine;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            engine = GetComponent<Engine>();
        }

        protected virtual void Update()
        {
            if (state == DamperState.Accelerating)
            {
                timer += Time.deltaTime;
                engine.power = acceleration.Evaluate(timer);
                if (timer >= acceleration[acceleration.length - 1].time)
                {
                    timer = 0;
                    state = DamperState.Stop;
                    enabled = false;
                }
            }
            else if (state == DamperState.Decelerating)
            {
                timer += Time.deltaTime;
                engine.power = deceleration.Evaluate(timer);
                if (timer >= deceleration[deceleration.length - 1].time)
                {
                    engine.enabled = false;
                    engine.power = timer = 0;
                    state = DamperState.Stop;
                    enabled = false;
                }
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Begin accelerate engine's power.
        /// </summary>
        public virtual void BeginAccelerate()
        {
            if (engine.power == 0)
            {
                state = DamperState.Accelerating;
                enabled = true;
            }
        }

        /// <summary>
        /// Begin decelerate engine's power.
        /// </summary>
        public virtual void BeginDecelerate()
        {
            if (engine.power != 0 && state == DamperState.Stop)
            {
                state = DamperState.Decelerating;
                enabled = true;
            }
        }
        #endregion
    }
}