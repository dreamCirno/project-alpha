//------------------------------------------------------------------------------
//	<auto-generated>
//		This code was generated by autoBindTool.
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
//	</auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace KyuuGames
{
	public partial class IActorLogicEvent_Event
	{
		public static readonly int OnMainPlayerDataChange = RuntimeId.ToRuntimeId("IActorLogicEvent_Event.OnMainPlayerDataChange");
		public static readonly int OnMainPlayerLevelChange = RuntimeId.ToRuntimeId("IActorLogicEvent_Event.OnMainPlayerLevelChange");
		public static readonly int OnMainPlayerGoldChange = RuntimeId.ToRuntimeId("IActorLogicEvent_Event.OnMainPlayerGoldChange");
		public static readonly int OnMainPlayerDiamondChange = RuntimeId.ToRuntimeId("IActorLogicEvent_Event.OnMainPlayerDiamondChange");
		public static readonly int OnMainPlayerBindDiamondChange = RuntimeId.ToRuntimeId("IActorLogicEvent_Event.OnMainPlayerBindDiamondChange");
		public static readonly int OnMainPlayerCurrencyChange = RuntimeId.ToRuntimeId("IActorLogicEvent_Event.OnMainPlayerCurrencyChange");
		public static readonly int OnMainPlayerExpChange = RuntimeId.ToRuntimeId("IActorLogicEvent_Event.OnMainPlayerExpChange");
	}

	[EventInterfaceImp(EEventGroup.GroupLogic)]
	public partial class IActorLogicEvent_Gen : IActorLogicEvent
	{
		private EventDispatcher _dispatcher;
		public IActorLogicEvent_Gen(EventDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

        public void OnMainPlayerDataChange()
        {
            _dispatcher.Send(IActorLogicEvent_Event.OnMainPlayerDataChange);
        }

        public void OnMainPlayerLevelChange()
        {
            _dispatcher.Send(IActorLogicEvent_Event.OnMainPlayerLevelChange);
        }

        public void OnMainPlayerGoldChange(System.UInt32 oldVal,System.UInt32 newVal)
        {
            _dispatcher.Send(IActorLogicEvent_Event.OnMainPlayerGoldChange,oldVal,newVal);
        }

        public void OnMainPlayerDiamondChange(System.UInt32 oldVal,System.UInt32 newVal)
        {
            _dispatcher.Send(IActorLogicEvent_Event.OnMainPlayerDiamondChange,oldVal,newVal);
        }

        public void OnMainPlayerBindDiamondChange(System.UInt32 oldVal,System.UInt32 newVal)
        {
            _dispatcher.Send(IActorLogicEvent_Event.OnMainPlayerBindDiamondChange,oldVal,newVal);
        }

        public void OnMainPlayerCurrencyChange(KyuuGames.CurrencyType type,System.UInt32 oldVal,System.UInt32 newVal)
        {
            _dispatcher.Send(IActorLogicEvent_Event.OnMainPlayerCurrencyChange,type,oldVal,newVal);
        }

        public void OnMainPlayerExpChange(System.UInt64 oldVal,System.UInt64 newVal)
        {
            _dispatcher.Send(IActorLogicEvent_Event.OnMainPlayerExpChange,oldVal,newVal);
        }

	}
}
