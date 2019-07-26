using System.Collections.Generic;
using System.IO;

namespace RainbowTag
{
	public class DataFileManager
	{

		private static readonly string FolderName = FileManager.GetAppFolder(true)+"RainbowTag";
		private static readonly string FileName = FolderName+Path.DirectorySeparatorChar+"RainbowIDs.txt";
		
		public DataFileManager()
		{
			if (!File.Exists(FileName))
			{
				CreateFile();
			}
		}

		public List<string> ReadFile()
		{
			string[] fileContents = FileManager.ReadAllLines(FileName);
			List<string> steamIdList = new List<string>();
			
			foreach (string fileContent in fileContents)
			{
				if (fileContent.StartsWith("#") || string.IsNullOrEmpty(fileContent))
					continue;

				string steamId = fileContent.Trim();
				steamIdList.Add(steamId);
			}
			return steamIdList;
		}

		private void CreateFile()
		{
			if (!Directory.Exists(FolderName))
				Directory.CreateDirectory(FolderName);
			
			if (!File.Exists(FileName))
				File.Create(FileName).Dispose();

			using (StreamWriter wr = new StreamWriter(FileName))
			{
				wr.WriteLine("# Place SteamIDs in this file that you wish to have a rainbow tag. One ID per line.");
				wr.WriteLine("# Lines that start with a # are commented out and are not parsed. ");
			}
		}
	}
}