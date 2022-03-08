using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer.Osu {

	class Mixer {

		internal List<Beatmap> Beatmaps { get; }

		internal int Duration { get; }

		internal int EndBPMChange => ChangeSongTimingPointTime;

		internal int StartBPMChange => Duration - ChangeSongTimingPointTime;

		const int ChangeSongTimingPointTime = 1000;

		const int OnceProcessPersent = 25;

		internal Mixer(IEnumerable<BeatmapQueue> beatmapQueues, int duration) {
			Duration = duration;
			Beatmaps = new List<Beatmap>();
			foreach (var beatmapQueue in beatmapQueues) {
				Beatmaps.Add(beatmapQueue.Beatmap);
			}
		}

		internal Beatmap MixBeatmap(ref Beatmap Res, Action<int> ReportProgress) {
			//double AverageBeatLength = GetMixedAverageBPM();
			//System.Windows.Forms.MessageBox.Show($"AvgBPM: {60000 / AverageBeatLength}");

			MixTimingPoints(ref Res, ReportProgress);

			MixHitObjects(ref Res, ReportProgress);

			return Res;
		}

		void MixTimingPoints(ref Beatmap Res, Action<int> ReportProgress) {
			double MixedAverageBPM = GetMixedAverageBPM();
			#if DEBUG
			Console.WriteLine($"MixedAvgBPM: {MixedAverageBPM}");
			#endif

			int AppendOffset = 0;
			for (int i = 0; i < Beatmaps.Count; i++) {
				Beatmap beatmap = Beatmaps[i];

				double AverageBPM = GetAverageBPM(beatmap).Key;
				double BPMScale = MixedAverageBPM / AverageBPM;
				#if DEBUG
				Console.WriteLine($"AvgBPM: {AverageBPM}\nBPMScale: {BPMScale}x");
				#endif

				if (i != 0) {
					int StartOffset = beatmap.HitObjects[0].StartOffset;

					TimingPoint FirstTimingPoint = (
						from t in beatmap.TimingPoints
						where t.Uninherited == 1
						where t.Offset <= StartOffset
						select t
					).LastOrDefault();

					if (FirstTimingPoint is null) FirstTimingPoint = (
						from t in beatmap.TimingPoints
						where t.Uninherited == 1
						select t
					).First();

					TimingPoint[] UnderZeroTimingPoint = (
						from t in beatmap.TimingPoints
						where t.Uninherited == 1
						where t.Offset < 0
						select t
					).ToArray();

					if (UnderZeroTimingPoint.Length > 0)
						FirstTimingPoint = UnderZeroTimingPoint.Last();

					TimingPoint FirstTimingPointInherited = (
						from t in beatmap.TimingPoints
						where t.Uninherited == 0
						where t.Offset == FirstTimingPoint.Offset
						select t
					).FirstOrDefault();

					Res.TimingPoints.Add(FirstTimingPoint.Clone(AppendOffset - FirstTimingPoint.Offset));
					if (beatmap.Mode == GameMode.Mania) {
						if (FirstTimingPointInherited is null) {
							Res.TimingPoints.Add(new TimingPoint(
								FirstTimingPoint.Offset + AppendOffset - FirstTimingPoint.Offset,
								100 / BPMScale,
								FirstTimingPoint.Meter,
								FirstTimingPoint.SampleSet,
								FirstTimingPoint.SampleIndex,
								FirstTimingPoint.Volume,
								0,
								FirstTimingPoint.Effects
							));
						}
						else Res.TimingPoints.Add(FirstTimingPointInherited.Clone(AppendOffset - FirstTimingPointInherited.Offset, 100 / ((100 / FirstTimingPointInherited.BeatLength) * BPMScale)));
					}
					else {
						if (!(FirstTimingPointInherited is null))
							Res.TimingPoints.Add(FirstTimingPointInherited.Clone(AppendOffset - FirstTimingPointInherited.Offset));
					}

					AppendOffset += StartBPMChange;

					TimingPoint[] TimingPoints = (
						from t in beatmap.TimingPoints
						where t.Offset > StartOffset - StartBPMChange
						select t
					).ToArray();

					if (TimingPoints.Length == 0 ) {
						Res.TimingPoints.Add(new TimingPoint(
							StartOffset,
							FirstTimingPoint.BeatLength,
							FirstTimingPoint.Meter,
							FirstTimingPoint.SampleSet,
							FirstTimingPoint.SampleIndex,
							FirstTimingPoint.Volume,
							FirstTimingPoint.Uninherited,
							FirstTimingPoint.Effects
						));
						if (!(FirstTimingPointInherited is null))
							Res.TimingPoints.Add(new TimingPoint(
								StartOffset,
								FirstTimingPointInherited.BeatLength,
								FirstTimingPointInherited.Meter,
								FirstTimingPointInherited.SampleSet,
								FirstTimingPointInherited.SampleIndex,
								FirstTimingPointInherited.Volume,
								FirstTimingPointInherited.Uninherited,
								FirstTimingPointInherited.Effects
							));
					}
					if (TimingPoints.Length != 0) {
						AppendOffset -= StartOffset;
						for (int n = 0; n < TimingPoints.Length; n++) {
							TimingPoint timingPoint = TimingPoints[n];

							if (beatmap.Mode == GameMode.Mania)
								ManiaNeutralizeTimingPoint(ref Res, beatmap, timingPoint, AppendOffset, BPMScale);
							else Res.TimingPoints.Add(timingPoint.Clone(AppendOffset));
						}
					}
				}
				else {
					for (int n = 0; n < beatmap.TimingPoints.Count; n++) {
						TimingPoint timingPoint = beatmap.TimingPoints[n];

						if (beatmap.Mode == GameMode.Mania)
							ManiaNeutralizeTimingPoint(ref Res, beatmap, timingPoint, AppendOffset, BPMScale);
						else Res.TimingPoints.Add(timingPoint.Clone(AppendOffset));
					}
				}
				AppendOffset += GetLastNotesOffset(beatmap) + EndBPMChange;

				ReportProgress((int) (((double) (i + 1) / Beatmaps.Count) * OnceProcessPersent));
			}
		}

		void ManiaNeutralizeTimingPoint(ref Beatmap Res, Beatmap beatmap, TimingPoint timingPoint, int AppendOffset, double BPMScale) {
			double BPMScaleBeatLength = 100 / BPMScale;

			if (timingPoint.Uninherited == 1) {
				Res.TimingPoints.Add(timingPoint.Clone(AppendOffset));

				int OffsetInheritedCount = (
					from t in beatmap.TimingPoints
					where t.Uninherited == 0
					where t.Offset == timingPoint.Offset
					select t
				).Count();
				if (OffsetInheritedCount > 0) return;

				Res.TimingPoints.Add(new TimingPoint(
					timingPoint.Offset + AppendOffset,
					BPMScaleBeatLength,
					timingPoint.Meter,
					timingPoint.SampleSet,
					timingPoint.SampleIndex,
					timingPoint.Volume,
					0,
					timingPoint.Effects
				));
			}
			else {
				double BaseScale = 100 / timingPoint.BeatLength;
				double AfterScale = BaseScale * BPMScale;

				Res.TimingPoints.Add(timingPoint.Clone(AppendOffset, 100 / AfterScale));
			}
		}

		void MixHitObjects(ref Beatmap Res, Action<int> ReportProgress) {
			int AppendOffset = 0;
			for (int i = 0; i < Beatmaps.Count; i++) {
				Beatmap beatmap = Beatmaps[i];

				int SubOffset = 0;
				if (i != 0) SubOffset = beatmap.HitObjects[0].StartOffset;
				for (int n = 0; n < beatmap.HitObjects.Count; n++) {
					HitObject hitObject = beatmap.HitObjects[n];

					Res.HitObjects.Add(hitObject.Clone(AppendOffset - SubOffset));
				}

				AppendOffset += GetLastNotesOffset(beatmap) - SubOffset + Duration;
				ReportProgress((int) (((double) (i + 1) / Beatmaps.Count) * OnceProcessPersent) + OnceProcessPersent);
			}
		}

		void DictionaryAddAppend(ref Dictionary<double, int> dic, double key, int value) {
			double AddKey = double.Parse(key.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture);
			if (!dic.ContainsKey(AddKey))
				dic.Add(AddKey, value);
			else dic[AddKey] += value;
		}

		KeyValuePair<double, int> GetAverageBPM(Beatmap beatmap, bool IsNotFirst = false, bool IsNotLast = false) {
			// BPM : Time
			Dictionary<double, int> BPMOffsetDuration = new Dictionary<double, int>();

			TimingPoint[] NotInheritedTimingPoints = (
				from t in beatmap.TimingPoints
				where t.Uninherited == 1
				select t
			).ToArray();

			if (NotInheritedTimingPoints.Length == 0)
				throw new ArgumentOutOfRangeException("Cannot be gotten without TimingPoint.");
			else if (NotInheritedTimingPoints.Length == 1) {
				DictionaryAddAppend(ref BPMOffsetDuration,
					60000 / NotInheritedTimingPoints[0].BeatLength,
					Math.Abs(Math.Min(0, NotInheritedTimingPoints[0].Offset)) + GetLastNotesOffset(beatmap) + (IsNotLast ? EndBPMChange : 0) - 1
				);
			}
			else {
				for (int n = 0; n < NotInheritedTimingPoints.Length; n++) {
					TimingPoint timingPoint = NotInheritedTimingPoints[n];

					int Time;
					if (n == 0) {
						int DurationOffset = 0;
						if (IsNotFirst) DurationOffset = StartBPMChange;

						TimingPoint NextTimingPoint = NotInheritedTimingPoints[n + 1];
						Time = Math.Abs(Math.Min(0, timingPoint.Offset)) + NextTimingPoint.Offset + DurationOffset;
					}
					else if (n == NotInheritedTimingPoints.Length - 1) {
						Time = GetLastNotesOffset(beatmap) - timingPoint.Offset + (IsNotLast ? EndBPMChange : 0);
					}
					else {
						TimingPoint NextTimingPoint = NotInheritedTimingPoints[n + 1];
						Time = NextTimingPoint.Offset - timingPoint.Offset;
					}
					DictionaryAddAppend(ref BPMOffsetDuration,
						60000 / timingPoint.BeatLength,
						Time
					);
				}
			}

			KeyValuePair<double, int> MaxBPMOffsetDuration = new KeyValuePair<double, int>(0, 0);
			foreach (var KeyValue in BPMOffsetDuration) {
				#if DEBUG
				Console.WriteLine($"Key: {KeyValue.Key}, Value: {KeyValue.Value}");
				#endif

				if (KeyValue.Value > MaxBPMOffsetDuration.Value)
					MaxBPMOffsetDuration = KeyValue;
			}

			return MaxBPMOffsetDuration;
		}

		double GetMixedAverageBPM() {
			// BPM : Time
			Dictionary<double, int> BPMOffsetDuration = new Dictionary<double, int>();

			for (int i = 0; i < Beatmaps.Count; i++) {
				Beatmap beatmap = Beatmaps[i];

				TimingPoint[] NotInheritedTimingPoints = (
					from t in beatmap.TimingPoints
					where t.Uninherited == 1
					select t
				).ToArray();

				if (NotInheritedTimingPoints.Length == 0)
					throw new ArgumentOutOfRangeException("Cannot be gotten without TimingPoint.");
				else if (NotInheritedTimingPoints.Length == 1) {
					DictionaryAddAppend(ref BPMOffsetDuration,
						60000 / NotInheritedTimingPoints[0].BeatLength,
						Math.Abs(Math.Min(0, NotInheritedTimingPoints[0].Offset)) + GetLastNotesOffset(beatmap) + (i != Beatmaps.Count - 1 ? EndBPMChange : 0) - 1
					);
				}
				else {
					for (int n = 0; n < NotInheritedTimingPoints.Length; n++) {
						TimingPoint timingPoint = NotInheritedTimingPoints[n];

						int Time;
						if (n == 0) {
							int DurationOffset = 0;
							if (i > 0) DurationOffset = StartBPMChange;

							TimingPoint NextTimingPoint = NotInheritedTimingPoints[n + 1];
							Time = Math.Abs(Math.Min(0, timingPoint.Offset)) + NextTimingPoint.Offset + DurationOffset;
						}
						else if (n == NotInheritedTimingPoints.Length - 1) {
							Time = GetLastNotesOffset(beatmap) - timingPoint.Offset + (i != Beatmaps.Count - 1 ? EndBPMChange : 0);
						}
						else {
							TimingPoint NextTimingPoint = NotInheritedTimingPoints[n + 1];
							Time = NextTimingPoint.Offset - timingPoint.Offset;
						}
						DictionaryAddAppend(ref BPMOffsetDuration,
							60000 / timingPoint.BeatLength,
							Time
						);
					}
				}
			}

			KeyValuePair<double, int> MaxBPMOffsetDuration = new KeyValuePair<double, int>(0, 0);
			foreach (var KeyValue in BPMOffsetDuration) {
				#if DEBUG
				Console.WriteLine($"Key: {KeyValue.Key}, Value: {KeyValue.Value}");
				#endif

				if (KeyValue.Value > MaxBPMOffsetDuration.Value)
					MaxBPMOffsetDuration = KeyValue;
			}

			return MaxBPMOffsetDuration.Key;
		}

		internal static int GetLastNotesOffset(Beatmap beatmap) {
			var LastHitobjects = (
				from h in beatmap.HitObjects
				where h.StartOffset == beatmap.HitObjects[beatmap.HitObjects.Count - 1].StartOffset ||
					(h.Object != HitObject.ObjectType.Normal && h.Object != HitObject.ObjectType.ShortNormal ?
						h.EndOffset >= beatmap.HitObjects[beatmap.HitObjects.Count - 1].StartOffset : false
					)
				select h
			);

			if (LastHitobjects.Count() == 0)
				throw new ArgumentNullException("Not found Last offset");

			var LNObjects = (
					from h in LastHitobjects
					where h.Object != HitObject.ObjectType.Normal && h.Object != HitObject.ObjectType.ShortNormal
					select h
			);
			if (LNObjects.Count() > 1) {
				int MaxOffset = 0;
				foreach (var hitobject in LNObjects) {
					if (hitobject.EndOffset > MaxOffset)
						MaxOffset = hitobject.EndOffset;
				}

				return MaxOffset;
			}
			else if (LNObjects.Count() == 1) {
				return LNObjects.First().EndOffset;
			}

			return LastHitobjects.First().StartOffset;
		}
	}
}
