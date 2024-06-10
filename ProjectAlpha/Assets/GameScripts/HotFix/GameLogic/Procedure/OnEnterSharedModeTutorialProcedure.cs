using TEngine;

namespace GameLogic
{
    public class OnEnterSharedModeTutorialProcedure : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameModule.Scene.LoadScene("tutorial shared mode");
        }
    }
}