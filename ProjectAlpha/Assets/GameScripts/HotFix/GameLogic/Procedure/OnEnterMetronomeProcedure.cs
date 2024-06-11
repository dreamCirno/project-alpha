using TEngine;

namespace GameLogic
{
    public class OnEnterMetronomeProcedure : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            GameModule.Scene.LoadScene("Metronome");
        }
    }
}