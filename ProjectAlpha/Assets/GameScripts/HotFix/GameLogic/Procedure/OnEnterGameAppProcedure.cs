﻿using TEngine;

namespace KyuuGames
{
    public class OnEnterGameAppProcedure : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Log.Debug("OnEnter GameApp Procedure");
        }
    }
}