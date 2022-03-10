using OsuBeatmapMixer.Osu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer {

	public class BeatmapQueue {

		public int Order { get; set; }

		public int Offset { get; set; }

		public string Artist => Beatmap.Artist;

		public string Title => Beatmap.Title;

		public string Creator => Beatmap.Creater;

		public string DiffName => Beatmap.Difficulty;

		private string DirPath { get; }

		internal Beatmap Beatmap { get; }

		public BeatmapQueue(string Path) {
			Beatmap = Parser.ParseBeatmap(Path);
			Beatmap.Offset = Offset;
			DirPath = System.IO.Path.GetDirectoryName(Path);
		}

		internal string GetAudioPath() =>
			$@"{DirPath}\{Beatmap.AudioFilename}";
	}
}
