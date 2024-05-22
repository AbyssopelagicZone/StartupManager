using UnityEngine;

namespace Abyss.StartupManager.Samples
{
	public class Cube : MonoBehaviour
	{
		#region UnityMembers
		private void Update()
		{
			transform.rotation *= Quaternion.Euler(0, 0, 10 * Time.deltaTime);
		}
		#endregion
	}
}