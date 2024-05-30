using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Abyss.StartupManager
{
	public class DefaultLoadingScreen : LoadingScreen
	{
		#region UnitySerialized
		[SerializeField] private Text PercentsText;
		[SerializeField] private Text MessageText;
		[SerializeField] private Slider Slider;
		#endregion

		#region Private Members
		private void UpdateProgress(float progress)
		{
			Slider.value = progress;
			PercentsText.text = progress.ToString(CultureInfo.InvariantCulture);
		}

		private void UpdatedMessage(string message)
		{
			MessageText.text = $"Message: {message}";
		}
		#endregion

		#region Overrides
		public override void Report(float value)
		{
			UpdateProgress(value);
		}

		public override void Report(string message)
		{
			UpdatedMessage(message);
		}

		public override void Report(float value, string message)
		{
			UpdatedMessage(message);
			UpdateProgress(value);
		}

		public override void Open()
		{
			gameObject.SetActive(true);
		}

		public override void Close()
		{
			gameObject.SetActive(false);
		}
		#endregion
	}
}