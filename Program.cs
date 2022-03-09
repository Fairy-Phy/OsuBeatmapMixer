using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsuBeatmapMixer {
	static class Program {
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] Args) {
			if (Args.Length > 0) {
				try {
					System.Threading.Thread.CurrentThread.CurrentUICulture =
						System.Globalization.CultureInfo.GetCultureInfo(Args[0]);
				}
				catch (System.Globalization.CultureNotFoundException) {
					MessageBox.Show($"{Args[0]} is not culture name.");
				}
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
