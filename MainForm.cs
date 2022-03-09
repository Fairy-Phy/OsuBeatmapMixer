using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsuBeatmapMixer {
	public partial class MainForm : Form {

		internal BindingList<BeatmapQueue> Queues { get; }

		static List<char> InvalidChars => new List<char>(System.IO.Path.GetInvalidFileNameChars().Concat(System.IO.Path.GetInvalidPathChars()));

		readonly MessageData messageData;

		public static CultureInfo GlobalUICulture {
			get { return Thread.CurrentThread.CurrentUICulture; }
			set {
				if (GlobalUICulture != value) {
					Thread.CurrentThread.CurrentUICulture = value;
				}
			}
		}

		/*void SetCulture(string Culture) {
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Culture);

			ComponentResourceManager Resources = new ComponentResourceManager(GetType());

			var Controls = GetControl(this);
			for (int i = 0; i < Controls.Count; i++) {
				if (Controls[i] is DataGridView beatmaps) {
					for (int n = 0; n < beatmaps.Columns.Count; n++) {
						Resources.ApplyResources(beatmaps.Columns[n], beatmaps.Columns[n].Name);
					}
				}
				Resources.ApplyResources(Controls[i], Controls[i].Name);
			}
		}

		List<Control> GetControl(Control control) {
			var Controls = control.Controls.Cast<Control>().ToList();
			for (int i = 0; i < Controls.Count; i++) {
				Controls = Controls.Concat(GetControl(Controls[i])).ToList();
			}
			return Controls;
		}*/

		[System.Diagnostics.Conditional("DEBUG")]
		void GetDebugBuildText() {
			Text += " (Debug Build)";
		}

		public MainForm() {
			InitializeComponent();
			GetDebugBuildText();

			messageData = new MessageData();

			Queues = new BindingList<BeatmapQueue>();

			beatmapQueueBindingSource.DataSource = Queues;

			Beatmaps.ClearSelection();

			//SetCulture("en-US");
		}

		bool CheckDuplicationBeatmap(BeatmapQueue add_beatmap) {
			for (int i = 0; i < Queues.Count; i++) {
				BeatmapQueue queue = Queues[i];
				if (add_beatmap.GetAudioPath() == queue.GetAudioPath() &&
					add_beatmap.Artist == queue.Artist &&
					add_beatmap.Title == queue.Title &&
					add_beatmap.DiffName == queue.DiffName
				) return true;
			}

			return false;
		}

		private void Beatmap_Add_Click(object sender, EventArgs e) {
			if (OpenOsuFileDialog.ShowDialog() == DialogResult.OK) {
				try {
					BeatmapQueue beatmap = new BeatmapQueue(OpenOsuFileDialog.FileName) {
						Order = Queues.Count + 1
					};

					if (CheckDuplicationBeatmap(beatmap)) {
						if (MessageBox.Show(
							messageData.WarningText1,
							messageData.MessageWarning,
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Warning
						) == DialogResult.No) return;
					}

					Queues.Add(beatmap);
					Beatmaps.AutoResizeColumns();
					//Beatmaps.Refresh();
					VisibleChange();
				}
				catch (Exception Error) {
					MessageBox.Show($"Add Error: {Error}");
					#if DEBUG
					throw Error;
					#endif
				}
			}
		}

		private void BeatmapOrderDown_Click(object sender, EventArgs e) {
			int SelectRow = Beatmaps.SelectedRows[0].Index;
			BeatmapQueue SelectQueue = Queues[SelectRow];
			BeatmapQueue UpperQueue = Queues[SelectRow + 1];

			SelectQueue.Order++;
			UpperQueue.Order--;

			Queues[SelectRow + 1] = SelectQueue;
			Queues[SelectRow] = UpperQueue;

			Beatmaps.Rows[SelectRow + 1].Selected = true;
		}

		private void BeatmapOrderUp_Click(object sender, EventArgs e) {
			int SelectRow = Beatmaps.SelectedRows[0].Index;
			BeatmapQueue SelectQueue = Queues[SelectRow];
			BeatmapQueue LowerQueue = Queues[SelectRow - 1];

			SelectQueue.Order--;
			LowerQueue.Order++;

			Queues[SelectRow - 1] = SelectQueue;
			Queues[SelectRow] = LowerQueue;

			Beatmaps.Rows[SelectRow - 1].Selected = true;
		}

		private void BeatmapDeleteButton_Click(object sender, EventArgs e) {
			int SelectRow = Beatmaps.SelectedRows[0].Index;

			if (MessageBox.Show(
				messageData.ConfirmationText1,
				messageData.MessageConfirmation,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning
			) == DialogResult.No) return;

			Queues.RemoveAt(SelectRow);

			for (int i = SelectRow; i < Queues.Count; i++)
				Queues[i].Order--;
		}

		private void Beatmaps_SelectionChanged(object sender, EventArgs e) {
			VisibleChange();
		}

		void VisibleChange() {
			if (Beatmaps.SelectedRows.Count == 0) {
				BeatmapOrderDown.Visible = 
				BeatmapOrderUp.Visible = 
				BeatmapDeleteButton.Visible = false;
			}
			else {
				BeatmapDeleteButton.Visible = true;

				int SelectRow = Beatmaps.SelectedRows[0].Index;
				if (SelectRow > 0) {
					BeatmapOrderUp.Visible = true;
				}
				else {
					BeatmapOrderUp.Visible = false;
				}

				if (SelectRow < Queues.Count - 1) {
					BeatmapOrderDown.Visible = true;
				}
				else {
					BeatmapOrderDown.Visible = false;
				}
			}
		}

		bool CheckMode() {
			Osu.GameMode Mode = Queues[0].Beatmap.Mode;
			for (int i = 1; i < Queues.Count; i ++) {
				if (Queues[i].Beatmap.Mode != Mode)
					return true;
			}

			return false;
		}

		bool CheckKeyIfMania() {
			Osu.GameMode Mode = Queues[0].Beatmap.Mode;
			if (Mode != Osu.GameMode.Mania) return false;

			double CircleSize = Queues[0].Beatmap.CircleSize;
			for (int i = 1; i < Queues.Count; i++) {
				if (Queues[i].Beatmap.CircleSize != CircleSize)
					return true;
			}

			return false;
		}

		bool CheckSliderMultiplier() {
			Osu.GameMode Mode = Queues[0].Beatmap.Mode;
			if (Mode == Osu.GameMode.Mania) return false;

			double SliderMultiplier = Queues[0].Beatmap.SliderMultiplier;
			for (int i = 1; i < Queues.Count; i++) {
				if (Queues[i].Beatmap.SliderMultiplier != SliderMultiplier)
					return true;
			}

			return false;
		}

		bool CheckMetadata() {
			if (
				string.IsNullOrWhiteSpace(ArtistTextBox.Text) ||
				string.IsNullOrWhiteSpace(TitleTextBox.Text)
			) return true;

			return false;
		}

		void AllControlChange(bool Enable) {
			Beatmaps.Enabled =
			BeatmapOrderUp.Enabled =
			BeatmapOrderDown.Enabled =
			BeatmapDeleteButton.Enabled =
			Beatmap_Add.Enabled =
			ArtistTextBox.Enabled =
			CreatorTextBox.Enabled =
			TitleTextBox.Enabled =
			HPNum.Enabled =
			ODNum.Enabled =
			CSNum.Enabled =
			ARNum.Enabled =
			SongDurationNum.Enabled =
			SourceTextBox.Enabled =
			TagsTextBox.Enabled =
			DifficultyTextBox.Enabled =
			SliderTickRateNum.Enabled =
			ExecuteButton.Enabled = Enable;
		}

		void ExecuteFinalize() {
			ProgressBar1.Visible = false;
			ProgressBar1.Value = 0;

			ChangeLanguageButton.Visible = true;

			AllControlChange(true);
		}

		bool AllCheck() {
			string ErrorMessage = null;
			if (Queues.Count < 2) ErrorMessage = messageData.ErrorText1; 
			else if (CheckMode()) ErrorMessage = messageData.ErrorText2;
			else if (CheckKeyIfMania()) ErrorMessage = messageData.ErrorText3;
			else if (CheckSliderMultiplier()) ErrorMessage = messageData.ErrorText4;
			else if (CheckMetadata()) ErrorMessage = messageData.ErrorText5;
			else if (SaveOszFileDialog.ShowDialog() != DialogResult.OK) ErrorMessage = messageData.ErrorText6;

			if (!(ErrorMessage is null)) {
				MessageBox.Show(ErrorMessage, messageData.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return true;
			}
			return false;
		}

		private void ExecuteButton_Click(object sender, EventArgs e) {
			AllControlChange(false);

			ChangeLanguageButton.Visible = false;
			ProgressBar1.Visible = true;

			if (AllCheck()) {
				ExecuteFinalize();
				return;
			}

			string ExportDirPath = System.IO.Path.GetDirectoryName(SaveOszFileDialog.FileName);
			if (MessageBox.Show(string.Format(messageData.ConfirmationText2 + "\n{0}", ExportDirPath), messageData.MessageConfirmation, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK) {
				MessageBox.Show(messageData.ErrorText7, messageData.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Error);

				ExecuteFinalize();
				return;
			}

			string ExportAudioName = System.IO.Path.GetFileName(SaveOszFileDialog.FileName);

			string ExportOsuName = $"{ArtistTextBox.Text} - {TitleTextBox.Text} ({CreatorTextBox.Text}) [{DifficultyTextBox.Text}].osu";
			for (int i = 0; i < InvalidChars.Count; i++)
				ExportOsuName = ExportOsuName.Replace(InvalidChars[i].ToString(CultureInfo.InvariantCulture), "");
			string ExportOsuPath = System.IO.Path.Combine(ExportDirPath, ExportOsuName);
			if (System.IO.File.Exists(ExportOsuPath)) {
				if (MessageBox.Show(string.Format(messageData.WarningText2, ExportOsuPath), messageData.MessageWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) {
					MessageBox.Show(messageData.ErrorText7, messageData.MessageError, MessageBoxButtons.OK, MessageBoxIcon.Error);

					ExecuteFinalize();
					return;
				}
			}

			MixData mixData = new MixData(Queues, (int) SongDurationNum.Value * 1000, SaveOszFileDialog.FileName, ExportOsuPath);

			mixData.MixedBeatmap.Artist = ArtistTextBox.Text;
			mixData.MixedBeatmap.Title = TitleTextBox.Text;
			mixData.MixedBeatmap.Creater = CreatorTextBox.Text;
			mixData.MixedBeatmap.Difficulty = DifficultyTextBox.Text;
			mixData.MixedBeatmap.AudioFilename = ExportAudioName;
			mixData.MixedBeatmap.CircleSize = Queues[0].Beatmap.Mode == Osu.GameMode.Mania ? Queues[0].Beatmap.CircleSize : (double) CSNum.Value;
			mixData.MixedBeatmap.ApproachRate = Queues[0].Beatmap.Mode == Osu.GameMode.Mania ? 5 : (double) ARNum.Value;
			mixData.MixedBeatmap.HPDrainRate = (double) HPNum.Value;
			mixData.MixedBeatmap.Mode = Queues[0].Beatmap.Mode;
			mixData.MixedBeatmap.OverallDifficulty = (double) ODNum.Value;
			mixData.MixedBeatmap.SliderMultiplier = Queues[0].Beatmap.SliderMultiplier;
			mixData.MixedBeatmap.SliderTickRate = (double) SliderTickRateNum.Value;
			mixData.MixedBeatmap.Source = SourceTextBox.Text;
			mixData.MixedBeatmap.Tags = TagsTextBox.Text;

			//MessageBox.Show(mixData.MixedBeatmap.AudioFilename);

			BackgroundWorker1.RunWorkerAsync(mixData);
		}

		private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = (BackgroundWorker) sender;

			MixData mixData = (MixData) e.Argument;

			mixData.Mix(worker.ReportProgress);
		}

		private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			ProgressBar1.Value = e.ProgressPercentage;
		}

		private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			if (e.Error is null) {
				MessageBox.Show(messageData.FinishedText1, messageData.MessageFinished, MessageBoxButtons.OK, MessageBoxIcon.Information);

				ExecuteFinalize();
			}
			else {
				MessageBox.Show($"Execute Error: {e.Error}");
				#if DEBUG
				throw e.Error;
				#endif
			}
		}

		private void ChangeLanguageButton_Click(object sender, EventArgs e) {
			System.Diagnostics.Process.Start(Application.ExecutablePath, messageData.ChangeLang);

			Application.Exit();
		}
	}
}
