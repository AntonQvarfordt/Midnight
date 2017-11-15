using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WhiteCat
{
	/// <summary>
	/// ScriptableComponent
	/// </summary>
	public class ScriptableComponent : MonoBehaviour
	{
		/// <summary>
		/// RectTransform for UI scripting
		/// </summary>
		public RectTransform rectTransform
		{
			get { return transform as RectTransform; }
		}

	} // class ScriptableComponent

} // namespace WhiteCat