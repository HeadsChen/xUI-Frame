using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class Utility {

	[MenuItem("Utility/ClearUp")]
	public static void ClearUpFiles(){
		string[] filesPath = Directory.GetFiles (Application.dataPath, "*.*", SearchOption.AllDirectories);
		for (int i = 0; i < filesPath.Length; i++) {
			if (filesPath [i].EndsWith (".meta") || filesPath [i].EndsWith (".DS_Store")) {
				File.Delete (filesPath [i]);
			}
		}
	}



}
