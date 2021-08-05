using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// A pair of generic methods to save and load data to files.
/// </summary>
public static class SaveAndLoad
{
	private static string GetPath()
	{
		return Application.dataPath;
	}

	/// <summary>
	/// Serialize and save data to a file.
	/// </summary>
	/// <typeparam name="T">A serializable class. THIS IS A PRECONDITION</typeparam>
	/// <param name="data">The data object to serialize.</param>
	/// <param name="folder">The folder where the data will be saved.</param>
	/// <param name="name">The name and extension of the file to save the data.</param>
	public static void SaveData<T>(T data, string folder, string name)
	{
		if (!Directory.Exists(string.Format("{0}/{1}", GetPath(), folder)))
			Directory.CreateDirectory(string.Format("{0}/{1}", GetPath(), folder));

		BinaryFormatter formatter = new BinaryFormatter();
		string path = string.Format("{0}/{1}/{2}", GetPath(), folder, name);
		FileStream stream = new FileStream(path, FileMode.Create);
		formatter.Serialize(stream, data);
		stream.Close();
	}

	/// <summary>
	/// Deserialize and return data from a file.
	/// </summary>
	/// <typeparam name="T">A serializable class. THIS IS A PRECONDITION</typeparam>
	/// <param name="folder">The folder where the data will be loaded from.</param>
	/// <param name="name">The name and extension of the file to load.</param>
	public static T LoadData<T>(string folder, string name)
	{
		if (!Directory.Exists(string.Format("{0}/{1}", GetPath(), folder)))
			Directory.CreateDirectory(string.Format("{0}/{1}", GetPath(), folder));
		if (!File.Exists(string.Format("{0}/{1}/{2}", GetPath(), folder, name)))
			return default(T);

		BinaryFormatter formatter = new BinaryFormatter();
		string path = string.Format("{0}/{1}/{2}", GetPath(), folder, name);
		FileStream stream = new FileStream(path, FileMode.Open);
		T data = (T)formatter.Deserialize(stream);
		stream.Close();
		return data;
	}
}
