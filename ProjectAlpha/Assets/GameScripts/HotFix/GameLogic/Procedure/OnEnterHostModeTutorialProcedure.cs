using TEngine;

namespace GameLogic
{
    public class OnEnterHostModeTutorialProcedure : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameModule.Scene.LoadScene("tutorial host mode");
        }
    }
}