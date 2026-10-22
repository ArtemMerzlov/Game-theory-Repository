
using System;

class Program
{
    static void Main()
    {
        Alternative[] alternatives =
        {
            new FullLocalization(),
            new ScrewdriverAssembly(),
            new Hybrid()
        };

        double baseIncome = 1000000; //  рублей
        double annualProfit = 200000; //  рублей

        foreach (Alternative alternative in alternatives)
        {
            double NPV = -baseIncome + alternative.CalculateIncome(annualProfit);

            double fiveYearIncome = alternative.CalculateFiveYearIncome(NPV);

            Console.WriteLine($"\n{alternative.Name}");
            Console.WriteLine(
                $"Доход за год: {NPV:F2} руб.");
            Console.WriteLine(
                $"Доход за 5 лет с обесцениванием: " +
                $"{fiveYearIncome:F2}  руб.");

            var maximax = alternative.CalculateMaximax(annualProfit);

            Console.WriteLine(
                $"МаксиМакс — лучший фактор: {maximax.FactorName}");

            Console.WriteLine(
                $"МаксиМакс — максимальная прибыль фактора: " +
                $"{maximax.MaxProfit:N2} руб.");

            var wald = alternative.CalculateWald(annualProfit);

            Console.WriteLine(
                $"Вальд — худший фактор: {wald.FactorName}");

            Console.WriteLine(
                $"Вальд — минимальная прибыль: {wald.MinProfit:N2} руб.");


            var savage = alternative.CalculateSavage(
                annualProfit, alternatives);

            Console.WriteLine(
                $"Сэвидж — фактор максимального сожаления: {savage.FactorName}");

            Console.WriteLine(
                $"Сэвидж — максимальное сожаление: {savage.MaxRegret:N2} руб.");

            double hurwicz = alternative.CalculateHurwicz(annualProfit, 0.7);

            Console.WriteLine($"Критерий Гурвица (α = 0,7): {hurwicz:N2} руб.");

        }


        var bestWald = alternatives
            .Select(a => new
            {
                Strategy = a.Name,
                Result = a.CalculateWald(annualProfit)
            })
            .OrderByDescending(x => x.Result.MinProfit)
            .First();

        Console.WriteLine("\n=== ЛУЧШАЯ СТРАТЕГИЯ ПО ВАЛЬДУ ===");
        Console.WriteLine($"Стратегия: {bestWald.Strategy}");
        Console.WriteLine($"Худший фактор: {bestWald.Result.FactorName}");
        Console.WriteLine($"Гарантированная прибыль: {bestWald.Result.MinProfit:N2} руб.");


        var bestSavage = alternatives
            .Select(a => new
            {
                Strategy = a.Name,
                Result = a.CalculateSavage(annualProfit, alternatives)
            })
            .OrderBy(x => x.Result.MaxRegret)
            .First();

        Console.WriteLine("\n=== ЛУЧШАЯ СТРАТЕГИЯ ПО СЭВИДЖУ ===");
        Console.WriteLine($"Стратегия: {bestSavage.Strategy}");
        Console.WriteLine($"Фактор максимального сожаления: {bestSavage.Result.FactorName}");
        Console.WriteLine($"Минимальное максимальное сожаление: {bestSavage.Result.MaxRegret:N2} руб.");

        var bestHurwicz = alternatives
    .Select(a => new
    {
        Strategy = a.Name,
        Value = a.CalculateHurwicz(annualProfit, 0.7)
    })
    .OrderByDescending(x => x.Value)
    .First();

        Console.WriteLine("\n=== ЛУЧШАЯ СТРАТЕГИЯ ПО ГУРВИЦУ ===");
        Console.WriteLine($"Стратегия: {bestHurwicz.Strategy}");
        Console.WriteLine($"Критерий Гурвица: {bestHurwicz.Value:N2} руб.");
        Console.WriteLine("Коэффициент оптимизма: 0,7");

    }
}
