using UnityEngine;
using System.Collections;

public static class Utility {

	public static class Physics2D {
		public static Vector2 gravity = UnityEngine.Physics2D.gravity;
		
		public static float realForce(float f) {
			return (f / Time.fixedDeltaTime);
		}
	}
}
