
public class FullLocalization : Alternative
{
    public FullLocalization() : base("Полная локализация")
    {
        Localization = 0.90;

        // Риски
        ImportDisruption = 1.7;
        TechnologyShift = 0.8;
        StaffShortage = 0.8;
        CurrencyAndLogistics = 1.7;
        InflationAndRate = 1.0;

        // Возможности
        SpecialOrderGrowth = 1.5;
        TelematicsBenefits = 1.8;
        SubsidiesAndGrants = 1.4;
        StateTransportOrder = 1.6;
    }
}

public class ScrewdriverAssembly : Alternative
{
    public ScrewdriverAssembly() : base("Отвёрточная сборка")
    {
        Localization = 0.20;

        // Риски
        ImportDisruption = 0.2;
        TechnologyShift = 0.5;
        StaffShortage = 1.0;
        CurrencyAndLogistics = 0.2;
        InflationAndRate = 0.8;

        // Возможности
        SpecialOrderGrowth = 0.4;
        TelematicsBenefits = 0.2;
        SubsidiesAndGrants = 0.4;
        StateTransportOrder = 0.2;
    }
}

public class Hybrid : Alternative
{
    public Hybrid() : base("Гибрид")
    {
        Localization = 0.60;

        // Риски
        ImportDisruption = 0.4;
        TechnologyShift = 1.0;
        StaffShortage = 1.0;
        CurrencyAndLogistics = 0.6;
        InflationAndRate = 0.8;

        // Возможности
        SpecialOrderGrowth = 1.2;
        TelematicsBenefits = 1.2;
        SubsidiesAndGrants = 1.1;
        StateTransportOrder = 1.2;
    }
}
