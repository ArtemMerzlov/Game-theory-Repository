
using System.Linq;
public abstract class Alternative
{
    public string Name { get; }
    public double Localization { get; protected set; }

    // Риски
    public double ImportDisruption { get; protected set; }
    public double TechnologyShift { get; protected set; }
    public double StaffShortage { get; protected set; }
    public double CurrencyAndLogistics { get; protected set; }
    public double InflationAndRate { get; protected set; }

    // Возможности
    public double SpecialOrderGrowth { get; protected set; }
    public double TelematicsBenefits { get; protected set; }
    public double SubsidiesAndGrants { get; protected set; }
    public double StateTransportOrder { get; protected set; }

    protected Alternative(string name)
    {
        Name = name;
    }

    public double CalculateIncome(double baseIncome)
    {
       

        return
              (baseIncome * ImportDisruption)    
            + (baseIncome * TechnologyShift)
            + (baseIncome * StaffShortage)
            + (baseIncome * CurrencyAndLogistics)
            + (baseIncome * InflationAndRate)
            + (baseIncome * SpecialOrderGrowth)
            + (baseIncome * TelematicsBenefits)
            + (baseIncome * SubsidiesAndGrants)
            + (baseIncome * StateTransportOrder);
    }



    public double CalculateFiveYearIncome(double baseIncome)
    {
        double totalIncome = 0;
        double depreciationRate = 0.15;

        for (int year = 1; year <= 5; year++)
        {

            totalIncome += baseIncome/(1+depreciationRate*year);
        }

        return totalIncome;
    }


    public (string FactorName, double MaxProfit) CalculateMaximax(double baseIncome)
    {
        var factors = new (string Name, double Coefficient)[]
        {
        ("Срыв импортозамещения", ImportDisruption),
        ("Технологический сдвиг", TechnologyShift),
        ("Дефицит кадров", StaffShortage),
        ("Валютный и логистический риски", CurrencyAndLogistics),
        ("Рост ставки и инфляция", InflationAndRate),
        ("Рост специального/оборонного заказа", SpecialOrderGrowth),
        ("Преференции за локализацию телематики", TelematicsBenefits),
        ("Субсидии и гранты", SubsidiesAndGrants),
        ("Государственный заказ на спецтранспорт", StateTransportOrder)
        };

        var bestFactor = factors
            .Select(f => (f.Name, Profit: baseIncome * f.Coefficient))
            .OrderByDescending(f => f.Profit)
            .First();

        return (bestFactor.Name, bestFactor.Profit);
    }


    public (string FactorName, double MinProfit) CalculateWald(double baseIncome)
    {
        var factors = new (string Name, double Coefficient)[]
        {
        ("Срыв импортозамещения", ImportDisruption),
        ("Технологический сдвиг", TechnologyShift),
        ("Дефицит кадров", StaffShortage),
        ("Валютный и логистический риски", CurrencyAndLogistics),
        ("Рост ставки и инфляция", InflationAndRate),
        ("Рост специального/оборонного заказа", SpecialOrderGrowth),
        ("Преференции за локализацию телематики", TelematicsBenefits),
        ("Субсидии и гранты", SubsidiesAndGrants),
        ("Государственный заказ на спецтранспорт", StateTransportOrder)
        };

        var worstFactor = factors
            .Select(f => (f.Name, Profit: baseIncome * f.Coefficient))
            .OrderBy(f => f.Profit)
            .First();

        return (worstFactor.Name, worstFactor.Profit);
    }


    public (string FactorName, double MaxRegret) CalculateSavage( double baseIncome,Alternative[] alternatives)
    {
        var factors = new (string Name, Func<Alternative, double> GetCoefficient)[]
        {
        ("Срыв импортозамещения", a => a.ImportDisruption),
        ("Технологический сдвиг", a => a.TechnologyShift),
        ("Дефицит кадров", a => a.StaffShortage),
        ("Валютный и логистический риски", a => a.CurrencyAndLogistics),
        ("Рост ставки и инфляция", a => a.InflationAndRate),
        ("Рост специального/оборонного заказа", a => a.SpecialOrderGrowth),
        ("Преференции за локализацию телематики", a => a.TelematicsBenefits),
        ("Субсидии и гранты", a => a.SubsidiesAndGrants),
        ("Государственный заказ на спецтранспорт", a => a.StateTransportOrder)
        };

        string worstFactorName = "";
        double maxRegret = double.MinValue;

        foreach (var factor in factors)
        {
            // Наилучший результат среди всех стратегий для данного фактора.
            double bestProfit = alternatives.Max(
                a => baseIncome * factor.GetCoefficient(a));

            // Результат текущей стратегии.
            double currentProfit = baseIncome * factor.GetCoefficient(this);

            // Упущенная выгода.
            double regret = bestProfit - currentProfit;

            if (regret > maxRegret)
            {
                maxRegret = regret;
                worstFactorName = factor.Name;
            }
        }

        return (worstFactorName, maxRegret);
    }

    public double CalculateHurwicz(double baseIncome, double optimismCoefficient = 0.7)
    {
        if (optimismCoefficient < 0 || optimismCoefficient > 1)
            throw new ArgumentOutOfRangeException(
                nameof(optimismCoefficient),
                "Коэффициент оптимизма должен быть от 0 до 1.");

        double[] scenarioIncomes =
        {
        baseIncome * ImportDisruption,
        baseIncome * TechnologyShift,
        baseIncome * StaffShortage,
        baseIncome * CurrencyAndLogistics,
        baseIncome * InflationAndRate,
        baseIncome * SpecialOrderGrowth,
        baseIncome * TelematicsBenefits,
        baseIncome * SubsidiesAndGrants,
        baseIncome * StateTransportOrder
    };

        double maxIncome = scenarioIncomes.Max();
        double minIncome = scenarioIncomes.Min();

        return optimismCoefficient * maxIncome
             + (1 - optimismCoefficient) * minIncome;
    }


}
