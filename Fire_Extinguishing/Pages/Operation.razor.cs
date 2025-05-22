using Microsoft.AspNetCore.Components;

namespace Fire_Extinguishing.Pages;

public partial class Operation : ComponentBase
{
    double DAA = 0.0001;
    double T = 0.00001;
    double OSP = 0.00011;
    double DG = 0.055;
    double P1 = 0.018;
    double V11 = 0.0042;
    double V12 = 0.0042;
    double N1 = 0.00001;

    double OP = 0.01;
    double P2 = 0.018;
    double V21 = 0.0042;
    double V22 = 0.0042;
    double N2 = 0.00001;

    double TopFailPercent = 0;
    double CriticalFailPercent = 0;
    double F1Percent = 0;
    double F2Percent = 0;
    double DAA_percent = 0;
    double T_percent = 0;
    double OSP_percent = 0;
    double DG_percent = 0;

    double MultiplySurvivals(params double[] ps)
    {
        double result = 1.0;
        foreach (var p in ps)
            result *= (1 - p);
        return result;
    }

    protected override void OnInitialized() => CalculateFailure();

    void CalculateFailure()
    {
        double SuccessBranch1 = MultiplySurvivals(OSP, DG, P1, V11, V12, N1);
        double SuccessBranch2 = MultiplySurvivals(OP, OSP, DG, P2, V21, V22, N2);

        double F1 = 1 - SuccessBranch1;
        double F2 = 1 - SuccessBranch2;

        F1Percent = F1 * 100;
        F2Percent = F2 * 100;

        // احتمال شکست بحرانی
        double SuccessCritical = (1 - DAA) * (1 - T) * (1 - OSP) * (1 - DG);
        double FailCritical = 1 - SuccessCritical;
        CriticalFailPercent = FailCritical * 100;

        double TopFailure = 1 - SuccessCritical * (1 - F1 * F2);
        TopFailPercent = TopFailure * 100;
    }
}