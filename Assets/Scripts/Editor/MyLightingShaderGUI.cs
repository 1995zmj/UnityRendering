using UnityEngine;
using UnityEditor;

public class MyLightingShaderGUI : ShaderGUI {

	static GUIContent staticLabel = new GUIContent();

	Material target;
	MaterialEditor editor;
	MaterialProperty[] properties;

	public override void OnGUI (
		MaterialEditor editor, MaterialProperty[] properties
	) {
		this.target = editor.target as Material;
		this.editor = editor;
		this.properties = properties;
		DoMain();
		DoSecondary();
	}

	void DoMain () {
		GUILayout.Label("Main Maps", EditorStyles.boldLabel);

		MaterialProperty mainTex = FindProperty("_MainTex");
		editor.TexturePropertySingleLine(
			MakeLabel(mainTex, "Albedo (RGB)"), mainTex, FindProperty("_Tint")
		);
		DoMetallic();
		DoSmoothness();
		DoNormals();
		editor.TextureScaleOffsetProperty(mainTex);
	}

	void DoNormals () {
		MaterialProperty map = FindProperty("_NormalMap");
		editor.TexturePropertySingleLine(
			MakeLabel(map), map,
			map.textureValue ? FindProperty("_BumpScale") : null
		);
	}

	void DoMetallic () {
		MaterialProperty map = FindProperty("_MetallicMap");
		EditorGUI.BeginChangeCheck();
		editor.TexturePropertySingleLine(
			MakeLabel(map, "Metallic (R)"), map,
			map.textureValue ? null : FindProperty("_Metallic")
		);
		if (EditorGUI.EndChangeCheck()) {
			SetKeyword("_METALLIC_MAP", map.textureValue);
		}
	}

	void DoSmoothness () {
		MaterialProperty slider = FindProperty("_Smoothness");
		EditorGUI.indentLevel += 2;
		editor.ShaderProperty(slider, MakeLabel(slider));
		EditorGUI.indentLevel -= 2;
	}

	void DoSecondary () {
		GUILayout.Label("Secondary Maps", EditorStyles.boldLabel);

		MaterialProperty detailTex = FindProperty("_DetailTex");
		editor.TexturePropertySingleLine(
			MakeLabel(detailTex, "Albedo (RGB) multiplied by 2"), detailTex
		);
		DoSecondaryNormals();
		editor.TextureScaleOffsetProperty(detailTex);
	}

	void DoSecondaryNormals () {
		MaterialProperty map = FindProperty("_DetailNormalMap");
		editor.TexturePropertySingleLine(
			MakeLabel(map), map,
			map.textureValue ? FindProperty("_DetailBumpScale") : null
		);
	}

	MaterialProperty FindProperty (string name) {
		return FindProperty(name, properties);
	}

	static GUIContent MakeLabel (string text, string tooltip = null) {
		staticLabel.text = text;
		staticLabel.tooltip = tooltip;
		return staticLabel;
	}

	static GUIContent MakeLabel (
		MaterialProperty property, string tooltip = null
	) {
		staticLabel.text = property.displayName;
		staticLabel.tooltip = tooltip;
		return staticLabel;
	}

	void SetKeyword (string keyword, bool state) {
		if (state) {
			target.EnableKeyword(keyword);
		}
		else {
			target.DisableKeyword(keyword);
		}
	}
}