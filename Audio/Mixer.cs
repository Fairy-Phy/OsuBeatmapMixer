using NAudio.Lame;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer.Audio {

	class Mixer {


		internal string[] Songs { get; }

		internal int[] FirstNotesOffsets { get; }

		internal int[] LastNotesOffsets { get; }

		internal int Duration { get; }

		internal int EndTime => ChangeSongTime;

		internal int NextStartTime => Duration - EndTime - StartTime;

		internal int StartTime => ChangeSongTime;

		const int ChangeSongTime = 1000;

		const int OnceProcessPersent = 10;

		const int PrevProcessPersent = 50;

		internal Mixer(IEnumerable<BeatmapQueue> BeatmapQueues, int duration) {
			int Length = BeatmapQueues.Count();

			Songs = new string[Length];
			FirstNotesOffsets = new int[Length];
			LastNotesOffsets = new int[Length];
			Duration = duration;

			int i = 0;
			foreach (var beatmapQueue in BeatmapQueues) {
				Songs[i] = beatmapQueue.GetAudioPath();
				FirstNotesOffsets[i] = beatmapQueue.Beatmap.HitObjects[0].StartOffset;
				LastNotesOffsets[i++] = Osu.Mixer.GetLastNotesOffset(beatmapQueue.Beatmap);
			}
		}

		internal void Mix(string ExportPath, Action<int> ReportProgress) {
			List<IDisposable> OpennedDisposable = new List<IDisposable>();
			List<ISampleProvider> Source = new List<ISampleProvider>();
			List<int> AudioTimes = new List<int>();
			for (int i = 0; i < Songs.Length; i++) {
				string SongPath = Songs[i];
				Mp3FileReader Song = new Mp3FileReader(SongPath);
				AudioTimes.Add((int) Song.TotalTime.TotalMilliseconds);
				Source.Add(Song.ToSampleProvider());
				OpennedDisposable.Add(Song);
				ReportProgress((int) (((double) (i + 1) / Songs.Length) * OnceProcessPersent) + PrevProcessPersent);
			}

			int MaxSampleRate = 0;
			int MaxBitsPerSample = 0;
			int MaxChannels = 0;
			for (int i = 0; i < Source.Count; i++) {
				/*System.Windows.Forms.MessageBox.Show(
					string.Format("samplerate: {0}\nBitPerSample:{1}\nChannels:{2}\nAverageBytesPerSecounts:{3}\nBlockAlign:{4}",
						Source[i].WaveFormat.SampleRate,
						Source[i].WaveFormat.BitsPerSample,
						Source[i].WaveFormat.Channels,
						Source[i].WaveFormat.AverageBytesPerSecond,
						Source[i].WaveFormat.BlockAlign
					)
				);*/

				if (Source[i].WaveFormat.SampleRate > MaxSampleRate)
					MaxSampleRate = Source[i].WaveFormat.SampleRate;
				if (Source[i].WaveFormat.BitsPerSample > MaxBitsPerSample)
					MaxBitsPerSample = Source[i].WaveFormat.BitsPerSample;
				if (Source[i].WaveFormat.Channels > MaxChannels)
					MaxChannels = Source[i].WaveFormat.Channels;
				ReportProgress((int) (((double) (i + 1) / Source.Count) * OnceProcessPersent) + (OnceProcessPersent * 1) + PrevProcessPersent);
			}

			WaveFormat ConvertFormat = new WaveFormat(MaxSampleRate, MaxBitsPerSample, MaxChannels);
			for (int i = 0; i < Source.Count; i++) {
				if (
					Source[i].WaveFormat.SampleRate != MaxSampleRate ||
					Source[i].WaveFormat.BitsPerSample != MaxBitsPerSample ||
					Source[i].WaveFormat.Channels != MaxChannels
				) {
					MediaFoundationResampler Resample =
						new MediaFoundationResampler(Source[i].ToWaveProvider(), ConvertFormat);
					Source[i] = Resample.ToSampleProvider();
					OpennedDisposable.Add(Resample);
				}

				ReportProgress((int) (((double) (i + 1) / Source.Count) * OnceProcessPersent) + (OnceProcessPersent * 2) + PrevProcessPersent);
			}

			DelayFadeOutSampleProvider FadeSource = new DelayFadeOutSampleProvider(Source[0]);
			//Console.WriteLine(LastNotesOffsets[0]);
			FadeSource.BeginFadeOut(LastNotesOffsets[0], EndTime); // Duration Fade
			ISampleProvider ResSource = FadeSource.Take(TimeSpan.FromMilliseconds(LastNotesOffsets[0] + EndTime));
			int AddSilenceTime = Math.Max(0, LastNotesOffsets[0] + EndTime - AudioTimes[0]);
			for (int i = 1; i < Source.Count; i++) {
				//Console.WriteLine($"{FirstNotesOffsets[i]} : {LastNotesOffsets[i]}");
				int FadeFirstTime = FirstNotesOffsets[i] - StartTime;
				if (FadeFirstTime < 0) {
					AddSilenceTime += Math.Abs(FadeFirstTime);
					FadeFirstTime = 0;
				}
				FadeSource = new DelayFadeOutSampleProvider(Source[i].Skip(TimeSpan.FromMilliseconds(FadeFirstTime)), true);
				FadeSource.BeginFadeIn(StartTime);
				if (i != Source.Count - 1) {
					int FadeEndTime = LastNotesOffsets[i] - FadeFirstTime;
					FadeSource = new DelayFadeOutSampleProvider(FadeSource.Take(TimeSpan.FromMilliseconds(FadeEndTime + EndTime)));
					FadeSource.BeginFadeOut(FadeEndTime, EndTime);
				}
				ResSource = ResSource.FollowedBy(TimeSpan.FromMilliseconds(NextStartTime + AddSilenceTime), FadeSource);
				AddSilenceTime = Math.Max(0, LastNotesOffsets[i] + EndTime - AudioTimes[i]);
				ReportProgress((int) (((double) (i + 1) / Source.Count) * OnceProcessPersent) + (OnceProcessPersent * 3) + PrevProcessPersent);
			}

			var waveProvider = ResSource.ToWaveProvider();
			//new WaveToSampleProvider().

			//var mixingSampleProvider = new MixingSampleProvider(Source);
			//var waveProvider = mixingSampleProvider.ToWaveProvider();

			using (LameMP3FileWriter Writer = new LameMP3FileWriter(ExportPath, waveProvider.WaveFormat, 192)) {
				byte[] Buffer = new byte[waveProvider.WaveFormat.AverageBytesPerSecond];
				while (true) {
					int ReadCount = waveProvider.Read(Buffer, 0, Buffer.Length);
					if (ReadCount == 0) break;
					Writer.Write(Buffer, 0, ReadCount);
				}
			}

			foreach (var Disposable in OpennedDisposable) {
				Disposable.Dispose();
			}

			ReportProgress((OnceProcessPersent * 5) + PrevProcessPersent);
		}
	}
}
