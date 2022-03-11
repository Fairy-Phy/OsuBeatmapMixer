using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer.Osu {

	class Beatmap {

		internal string AudioFilename { get; set; }

		internal GameMode Mode { get; set; }

		internal string Title { get; set; }

		internal string Artist { get; set; }

		internal string Creater { get; set; }

		// Version
		internal string Difficulty { get; set; }

		internal string Source { get; set; }

		internal string Tags { get; set; }

		internal double HPDrainRate { get; set; }

		// Mania Only(key check)
		internal double CircleSize { get; set; }

		internal double OverallDifficulty { get; set; }

		internal double ApproachRate { get; set; }

		internal double SliderMultiplier { get; set; } = 1.4;

		internal double SliderTickRate { get; set; } = 1;

		internal List<TimingPoint> TimingPoints { get;}

		internal List<HitObject> HitObjects { get; }

		internal int Offset { get; set; } = 0;

		internal Beatmap() {
			TimingPoints = new List<TimingPoint>();
			HitObjects = new List<HitObject>();
		}

		internal void SaveFile(string ExportPath) {
			using (StreamWriter ExportStreamWriter = new StreamWriter(ExportPath, false, Encoding.UTF8)) {
				ExportStreamWriter.WriteLine($"osu file format v14");
				ExportStreamWriter.WriteLine();
				ExportStreamWriter.WriteLine($"[General]");
				ExportStreamWriter.WriteLine($"AudioFilename: {AudioFilename}");
				ExportStreamWriter.WriteLine($"AudioLeadIn: 0");
				ExportStreamWriter.WriteLine($"PreviewTime: -1");
				ExportStreamWriter.WriteLine($"Countdown: 0");
				ExportStreamWriter.WriteLine($"SampleSet: Normal");
				ExportStreamWriter.WriteLine($"StackLeniency: 0.7");
				ExportStreamWriter.WriteLine($"Mode: {(int) Mode}");
				ExportStreamWriter.WriteLine($"LetterboxInBreaks: 0");
				ExportStreamWriter.WriteLine($"WidescreenStoryboard: 0");
				ExportStreamWriter.WriteLine();
				ExportStreamWriter.WriteLine($"[Editor]");
				ExportStreamWriter.WriteLine($"DistanceSpacing: 0.6");
				ExportStreamWriter.WriteLine($"BeatDivisor: 4");
				ExportStreamWriter.WriteLine($"GridSize: 16");
				ExportStreamWriter.WriteLine($"TimelineZoom: 1");
				ExportStreamWriter.WriteLine();
				ExportStreamWriter.WriteLine($"[Metadata]");
				ExportStreamWriter.WriteLine($"Title:{Title}");
				ExportStreamWriter.WriteLine($"TitleUnicode:{Title}");
				ExportStreamWriter.WriteLine($"Artist:{Artist}");
				ExportStreamWriter.WriteLine($"ArtistUnicode:{Artist}");
				ExportStreamWriter.WriteLine($"Creator:{Creater}");
				ExportStreamWriter.WriteLine($"Version:{Difficulty}");
				ExportStreamWriter.WriteLine($"Source:{Source}");
				ExportStreamWriter.WriteLine($"Tags:{Tags}");
				ExportStreamWriter.WriteLine($"BeatmapID: 0");
				ExportStreamWriter.WriteLine($"BeatmapSetID: -1");
				ExportStreamWriter.WriteLine();
				ExportStreamWriter.WriteLine($"[Difficulty]");
				ExportStreamWriter.WriteLine($"HPDrainRate: {HPDrainRate}");
				ExportStreamWriter.WriteLine($"CircleSize: {CircleSize}");
				ExportStreamWriter.WriteLine($"OverallDifficulty: {OverallDifficulty}");
				ExportStreamWriter.WriteLine($"ApproachRate: {ApproachRate}");
				ExportStreamWriter.WriteLine($"SliderMultiplier: {SliderMultiplier}");
				ExportStreamWriter.WriteLine($"SliderTickRate: {SliderTickRate}");
				ExportStreamWriter.WriteLine();
				ExportStreamWriter.WriteLine($"[Events]");
				ExportStreamWriter.WriteLine($"//Background and Video events");
				ExportStreamWriter.WriteLine($"//Break Periods");
				ExportStreamWriter.WriteLine($"//Storyboard Layer 0 (Background)");
				ExportStreamWriter.WriteLine($"//Storyboard Layer 1 (Fail)");
				ExportStreamWriter.WriteLine($"//Storyboard Layer 2 (Pass)");
				ExportStreamWriter.WriteLine($"//Storyboard Layer 3 (Foreground)");
				ExportStreamWriter.WriteLine($"//Storyboard Layer 4 (Overlay)");
				ExportStreamWriter.WriteLine($"//Storyboard Sound Samples");

				ExportStreamWriter.WriteLine();
				ExportStreamWriter.WriteLine($"[TimingPoints]");
				for (int i = 0; i < TimingPoints.Count; i++)
					ExportStreamWriter.WriteLine(TimingPoints[i].ToString());

				ExportStreamWriter.WriteLine();
				ExportStreamWriter.WriteLine($"[HitObjects]");
				for (int i = 0; i < HitObjects.Count; i++)
					ExportStreamWriter.WriteLine(HitObjects[i].ToString());
			}
		}
	}
}
