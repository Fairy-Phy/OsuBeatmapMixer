using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapMixer.Osu {

	internal static class Utils {

		internal static double DoubleParse(string Value) {
			if (double.TryParse(Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double Res))
				return Res;
			#if DEBUG
			Console.WriteLine($"{Value} is NaN");
			#endif
			return double.NaN;
		}

		internal static int ParseToInt(string Value) {
			return (int) DoubleParse(Value);
		}
	}
}
