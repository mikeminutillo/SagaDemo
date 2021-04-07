<Query Kind="Statements" />

var rootFolder = Path.GetDirectoryName(Util.CurrentQueryPath).Dump("Cleaning up");

var patterns = new[] {
	".sagas",
	".learningtransport",
	".db",
	".audit-db",
	".logs"
};

foreach (var pattern in patterns)
{
	foreach (var dir in Directory.EnumerateDirectories(rootFolder, pattern, SearchOption.AllDirectories))
	{
		foreach (var file in Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories))
		{
			File.Delete(file);
		}

		Directory.Delete(dir, true);
	}
}