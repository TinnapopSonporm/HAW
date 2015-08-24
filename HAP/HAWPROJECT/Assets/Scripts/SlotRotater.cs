using UnityEngine;
using System.Collections;


namespace Slot
{
	public class SlotRotater : MonoBehaviour 
	{
		private Material[] _slotMaterial;
		private bool[] _isRunning;
		private float[] _slotPos = new float[3]{0,0,0};
		private float _slotSpeed = 0.5f;
		private int _currentSlotIndex = 0;

		public SlotRotater()
		{
			_slotMaterial = new Material[3];
			_isRunning = new bool[_slotMaterial.Length];
		}

		void Start () 
		{
			for (int i = 0;i < 3;i++)
			{
				_slotMaterial[i] = GameObject.Find("SLOT" + (i + 1).ToString()).GetComponent<MeshRenderer>().materials[0];
			}
			for (int i = 0; i < _slotMaterial.Length;i++)
			{
				_slotMaterial[i].SetTextureOffset("_MainTex", new Vector2(0, 0.2f * i));
			}

		}
		void Update () 
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (!_isRunning[0] && !_isRunning[1] && !_isRunning[2])
				{
					_isRunning[0] = true;
					_isRunning[1] = true;
					_isRunning[2] = true;
					_currentSlotIndex = 0;
				}
				else
				{
					_isRunning[_currentSlotIndex] = false;
					_currentSlotIndex++;
				}
			}

			for (int i = 0; i < 3; i++)
			{
				if (_isRunning[i])
				{
					if (i%2 == 0)
					{
						_slotPos[i] += Time.deltaTime * _slotSpeed;
					}
					else
					{
						_slotPos[i] -= Time.deltaTime * _slotSpeed;
					}
					_slotMaterial[i].SetTextureOffset("_MainTex", new Vector2(0, _slotPos[i]));
				}
			}
		}
	}
}
