using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Cameras
{
	public class CameraFollow : AbstractTargetFollower
	{
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
			transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime * Time.deltaTime);
		}
	}
}