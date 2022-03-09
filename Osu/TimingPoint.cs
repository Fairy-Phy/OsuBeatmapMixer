using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer.Osu {

	class TimingPoint {

		internal int Offset { get; }

		internal double BeatLength { get; }

		internal int Meter { get; }

		internal int SampleSet { get; }

		internal int SampleIndex { get; }

		internal int Volume { get; }

		internal int Uninherited { get; }

		internal int Effects { get; }

		internal TimingPoint(
			int offset,
			double beatLength,
			int meter,
			int sampleSet,
			int sampleIndex,
			int volume,
			int uninherited,
			int effects
		) {
			Offset = offset;
			BeatLength = beatLength;
			Meter = meter;
			SampleSet = sampleSet;
			SampleIndex = sampleIndex;
			Volume = volume;
			Uninherited = uninherited;
			Effects = effects;
		}

		internal TimingPoint Clone(int AppendOffset = 0, double? ChangeBeatLength = null) {
			int offset = Offset + AppendOffset;
			double beatLength = ChangeBeatLength is double changeBeatLength ? changeBeatLength : BeatLength;

			return new TimingPoint(offset, beatLength, Meter, SampleSet, SampleIndex, Volume, Uninherited, Effects);
		}

		public override string ToString() {
			return FormattableString.Invariant($"{Offset},{(Uninherited == 0 ? -BeatLength : BeatLength)},{Meter},{SampleSet},{SampleIndex},{Volume},{Uninherited},{Effects}");
		}
	}
}
