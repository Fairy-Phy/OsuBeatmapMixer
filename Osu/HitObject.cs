using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer.Osu {

	class HitObject {

		internal enum ObjectType {
			ShortNormal,
			Normal,
			ShortSlider,
			Slider,
			ShortSpinner,
			Spinner,
			ManiaLN
		}

		internal ObjectType Object { get; }

		internal int X { get; }

		internal int Y { get; }

		internal int StartOffset { get; }

		internal int Type { get; }

		internal int HitSound { get; }

		internal int EndOffset { get; }

		internal string SliderSetting { get; }

		internal string HitSample { get; }

		internal HitObject(int x, int y, int offset, int type, int hitSound, string hitSample) {
			Object = string.IsNullOrWhiteSpace(hitSample) ? ObjectType.ShortNormal : ObjectType.Normal;
			X = x;
			Y = y;
			StartOffset = offset;
			Type = type;
			HitSound = hitSound;
			if (Object == ObjectType.Normal) HitSample = hitSample;
		}

		internal HitObject(ObjectType objectType, int x, int y, int offset, int type, int hitSound, int endOffset, string sliderSetting, string hitSample) {
			Object = objectType;
			X = x;
			Y = y;
			StartOffset = offset;
			Type = type;
			HitSound = hitSound;

			if (objectType != ObjectType.ShortSlider)
				HitSample = hitSample;

			if (objectType == ObjectType.Normal) return;
			else EndOffset = endOffset;

			if (objectType == ObjectType.Slider || objectType == ObjectType.ShortSlider)
				SliderSetting = sliderSetting;
		}

		internal HitObject Clone(int AppendOffset = 0) {
			#if DEBUG
			if (AppendOffset != 0) Console.WriteLine($"Changed Offset: {StartOffset + AppendOffset}");
			#endif
			return new HitObject(
				Object, X, Y,
				StartOffset + AppendOffset,
				Type, HitSound,
				EndOffset + AppendOffset,
				SliderSetting, HitSample
			);
		}

		public override string ToString() {
			switch (Object) {
				case ObjectType.ShortNormal:
					return FormattableString.Invariant($"{X},{Y},{StartOffset},{Type},{HitSound}");
				case ObjectType.Normal:
					return FormattableString.Invariant($"{X},{Y},{StartOffset},{Type},{HitSound},{HitSample}");
				case ObjectType.Slider:
					return FormattableString.Invariant($"{X},{Y},{StartOffset},{Type},{HitSound},{SliderSetting},{HitSample}");
				case ObjectType.ShortSlider:
					return FormattableString.Invariant($"{X},{Y},{StartOffset},{Type},{HitSound},{SliderSetting}");
				case ObjectType.ShortSpinner:
					return FormattableString.Invariant($"{X},{Y},{StartOffset},{Type},{HitSound},{EndOffset}");
				case ObjectType.Spinner:
					return FormattableString.Invariant($"{X},{Y},{StartOffset},{Type},{HitSound},{EndOffset},{HitSample}");
				case ObjectType.ManiaLN:
					return FormattableString.Invariant($"{X},{Y},{(StartOffset <= -1000000000 ? "NaN" : StartOffset.ToString(System.Globalization.CultureInfo.InvariantCulture))},{Type},{HitSound},{EndOffset}:{HitSample}");
				default:
					throw new ArgumentException("Unknown ObjectType");
			}
		}
	}
}
