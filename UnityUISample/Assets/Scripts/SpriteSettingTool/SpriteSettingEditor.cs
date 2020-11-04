using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.U2D;

public class SpriteSettingEditor : EditorWindow
{
    static SpriteSettingEditor window;
	static SpriteAtlas curAtlas;		//선택한 아틀라스
	static SpriteAtlas drawAtlas;		//현재 보여주는 아틀라스

	/// 격자무늬 배경
	Texture2D mBackdropTex;
	public Texture2D backdropTexture
	{
		get
		{
			if (mBackdropTex == null) mBackdropTex = CreateCheckerTex(
				new Color(0.1f, 0.1f, 0.1f, 0.5f),
				new Color(0.2f, 0.2f, 0.2f, 0.5f));
			return mBackdropTex;
		}
	}

	//아틀라스에서 얻어온 복사생성된 Sprite 배열
	Sprite[] arrSprites;
	//선택한 Sprite
	Sprite selectedSprite;
	//처음 선택해서 들어온 Image
    Image img_current;

	//아틀라스안에서 스프라이트 검색
	string str_search;
	//Scroll 위치
	Vector2 mPos;
	//Double 클릭 체크용 시간 변수
	float mClickTime = 0f;

	[MenuItem("GameObject/Sprite Setting Tool", false, 11)]
    public static void OpenWindow()
    {
        if(window == null)
            window = CreateInstance<SpriteSettingEditor>();
       
        window.Init(Selection.activeGameObject);
    }

	//[MenuItem("GameObject/Sprite Setting Tool", true, 11)]
	//public static bool CheckGameObject()
	//{
	//	return Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Image>() != null;
	//}

	/// <summary>
	/// 초기화
	/// </summary>
	/// <param name="_selectionGO">선택한 게임 오브젝트를 받는다.</param>
	void Init(GameObject _selectionGO)
    {
		SetTargetImageObject(_selectionGO);

		minSize = new Vector2(400, 600);
		window.titleContent.text = "Sprite Setting Editor";
		if (curAtlas != null)
		{
			SetSpritesArray(true);
        }
        Show();
    }

	/// <summary>
	/// 대상 이미지 오브젝트를 세팅한다.
	/// </summary>
	/// <param name="_selectionGO"></param>
	void SetTargetImageObject(GameObject _selectionGO)
	{
		if (_selectionGO == null)
		{
			Debug.LogError("Sprite Setting Error : Do not select active GameObject");
			return;
		}

		Image _prevImage = img_current;
		img_current = _selectionGO.GetComponent<Image>();
		if (img_current == null)
		{
			Debug.LogWarning("Sprite Setting Warning : This GameObject not have Image component");
		}

		if (img_current != _prevImage)
		{
			Repaint();
		}
	}

    /// <summary>
    /// 스프라이트 이름으로 세팅한다.
    /// </summary>
    /// <param name="_spriteName"></param>
    void SetSprite(string _spriteName)
    {
		if (img_current == null)
		{
			EditorUtility.DisplayDialog("오류", "선택된 오브젝트에 Image컴포넌트가 없습니다.", "확인");
			return;
		}
        Sprite sprite = curAtlas.GetSprite(_spriteName);
		//SpriteAtlas의 GetSprite는 Clone으로 생성해서 반환하기 때문에 원본 텍스쳐를 접근해서 얻은 경로로 다시 Sprite를 로드한다.
        img_current.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(sprite.texture));
    }

	/// <summary>
	/// 스프라이트 배열을 세팅한다.
	/// </summary>
	void SetSpritesArray(bool _forced = false)
	{
		//지금 그리고 있는 아틀라스가 없거나 다른경우에 해당 아틀라스에서 스프라이트들을 얻는다.
		if (_forced == true || drawAtlas == null || drawAtlas != curAtlas)
		{
			drawAtlas = curAtlas;
			arrSprites = new Sprite[curAtlas.spriteCount];
			curAtlas.GetSprites(arrSprites);
		}
	}

	/// <summary>
	/// 스프라이트를 더블 클릭했을때 이벤트
	/// </summary>
	void DoubleClickEvent()
	{
		if (Event.current.button == 0)
		{
			float delta = Time.realtimeSinceStartup - mClickTime;
			mClickTime = Time.realtimeSinceStartup;

			if (selectedSprite != null && delta < 0.5f)
			{
				SetSprite(selectedSprite.name.Replace("(Clone)", ""));
				//Close();
			}
		}
	}

	/// <summary>
	/// 아틀라스에서 스프라이트 이름을 얻는다.
	/// </summary>
	/// <param name="_index"></param>
	/// <returns></returns>
	string GetAtlasSpriteName(int _index)
	{
		return arrSprites[_index].name.Replace("(Clone)", "");
	}

	#region Draw GUI
	/// <summary>
	/// 아틀라스 선택 GUI를 그린다.
	/// </summary>
	void DrawAtlasSelector()
    {
        GUILayout.BeginHorizontal();
		img_current = (Image)EditorGUILayout.ObjectField(img_current, typeof(Image), true, GUILayout.Width(150));
		curAtlas = (SpriteAtlas)EditorGUILayout.ObjectField(curAtlas, typeof(SpriteAtlas), false);
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// 스프라이트 선택 GUI를 그린다.
    /// </summary>
    void DrawSpriteSelector()
    {
        if (curAtlas == null)
		{
			GUILayout.BeginHorizontal();
			GUI.enabled = false;
            GUILayout.TextField("Select Atlas");
			GUILayout.EndHorizontal();
		}
        else
        {
			SetSpritesArray();
		}
        GUI.enabled = true;
    }

	/// <summary>
	/// 스프라이트 리스트를 그린다.
	/// </summary>
	void DrawSpriteList()
	{
		if (curAtlas == null)
			return;

		DrawSpriteSearch();

		float size = 80f;
		float padded = size + 10f;
		int screenWidth = (int)EditorGUIUtility.currentViewWidth;
		int columns = Mathf.FloorToInt(screenWidth / padded);
		if (columns < 1) columns = 1;

		int offset = 0;
		int _index = 0;
		Rect rect = new Rect(10f, 0, size, size);
		GUILayout.Space(10f);
		mPos = GUILayout.BeginScrollView(mPos);
		int rows = 1;

		List<Sprite> _resultSprite = new List<Sprite>(arrSprites);
		if(string.IsNullOrEmpty(str_search) == false)
            _resultSprite.RemoveAll(_item => _item.name.Contains(str_search) == false);
		while (offset < _resultSprite.Count)
		{
			GUILayout.BeginHorizontal();
			{
				int col = 0;
				rect.x = 10f;

				for (; offset < _resultSprite.Count; ++offset)
				{
					if (GUI.Button(rect, ""))
					{
						selectedSprite = _resultSprite[_index];
						DoubleClickEvent();
					}

					if (Event.current.type == EventType.Repaint)
					{
						DrawTexture(rect, backdropTexture);

						Rect uv = _resultSprite[_index].textureRect;
						uv = ConvertToTexCoords(uv, _resultSprite[_index].texture.width, _resultSprite[_index].texture.height);

						float scaleX = rect.width / uv.width;
						float scaleY = rect.height / uv.height;

						float aspect = (scaleY / scaleX);
						Rect clipRect = rect;
						if (aspect != 1f)
						{
							if (aspect < 1f)
							{
								float padding = size * (1f - aspect) * 0.5f;
								clipRect.xMin += padding;
								clipRect.xMax -= padding;
							}
							else
							{
								float padding = size * (1f - 1f / aspect) * 0.5f;
								clipRect.yMin += padding;
								clipRect.yMax -= padding;
							}
						}

						GUI.DrawTextureWithTexCoords(clipRect, _resultSprite[_index].texture, uv);

						if (selectedSprite == _resultSprite[_index])
						{
							DrawOutline(rect, new Color(0.4f, 1f, 0f, 1f));
						}
					}

					GUI.backgroundColor = new Color(1f, 1f, 1f, 0.5f);
					GUI.contentColor = new Color(1f, 1f, 1f, 0.7f);
					GUI.Label(new Rect(rect.x, rect.y + rect.height, rect.width, 32f), _resultSprite[_index].name.Replace("(Clone)", ""), "ProgressBarBack");
					GUI.contentColor = Color.white;
					GUI.backgroundColor = Color.white;
					_index++;

					if (++col >= columns)
					{
						++offset;
						break;
					}
					rect.x += padded;
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(padded);
			rect.y += padded + 26;
			++rows;
		}
		GUILayout.Space(rows * 26);
		GUILayout.EndScrollView();
	}

	void DrawSpriteSearch()
	{
		GUILayout.BeginHorizontal();
		GUIStyle _gUIStyle = GUI.skin.FindStyle("ToolbarSeachTextField");
		_gUIStyle.stretchWidth = true;
		str_search = EditorGUILayout.TextField(str_search, _gUIStyle);
		if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
		{
			// Remove focus if cleared
			str_search = string.Empty;
			GUI.FocusControl(null);
		}
		GUILayout.EndHorizontal();
	}
	/// <summary>
	/// 타일 텍스쳐 그리기
	/// </summary>
	/// <param name="rect">그리는 영역</param>
	/// <param name="tex">이미지 텍스쳐</param>
	void DrawTexture(Rect rect, Texture tex)
	{
		GUI.BeginGroup(rect);
		{
			int width = Mathf.RoundToInt(rect.width);
			int height = Mathf.RoundToInt(rect.height);

			for (int y = 0; y < height; y += tex.height)
			{
				for (int x = 0; x < width; x += tex.width)
				{
					GUI.DrawTexture(new Rect(x, y, tex.width, tex.height), tex);
				}
			}
		}
		GUI.EndGroup();
	}

	/// <summary>
	/// 아웃라인 그리기
	/// </summary>
	/// <param name="rect">그리는 영역</param>
	/// <param name="color">색상</param>
	void DrawOutline(Rect rect, Color color)
	{
		if (Event.current.type == EventType.Repaint)
		{
			Texture2D tex = EditorGUIUtility.whiteTexture;
			GUI.color = color;
			GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, 1f, rect.height), tex);
			GUI.DrawTexture(new Rect(rect.xMax, rect.yMin, 1f, rect.height), tex);
			GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, rect.width, 1f), tex);
			GUI.DrawTexture(new Rect(rect.xMin, rect.yMax, rect.width, 1f), tex);
			GUI.color = Color.white;
		}
	}

	/// <summary>
	/// 체크무늬 텍스쳐 세팅
	/// </summary>
	/// <param name="c0">체크 색상</param>
	/// <param name="c1">체크 색상</param>
	/// <returns></returns>
	Texture2D CreateCheckerTex(Color c0, Color c1)
	{
		Texture2D tex = new Texture2D(16, 16);
		tex.name = "[Generated] Checker Texture";
		tex.hideFlags = HideFlags.DontSave;

		for (int y = 0; y < 8; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c1);
		for (int y = 8; y < 16; ++y) for (int x = 0; x < 8; ++x) tex.SetPixel(x, y, c0);
		for (int y = 0; y < 8; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c0);
		for (int y = 8; y < 16; ++y) for (int x = 8; x < 16; ++x) tex.SetPixel(x, y, c1);

		tex.Apply();
		tex.filterMode = FilterMode.Point;
		return tex;
	}

	/// <summary>
	/// Rect의 좌상단 베이스를 우하단 베이스로 변경
	/// </summary>
	/// <param name="rect">대상 Rect</param>
	/// <param name="width">너비</param>
	/// <param name="height">높이</param>
	/// <returns></returns>
	Rect ConvertToTexCoords(Rect rect, int width, int height)
	{
		Rect final = rect;

		if (width != 0f && height != 0f)
		{
			final.xMin = rect.xMin / width;
			final.xMax = rect.xMax / width;
			final.yMin = 1f - rect.yMax / height;
			final.yMax = 1f - rect.yMin / height;
		}
		return final;
	}

	void OnGUI()
    {
        DrawAtlasSelector();
        DrawSpriteSelector();
		DrawSpriteList();
    }

	void OnSelectionChange()
	{
		SetTargetImageObject(Selection.activeGameObject);
	}
	#endregion Draw GUI
}
