

using UnityEngine;
using UnityEngine.UI;

namespace XUIF{
	[RequireComponent(typeof(Text))]
	public class Localize : MonoBehaviour {

		public string languageId;

		void Start(){
			Text text = GetComponent<Text> ();
			text.text = LanguageMgr.Instance.GetText (languageId);
			Destroy (this);
		}
	}
}

