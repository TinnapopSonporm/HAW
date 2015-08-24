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
		private GameObject[] _slotObject;
		//hack Slot Assign
		private List<int[]> slot = new List<int[]>();
		private int[] slot1 = new int[15]{0, 0, 0, 1, 1, 1, 2, 2, 2, 1, 1, 2, 2, 0, 0};
		private int[] slot2 = new int[15]{0, 0, 0, 1, 1, 1, 2, 2, 2, 0, 0, 2, 2, 1, 1};
		private int[] slot3 = new int[15]{0, 0, 0, 1, 1, 1, 2, 2, 2, 1, 2, 0, 0, 2, 1};
		private Texture2D[] testTex;

		public SlotTextureGenerator()
		{
			_slotType = new Texture2D[3];
			_slotObject = new GameObject[3];
		}

		void Start()
		{
			_symbolTexture = Resources.Load("CoreSymbol") as Texture2D;
			for (int i = 0;i < SLOT_SLOT;i++)
			{
				_slotObject[i] = GameObject.Find("SLOT" + (i + 1).ToString());
			}
			SlotDefine();
			SetupSlotTexture();
			AssignTextureToSlot();
		}

		void SlotDefine()
		{
			slot.Add(slot1);
			slot.Add(slot2);
			slot.Add(slot3);
		}

		void SetupSlotTexture()
		{
			for (int i = 0; i < _slotType.Length; i++)
			{
				Color[] tempColor = _symbolTexture.GetPixels(SYMBOL_SIZE * i, 0, SYMBOL_SIZE, SYMBOL_SIZE);
				_slotType[i] = new Texture2D(SYMBOL_SIZE,SYMBOL_SIZE);
				_slotType[i].SetPixels(tempColor);
				_slotType[i].Apply();
			}
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
		void AssignTextureToSlot()
		{
			for (int i = 0; i < _slotObject.Length;i++)
			{
				_slotObject[i].GetComponent<MeshRenderer>().materials[0].mainTexture = testTex[i];
				_slotObject[i].GetComponent<MeshRenderer>().materials[0].SetTextureScale("_MainTex" ,new Vector2(1f, 0.2f));
			}
		}
	}
	public enum SlotType
	{
		STR,
		DEX,
		INT
	}
}

