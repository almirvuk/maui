namespace Microsoft.Maui.Platform.Android
{
	public static class Extensions
	{
		internal static float ToEm(this double pt)
		{
			return (float)pt * 0.0624f; //Coefficient for converting Pt to Em
		}
	}
}
