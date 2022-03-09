using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer {

	class MessageData {

		internal string ChangeLang { get; }

		internal string MessageWarning { get; }

		internal string MessageError { get; }

		internal string MessageConfirmation { get; }

		internal string MessageFinished { get; }

		// The same beatmap already exists in the list. Are you sure you want to add it?
		internal string WarningText1 { get; }

		// {0} already exists. Are you sure you want to overwrite it?
		internal string WarningText2 { get; }

		// Are you sure you want to delete selected beatmap?
		internal string ConfirmationText1 { get; }

		// Are you sure you want to save the file in the folder at the following location?
		internal string ConfirmationText2 { get; }

		// At least 2 beatmaps are required for execution.
		internal string ErrorText1 { get; }

		// Different game modes are mixed in the list.
		internal string ErrorText2 { get; }

		// Different mania mode keys are mixed in the list.
		internal string ErrorText3 { get; }

		// Different slidermultiplier are mixed in the list.
		internal string ErrorText4 { get; }

		// Requires at least the artist name and title.
		internal string ErrorText5 { get; }

		// Please specify the location of the mp3 file.
		internal string ErrorText6 { get; }

		// The process has been canceled.
		internal string ErrorText7 { get; }

		// Mix Successful!
		internal string FinishedText1 { get; }

		internal MessageData() {
			ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));

			ChangeLang = resources.GetString(nameof(ChangeLang));
			MessageWarning = resources.GetString(nameof(MessageWarning));
			MessageError = resources.GetString(nameof(MessageError));
			MessageConfirmation = resources.GetString(nameof(MessageConfirmation));
			MessageFinished = resources.GetString(nameof(MessageFinished));
			WarningText1 = resources.GetString(nameof(WarningText1));
			WarningText2 = resources.GetString(nameof(WarningText2));
			ConfirmationText1 = resources.GetString(nameof(ConfirmationText1));
			ConfirmationText2 = resources.GetString(nameof(ConfirmationText2));
			ErrorText1 = resources.GetString(nameof(ErrorText1));
			ErrorText2 = resources.GetString(nameof(ErrorText2));
			ErrorText3 = resources.GetString(nameof(ErrorText3));
			ErrorText4 = resources.GetString(nameof(ErrorText4));
			ErrorText5 = resources.GetString(nameof(ErrorText5));
			ErrorText6 = resources.GetString(nameof(ErrorText6));
			ErrorText7 = resources.GetString(nameof(ErrorText7));
			FinishedText1 = resources.GetString(nameof(FinishedText1));
		}
	}
}
