/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CentrifugalVibrator.cs
 *  Description  :  Define CentrifugalVibrator component.
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
    /// Centrifugal vibrator.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/CentrifugalVibrator")]
    public class CentrifugalVibrator : Mechanism, IAngularMechanism
    {
        #region Field and Property
        /// <summary>
        /// Amplitude radius of vibrator.
        /// </summary>
        public float amplitudeRadius = 0.1f;

        /// <summary>
        /// Start loacal position.
        /// </summary>
        public Vector3 StartPosition { protected set; get; }

        /// <summary>
        /// Current rotate angle.
        /// </summary>
        protected float currentAngle;
        #endregion

        #region Protected Method
        protected override void Awake()
        {
            base.Awake();
            StartPosition = transform.localPosition;
        }

        /// <summary>
        /// Get local direction from wold direction.
        /// </summary>
        /// <param name="direction">Wold direction.</param>
        /// <returns>Local direction.</returns>
        protected Vector3 GetLocalDirection(Vector3 direction)
        {
            if (transform.parent)
                return transform.parent.InverseTransformVector(direction);
            else
                return direction;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive vibrator by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void Drive(float velocity)
        {
            AngularDrive(velocity);
        }

        /// <summary>
        /// Drive vibrator by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public void AngularDrive(float velocity)
        {
            currentAngle += velocity * Time.deltaTime;
            var direction = Quaternion.AngleAxis(currentAngle, transform.forward) * transform.right;
            transform.localPosition = StartPosition + GetLocalDirection(direction) * amplitudeRadius;
        }
        #endregion
    }
}