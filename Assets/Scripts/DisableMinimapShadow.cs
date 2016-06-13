using UnityEngine;
using System.Collections;

public class DisableMinimapShadow : MonoBehaviour {
	private float storedShadowDistance;

	void OnPreRender() {
		storedShadowDistance = QualitySettings.shadowDistance;
		QualitySettings.shadowDistance = 0;
	}
	void OnPostRender() {
		QualitySettings.shadowDistance = storedShadowDistance;
	}
}
