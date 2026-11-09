using Company.Core.Enums;
using Company.Core.MVVM;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Share.Config
{
    public class TrackAxisParameter:ObservableObject
    {

		public float 取料位 { get; set; }
		private Dictionary<TapeWidth, float> _轨道宽度 = new Dictionary<TapeWidth, float>()
		{
			{TapeWidth._8mm,8 },
			{TapeWidth._12mm,12 },
			{TapeWidth._16mm,16},
			{TapeWidth._24mm,24},
		};


        public Dictionary<TapeWidth, float> 轨道宽度
        {
			get { return _轨道宽度; }
			set { _轨道宽度 = value; OnPropertyChanged(); }


		}

		private KeyValuePair<TapeWidth,float>? _current = null;

		public KeyValuePair<TapeWidth,float>? Current
		{
			get { return _current; }
			set 
			{ 
				
				_current = value; 
				if(_current != null)
				{
					Value = 轨道宽度[Current.Value.Key];
				}
				OnPropertyChanged();

			}
		}

		private float _value;

		public float Value
		{
			get { return _value; }
			set 
			{
				if(Current == null)
				{
					return;
				}
				_value = value;
				OnPropertyChanged();
				轨道宽度[Current.Value.Key] = value;

			}
		}



	}
}
