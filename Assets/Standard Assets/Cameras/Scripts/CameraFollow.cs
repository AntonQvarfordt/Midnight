using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class CameraFollow : AbstractTargetFollower
	{
        public Vector3 Offset;
		[SerializeField]
		private float smoothTime = 5;
		private Vector3 velocity;

		public void Init (Transform target)
		{
			SetTarget(target);
			this.enabled = true;
		}

		protected override void FollowTarget(float deltaTime)
		{
			var targetPos = Target.position;
			targetPos.y = transform.position.y;
            targetPos += Offset;
			transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime * Time.deltaTime);
		}
	}
}