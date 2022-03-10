
namespace OsuBeatmapMixer {
	partial class MainForm {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.Beatmaps = new System.Windows.Forms.DataGridView();
			this.orderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.offsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.artistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Creator = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.diffNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.beatmapQueueBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.Beatmaps_Label = new System.Windows.Forms.Label();
			this.Beatmap_Add = new System.Windows.Forms.Button();
			this.OpenOsuFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.BeatmapOrderUp = new System.Windows.Forms.Button();
			this.BeatmapOrderDown = new System.Windows.Forms.Button();
			this.BeatmapDeleteButton = new System.Windows.Forms.Button();
			this.TiTleLabel = new System.Windows.Forms.Label();
			this.TitleTextBox = new System.Windows.Forms.TextBox();
			this.ArtistLabel = new System.Windows.Forms.Label();
			this.ArtistTextBox = new System.Windows.Forms.TextBox();
			this.HPLabel = new System.Windows.Forms.Label();
			this.ODLabel = new System.Windows.Forms.Label();
			this.ARLabel = new System.Windows.Forms.Label();
			this.SourceTextBox = new System.Windows.Forms.TextBox();
			this.SourceLabel = new System.Windows.Forms.Label();
			this.TagsLabel = new System.Windows.Forms.Label();
			this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
			this.ExecuteButton = new System.Windows.Forms.Button();
			this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.CSLabel = new System.Windows.Forms.Label();
			this.SongDurationNum = new System.Windows.Forms.NumericUpDown();
			this.SongDurationLabel = new System.Windows.Forms.Label();
			this.HPNum = new System.Windows.Forms.NumericUpDown();
			this.ODNum = new System.Windows.Forms.NumericUpDown();
			this.ARNum = new System.Windows.Forms.NumericUpDown();
			this.CSNum = new System.Windows.Forms.NumericUpDown();
			this.CreatorTextBox = new System.Windows.Forms.TextBox();
			this.CreatorLabel = new System.Windows.Forms.Label();
			this.DifficultyTextBox = new System.Windows.Forms.TextBox();
			this.DifficultyLabel = new System.Windows.Forms.Label();
			this.TagsTextBox = new System.Windows.Forms.TextBox();
			this.SliderTickRateNum = new System.Windows.Forms.NumericUpDown();
			this.SliderTickRateLabel = new System.Windows.Forms.Label();
			this.SaveOszFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.ChangeLanguageButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Beatmaps)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beatmapQueueBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SongDurationNum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HPNum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ODNum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ARNum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CSNum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SliderTickRateNum)).BeginInit();
			this.SuspendLayout();
			// 
			// Beatmaps
			// 
			this.Beatmaps.AllowUserToAddRows = false;
			this.Beatmaps.AllowUserToDeleteRows = false;
			this.Beatmaps.AllowUserToResizeColumns = false;
			this.Beatmaps.AllowUserToResizeRows = false;
			this.Beatmaps.AutoGenerateColumns = false;
			this.Beatmaps.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Beatmaps.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Beatmaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Beatmaps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
				this.orderDataGridViewTextBoxColumn,
				this.offsetDataGridViewTextBoxColumn,
				this.artistDataGridViewTextBoxColumn,
				this.titleDataGridViewTextBoxColumn,
				this.Creator,
				this.diffNameDataGridViewTextBoxColumn
			});
			this.Beatmaps.DataSource = this.beatmapQueueBindingSource;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.Beatmaps.DefaultCellStyle = dataGridViewCellStyle1;
			this.Beatmaps.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			resources.ApplyResources(this.Beatmaps, "Beatmaps");
			this.Beatmaps.MultiSelect = false;
			this.Beatmaps.Name = "Beatmaps";
			this.Beatmaps.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.Beatmaps.RowHeadersVisible = false;
			this.Beatmaps.RowTemplate.Height = 21;
			this.Beatmaps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.Beatmaps.SelectionChanged += new System.EventHandler(this.Beatmaps_SelectionChanged);
			// 
			// orderDataGridViewTextBoxColumn
			// 
			this.orderDataGridViewTextBoxColumn.DataPropertyName = "Order";
			this.orderDataGridViewTextBoxColumn.FillWeight = 20F;
			resources.ApplyResources(this.orderDataGridViewTextBoxColumn, "orderDataGridViewTextBoxColumn");
			this.orderDataGridViewTextBoxColumn.Name = "orderDataGridViewTextBoxColumn";
			this.orderDataGridViewTextBoxColumn.ReadOnly = true;

			// 
			// offsetDataGridViewTextBoxColumn
			// 
			this.offsetDataGridViewTextBoxColumn.DataPropertyName = "Offset";
			this.offsetDataGridViewTextBoxColumn.FillWeight = 20F;
			resources.ApplyResources(this.offsetDataGridViewTextBoxColumn, "offsetDataGridViewTextBoxColumn");
			this.offsetDataGridViewTextBoxColumn.Name = "offsetDataGridViewTextBoxColumn";
			this.offsetDataGridViewTextBoxColumn.ReadOnly = false;
			// 
			// artistDataGridViewTextBoxColumn
			// 
			this.artistDataGridViewTextBoxColumn.DataPropertyName = "Artist";
			this.artistDataGridViewTextBoxColumn.FillWeight = 90F;
			resources.ApplyResources(this.artistDataGridViewTextBoxColumn, "artistDataGridViewTextBoxColumn");
			this.artistDataGridViewTextBoxColumn.Name = "artistDataGridViewTextBoxColumn";
			this.artistDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// titleDataGridViewTextBoxColumn
			// 
			this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
			this.titleDataGridViewTextBoxColumn.FillWeight = 70F;
			resources.ApplyResources(this.titleDataGridViewTextBoxColumn, "titleDataGridViewTextBoxColumn");
			this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
			this.titleDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// Creator
			// 
			this.Creator.DataPropertyName = "Creator";
			this.Creator.FillWeight = 70F;
			resources.ApplyResources(this.Creator, "Creator");
			this.Creator.Name = "Creator";
			this.Creator.ReadOnly = true;
			// 
			// diffNameDataGridViewTextBoxColumn
			// 
			this.diffNameDataGridViewTextBoxColumn.DataPropertyName = "DiffName";
			this.diffNameDataGridViewTextBoxColumn.FillWeight = 70F;
			resources.ApplyResources(this.diffNameDataGridViewTextBoxColumn, "diffNameDataGridViewTextBoxColumn");
			this.diffNameDataGridViewTextBoxColumn.Name = "diffNameDataGridViewTextBoxColumn";
			this.diffNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// beatmapQueueBindingSource
			// 
			this.beatmapQueueBindingSource.DataSource = typeof(OsuBeatmapMixer.BeatmapQueue);
			// 
			// Beatmaps_Label
			// 
			resources.ApplyResources(this.Beatmaps_Label, "Beatmaps_Label");
			this.Beatmaps_Label.Name = "Beatmaps_Label";
			// 
			// Beatmap_Add
			// 
			resources.ApplyResources(this.Beatmap_Add, "Beatmap_Add");
			this.Beatmap_Add.Name = "Beatmap_Add";
			this.Beatmap_Add.UseVisualStyleBackColor = true;
			this.Beatmap_Add.Click += new System.EventHandler(this.Beatmap_Add_Click);
			// 
			// OpenOsuFileDialog
			// 
			resources.ApplyResources(this.OpenOsuFileDialog, "OpenOsuFileDialog");
			this.OpenOsuFileDialog.RestoreDirectory = true;
			// 
			// BeatmapOrderUp
			// 
			resources.ApplyResources(this.BeatmapOrderUp, "BeatmapOrderUp");
			this.BeatmapOrderUp.Name = "BeatmapOrderUp";
			this.BeatmapOrderUp.UseVisualStyleBackColor = true;
			this.BeatmapOrderUp.Click += new System.EventHandler(this.BeatmapOrderUp_Click);
			// 
			// BeatmapOrderDown
			// 
			resources.ApplyResources(this.BeatmapOrderDown, "BeatmapOrderDown");
			this.BeatmapOrderDown.Name = "BeatmapOrderDown";
			this.BeatmapOrderDown.UseVisualStyleBackColor = true;
			this.BeatmapOrderDown.Click += new System.EventHandler(this.BeatmapOrderDown_Click);
			// 
			// BeatmapDeleteButton
			// 
			resources.ApplyResources(this.BeatmapDeleteButton, "BeatmapDeleteButton");
			this.BeatmapDeleteButton.Name = "BeatmapDeleteButton";
			this.BeatmapDeleteButton.UseVisualStyleBackColor = true;
			this.BeatmapDeleteButton.Click += new System.EventHandler(this.BeatmapDeleteButton_Click);
			// 
			// TiTleLabel
			// 
			resources.ApplyResources(this.TiTleLabel, "TiTleLabel");
			this.TiTleLabel.Name = "TiTleLabel";
			// 
			// TitleTextBox
			// 
			resources.ApplyResources(this.TitleTextBox, "TitleTextBox");
			this.TitleTextBox.Name = "TitleTextBox";
			// 
			// ArtistLabel
			// 
			resources.ApplyResources(this.ArtistLabel, "ArtistLabel");
			this.ArtistLabel.Name = "ArtistLabel";
			// 
			// ArtistTextBox
			// 
			resources.ApplyResources(this.ArtistTextBox, "ArtistTextBox");
			this.ArtistTextBox.Name = "ArtistTextBox";
			// 
			// HPLabel
			// 
			resources.ApplyResources(this.HPLabel, "HPLabel");
			this.HPLabel.Name = "HPLabel";
			// 
			// ODLabel
			// 
			resources.ApplyResources(this.ODLabel, "ODLabel");
			this.ODLabel.Name = "ODLabel";
			// 
			// ARLabel
			// 
			resources.ApplyResources(this.ARLabel, "ARLabel");
			this.ARLabel.Name = "ARLabel";
			// 
			// SourceTextBox
			// 
			resources.ApplyResources(this.SourceTextBox, "SourceTextBox");
			this.SourceTextBox.Name = "SourceTextBox";
			// 
			// SourceLabel
			// 
			resources.ApplyResources(this.SourceLabel, "SourceLabel");
			this.SourceLabel.Name = "SourceLabel";
			// 
			// TagsLabel
			// 
			resources.ApplyResources(this.TagsLabel, "TagsLabel");
			this.TagsLabel.Name = "TagsLabel";
			// 
			// ProgressBar1
			// 
			resources.ApplyResources(this.ProgressBar1, "ProgressBar1");
			this.ProgressBar1.Name = "ProgressBar1";
			// 
			// ExecuteButton
			// 
			resources.ApplyResources(this.ExecuteButton, "ExecuteButton");
			this.ExecuteButton.Name = "ExecuteButton";
			this.ExecuteButton.UseVisualStyleBackColor = true;
			this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
			// 
			// BackgroundWorker1
			// 
			this.BackgroundWorker1.WorkerReportsProgress = true;
			this.BackgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
			this.BackgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
			this.BackgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
			// 
			// CSLabel
			// 
			resources.ApplyResources(this.CSLabel, "CSLabel");
			this.CSLabel.Name = "CSLabel";
			// 
			// SongDurationNum
			// 
			resources.ApplyResources(this.SongDurationNum, "SongDurationNum");
			this.SongDurationNum.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
			this.SongDurationNum.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.SongDurationNum.Name = "SongDurationNum";
			this.SongDurationNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// SongDurationLabel
			// 
			resources.ApplyResources(this.SongDurationLabel, "SongDurationLabel");
			this.SongDurationLabel.Name = "SongDurationLabel";
			// 
			// HPNum
			// 
			this.HPNum.DecimalPlaces = 2;
			resources.ApplyResources(this.HPNum, "HPNum");
			this.HPNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.HPNum.Name = "HPNum";
			this.HPNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// ODNum
			// 
			this.ODNum.DecimalPlaces = 2;
			resources.ApplyResources(this.ODNum, "ODNum");
			this.ODNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.ODNum.Name = "ODNum";
			this.ODNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// ARNum
			// 
			this.ARNum.DecimalPlaces = 2;
			resources.ApplyResources(this.ARNum, "ARNum");
			this.ARNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.ARNum.Name = "ARNum";
			this.ARNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// CSNum
			// 
			this.CSNum.DecimalPlaces = 2;
			resources.ApplyResources(this.CSNum, "CSNum");
			this.CSNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.CSNum.Name = "CSNum";
			this.CSNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// CreatorTextBox
			// 
			resources.ApplyResources(this.CreatorTextBox, "CreatorTextBox");
			this.CreatorTextBox.Name = "CreatorTextBox";
			// 
			// CreatorLabel
			// 
			resources.ApplyResources(this.CreatorLabel, "CreatorLabel");
			this.CreatorLabel.Name = "CreatorLabel";
			// 
			// DifficultyTextBox
			// 
			resources.ApplyResources(this.DifficultyTextBox, "DifficultyTextBox");
			this.DifficultyTextBox.Name = "DifficultyTextBox";
			// 
			// DifficultyLabel
			// 
			resources.ApplyResources(this.DifficultyLabel, "DifficultyLabel");
			this.DifficultyLabel.Name = "DifficultyLabel";
			// 
			// TagsTextBox
			// 
			resources.ApplyResources(this.TagsTextBox, "TagsTextBox");
			this.TagsTextBox.Name = "TagsTextBox";
			// 
			// SliderTickRateNum
			// 
			this.SliderTickRateNum.DecimalPlaces = 2;
			resources.ApplyResources(this.SliderTickRateNum, "SliderTickRateNum");
			this.SliderTickRateNum.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.SliderTickRateNum.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			this.SliderTickRateNum.Name = "SliderTickRateNum";
			this.SliderTickRateNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// SliderTickRateLabel
			// 
			resources.ApplyResources(this.SliderTickRateLabel, "SliderTickRateLabel");
			this.SliderTickRateLabel.Name = "SliderTickRateLabel";
			// 
			// SaveOszFileDialog
			// 
			resources.ApplyResources(this.SaveOszFileDialog, "SaveOszFileDialog");
			this.SaveOszFileDialog.RestoreDirectory = true;
			// 
			// ChangeLanguageButton
			// 
			resources.ApplyResources(this.ChangeLanguageButton, "ChangeLanguageButton");
			this.ChangeLanguageButton.Name = "ChangeLanguageButton";
			this.ChangeLanguageButton.Tag = "";
			this.ChangeLanguageButton.UseVisualStyleBackColor = true;
			this.ChangeLanguageButton.Click += new System.EventHandler(this.ChangeLanguageButton_Click);
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.ChangeLanguageButton);
			this.Controls.Add(this.SliderTickRateNum);
			this.Controls.Add(this.SliderTickRateLabel);
			this.Controls.Add(this.TagsTextBox);
			this.Controls.Add(this.DifficultyTextBox);
			this.Controls.Add(this.DifficultyLabel);
			this.Controls.Add(this.CreatorTextBox);
			this.Controls.Add(this.CreatorLabel);
			this.Controls.Add(this.SongDurationNum);
			this.Controls.Add(this.SongDurationLabel);
			this.Controls.Add(this.CSNum);
			this.Controls.Add(this.CSLabel);
			this.Controls.Add(this.ExecuteButton);
			this.Controls.Add(this.ProgressBar1);
			this.Controls.Add(this.TagsLabel);
			this.Controls.Add(this.SourceTextBox);
			this.Controls.Add(this.SourceLabel);
			this.Controls.Add(this.ARNum);
			this.Controls.Add(this.ARLabel);
			this.Controls.Add(this.ODNum);
			this.Controls.Add(this.HPNum);
			this.Controls.Add(this.ODLabel);
			this.Controls.Add(this.HPLabel);
			this.Controls.Add(this.ArtistTextBox);
			this.Controls.Add(this.ArtistLabel);
			this.Controls.Add(this.TitleTextBox);
			this.Controls.Add(this.TiTleLabel);
			this.Controls.Add(this.BeatmapDeleteButton);
			this.Controls.Add(this.BeatmapOrderDown);
			this.Controls.Add(this.BeatmapOrderUp);
			this.Controls.Add(this.Beatmap_Add);
			this.Controls.Add(this.Beatmaps_Label);
			this.Controls.Add(this.Beatmaps);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			((System.ComponentModel.ISupportInitialize)(this.Beatmaps)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beatmapQueueBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SongDurationNum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HPNum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ODNum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ARNum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CSNum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SliderTickRateNum)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView Beatmaps;
		private System.Windows.Forms.Label Beatmaps_Label;
		private System.Windows.Forms.Button Beatmap_Add;
		private System.Windows.Forms.OpenFileDialog OpenOsuFileDialog;
		private System.Windows.Forms.BindingSource beatmapQueueBindingSource;
		private System.Windows.Forms.Button BeatmapOrderUp;
		private System.Windows.Forms.Button BeatmapOrderDown;
		private System.Windows.Forms.Button BeatmapDeleteButton;
		private System.Windows.Forms.Label TiTleLabel;
		private System.Windows.Forms.TextBox TitleTextBox;
		private System.Windows.Forms.Label ArtistLabel;
		private System.Windows.Forms.TextBox ArtistTextBox;
		private System.Windows.Forms.Label HPLabel;
		private System.Windows.Forms.Label ODLabel;
		private System.Windows.Forms.Label ARLabel;
		private System.Windows.Forms.TextBox SourceTextBox;
		private System.Windows.Forms.Label SourceLabel;
		private System.Windows.Forms.Label TagsLabel;
		private System.Windows.Forms.ProgressBar ProgressBar1;
		private System.Windows.Forms.Button ExecuteButton;
		private System.ComponentModel.BackgroundWorker BackgroundWorker1;
		private System.Windows.Forms.Label CSLabel;
		private System.Windows.Forms.NumericUpDown SongDurationNum;
		private System.Windows.Forms.Label SongDurationLabel;
		private System.Windows.Forms.NumericUpDown HPNum;
		private System.Windows.Forms.NumericUpDown ODNum;
		private System.Windows.Forms.NumericUpDown ARNum;
		private System.Windows.Forms.NumericUpDown CSNum;
		private System.Windows.Forms.TextBox CreatorTextBox;
		private System.Windows.Forms.Label CreatorLabel;
		private System.Windows.Forms.TextBox DifficultyTextBox;
		private System.Windows.Forms.Label DifficultyLabel;
		private System.Windows.Forms.TextBox TagsTextBox;
		private System.Windows.Forms.NumericUpDown SliderTickRateNum;
		private System.Windows.Forms.Label SliderTickRateLabel;
		private System.Windows.Forms.SaveFileDialog SaveOszFileDialog;
		private System.Windows.Forms.DataGridViewTextBoxColumn orderDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn offsetDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn artistDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Creator;
		private System.Windows.Forms.DataGridViewTextBoxColumn diffNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.Button ChangeLanguageButton;
	}
}

