using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Slot
{
	public class SlotTextureGenerator : MonoBehaviour 
	{

		private Texture2D _symbolTexture;
		public Texture2D[] _slotType;
		private const int SYMBOL_SIZE = 128;
		private const int SLOT_SLOT = 3;

		//hack Slot Assign
		private List<int[]> slot = new List<int[]>();
		private int[] slot1 = new int[15]{0, 0, 0, 1, 1, 1, 2, 2, 2, 1, 1, 2, 2, 0, 0};
		private int[] slot2 = new int[15]{0, 0, 0, 1, 1, 1, 2, 2, 2, 0, 0, 2, 2, 1, 1};
		private int[] slot3 = new int[15]{0, 0, 0, 1, 1, 1, 2, 2, 2, 1, 2, 0, 0, 2, 1};

		public SlotTextureGenerator()
		{
			_slotType = new Texture2D[3];
		}

		void Start()
		{
			//Load Symbol Texture
			_symbolTexture = Resources.Load("CoreSymbol") as Texture2D;
			SlotDefine();
			SetupSlotTexture();
		}

		void SlotDefine()
		{
			slot.Add(slot1);
			slot.Add(slot2);
			slot.Add(slot3);
		}

		void SetupSlotTexture()
		{

			// Split Symbol one by one
			for (int i = 0; i < _slotType.Length; i++)
			{
				Color[] tempColor = _symbolTexture.GetPixels(SYMBOL_SIZE * i, 0, SYMBOL_SIZE, SYMBOL_SIZE);
				_slotType[i] = new Texture2D(SYMBOL_SIZE,SYMBOL_SIZE);
				_slotType[i].SetPixels(tempColor);
				_slotType[i].Apply();
			}
			//

			Texture2D[] tSlot = new Texture2D[SLOT_SLOT];
			for (int i = 0; i < tSlot.Length; i++)
			{
				tSlot[i] = new Texture2D(SYMBOL_SIZE, SYMBOL_SIZE * slot[i].Length);
				for (int j = 0; j < slot[i].Length; j++)
				{
					tSlot[i].SetPixels(0, j * SYMBOL_SIZE, SYMBOL_SIZE, SYMBOL_SIZE, _slotType[slot[i][j]].GetPixels(0, 0, SYMBOL_SIZE, SYMBOL_SIZE));
				}
				tSlot[i].Apply();
			}
			testTex = tSlot;
		}

		void Update () {
			
		}

		//Test Only
		public Texture2D[] testTex;
		void OnGUI()
		{
			GUI.DrawTexture(new Rect(0,0,testTex[0].width/4, testTex[0].height/4),testTex[0], ScaleMode.ScaleToFit);
			GUI.DrawTexture(new Rect(100,0,testTex[1].width/4, testTex[1].height/4),testTex[1], ScaleMode.ScaleToFit);
			GUI.DrawTexture(new Rect(200,0,testTex[2].width/4, testTex[2].height/4),testTex[2], ScaleMode.ScaleToFit);
		}

			
			
	}
	public enum SlotType
	{
		STR,
		DEX,
		INT
	}
}

