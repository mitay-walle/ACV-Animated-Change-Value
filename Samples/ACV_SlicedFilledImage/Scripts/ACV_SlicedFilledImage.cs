namespace Plugins.mitaywalle.ACV.Runtime
{
	public class ACV_SlicedFilledImage : ACV_Component<SlicedFilledImage>
	{
		public override void SetValue()
		{
			_target.fillAmount = (_animatedValue - _minValue) * 1f / (_maxValue - _minValue);

			base.SetValue();
		}

		public override void DisableTarget()
		{
			_target.gameObject.SetActive(false);
		}
	}
}