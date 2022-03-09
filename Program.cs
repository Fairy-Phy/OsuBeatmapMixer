using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsuBeatmapMixer {
	static class Program {

		static System.Globalization.CultureInfo Ja_JP => new System.Globalization.CultureInfo("ja-JP");

		static System.Globalization.CultureInfo En_US => new System.Globalization.CultureInfo("en-US");

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] Args) {
			if (Args.Length > 0) {
				try {
					System.Globalization.CultureInfo.CurrentUICulture =
						System.Globalization.CultureInfo.GetCultureInfo(Args[0]);
				}
				catch (System.Globalization.CultureNotFoundException) {
					MessageBox.Show($"{Args[0]} is not culture name.");
				}
			}
			if (System.Globalization.CultureInfo.CurrentCulture.Name != Ja_JP.Name) {
				System.Globalization.CultureInfo.CurrentCulture = En_US;
			}

			#if DEBUG
			MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentUICulture?.Name);
			MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture?.Name);
			MessageBox.Show(System.Globalization.CultureInfo.CurrentUICulture?.Name);
			MessageBox.Show(System.Globalization.CultureInfo.CurrentCulture?.Name);
			MessageBox.Show(System.Globalization.CultureInfo.DefaultThreadCurrentCulture?.Name);
			MessageBox.Show(System.Globalization.CultureInfo.DefaultThreadCurrentUICulture?.Name);
			#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
